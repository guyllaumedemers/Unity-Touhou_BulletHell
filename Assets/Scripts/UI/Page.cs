using System.Collections;
using System.Linq;
using UnityEngine;

public class Page : MonoBehaviour
{
    public PageFlag state { get; private set; }
    public PageTypeEnum type;
    public bool useAnimation;
    private Animator animator;
    private Coroutine co_Animation;
    [SerializeField] private GameObject header;

    #region public functions

    public void Animate(bool on)
    {
        if (useAnimation)
        {
            animator.SetBool("FLAG_ON", on);
            UnRegisterCoroutine();
            RegisterCoroutine(on);
        }
        else
        {
            if (!on) gameObject.SetActive(false);
        }
    }
    #endregion

    #region private functions
    private IEnumerator AwaitAnimation(bool on)
    {
        state = on ? PageFlag.FLAG_ON : PageFlag.FLAG_OFF;
        //Wait for the animator to reach the target state
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(state.ToString()))
        {
            yield return null;
        }
        //Wait for the animator to finish his animation
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < Globals.animationWaitTime)
        {
            yield return null;
        }
        state = PageFlag.FLAG_NONE;

        if (!on)
        {
            gameObject.SetActive(false);
        }
    }

    private void CheckAnimator()
    {
        if (!useAnimation) return;
        else
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                LogWarning("You are missing an animator component OR have set the page to be using animation");
                return;
            }
        }
    }

    private void RegisterCoroutine(bool on) => co_Animation = StartCoroutine(AwaitAnimation(on));

    private void UnRegisterCoroutine()
    {
        if (co_Animation != null) StopCoroutine(co_Animation);
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Page] : " + msg + " " + gameObject.name);

    #endregion

    private void OnEnable()
    {
        CheckAnimator();
        if (!header)
        {
            LogWarning("You have no header attach the the page script");
            return;
        }
        header.SetActive(true);
    }

    private void OnDisable()
    {
        if (!header)
        {
            LogWarning("You have no header attach the the page script");
            return;
        }
        header.SetActive(false);
    }
}

public enum PageFlag
{
    FLAG_ON,
    FLAG_OFF,
    FLAG_NONE
}
