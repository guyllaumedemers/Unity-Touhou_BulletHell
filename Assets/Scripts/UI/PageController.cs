using System.Collections;
using System.Linq;
using UnityEngine;

public class PageController : SingletonMonoPersistent<PageController>, IFlow
{
    private PageController() { }
    private Page[] pages;
    private Hashtable hashPages;
    private Coroutine co_WaitAnimation;
    private PageTypeEnum entryPage;

    #region public functions

    public void TurnPageOn(PageTypeEnum type)
    {
        if (type.Equals(PageTypeEnum.None)) return;
        else if (!PageExist(type))
        {
            LogWarning("You are trying to turn a page on " + type + " that has not been registered");
            return;
        }
        else
        {
            Page page = GetPage(type);
            page.gameObject.SetActive(true);
            page.Animate(true);
        }
    }

    public void TurnPageOff(PageTypeEnum off, PageTypeEnum on = PageTypeEnum.None, bool waitForExit = false)
    {
        if (off.Equals(PageTypeEnum.None)) return;
        else if (!PageExist(off))
        {
            LogWarning("You are trying to turn a page off " + off + " that has not been registered");
            return;
        }
        else
        {
            Page offpage = GetPage(off);
            if (offpage.gameObject.activeSelf)
            {
                offpage.Animate(false);
            }

            if (waitForExit)
            {
                Page onpage = GetPage(on);
                UnRegisterCoroutine();
                RegisterCoroutine(onpage, offpage);
            }
            else
            {
                TurnPageOn(on);
            }
        }
    }

    #endregion

    #region private functions

    private IEnumerator WaitForPageExit(Page on, Page off)
    {
        while (off.state != PageFlag.FLAG_NONE)
        {
            yield return null;
        }
        TurnPageOn(on.type);
    }

    private void RegisterAllPages()
    {
        foreach (var page in pages) RegisterPage(page);
    }

    private void RegisterPage(Page page)
    {
        if (PageExist(page.type))
        {
            LogWarning("Type " + page.type + " is already registered");
            return;
        }
        hashPages.Add(page.type, page);
    }

    private Page GetPage(PageTypeEnum type)
    {
        if (!PageExist(type))
        {
            LogWarning("You are trying to get a page " + type + " that as not been registered");
            return null;
        }
        return hashPages[type] as Page;
    }

    private bool PageExist(PageTypeEnum type) => hashPages.ContainsKey(type);

    private void RegisterCoroutine(params Page[] pages)
    {
        if (pages.Length < 2 || pages.Length > 2)
        {
            LogWarning("Wait for Page Exit Coroutine takes in arguments 2 params");
            return;
        }
        co_WaitAnimation = StartCoroutine(WaitForPageExit(pages[0], pages[1]));
    }

    private void UnRegisterCoroutine()
    {
        if (co_WaitAnimation != null) StopCoroutine(co_WaitAnimation);
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Page Controller] : " + msg);

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        hashPages = new Hashtable();
        pages = GameObject.FindGameObjectsWithTag(Globals.page).Select(page => page.GetComponent<Page>()).ToArray();
        entryPage = PageTypeEnum.Menu;
    }

    public void InitializationMethod()
    {
        RegisterAllPages();
        foreach (var page in pages) TurnPageOff(page.type);
        if (entryPage != PageTypeEnum.None) TurnPageOn(entryPage);
    }

    public void UpdateMethod() { }

    #endregion
}
