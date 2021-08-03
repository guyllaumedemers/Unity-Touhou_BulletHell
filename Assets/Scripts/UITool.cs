using System.Linq;
using UnityEngine;

public static class UITool
{
    #region public functions

    public static void SetUI(ref Canvas main, ref CanvasGroup[] groups, ref CanvasGroup active, string canvasName)
    {
        SetCanvas(ref main);
        SetCurtainsGroup(ref groups);
        SetUniqueCurtain(groups, ref active, canvasName);
    }

    public static void SetUniqueCurtain(CanvasGroup[] alphagroup, ref CanvasGroup first, string name)
    {
        if (alphagroup.Length < 1)
        {
            LogWarning("The array is empty");
            return;
        }
        else if (alphagroup.Length < 2)
        {
            first = alphagroup[0];
            return;
        }
        else first = alphagroup.Where(x => x.tag == name).FirstOrDefault();
    }

    #endregion


    #region private functions

    private static void SetCanvas(ref Canvas maincanvas)
    {
        maincanvas = GameObject.FindObjectOfType<Canvas>();
        if (!maincanvas || !Camera.main)
        {
            LogWarning($"There is no canvas in this scene {maincanvas} OR there is no camera with the tag main in the scene {Camera.main}");
            return;
        }

        maincanvas.renderMode = RenderMode.ScreenSpaceCamera;
        maincanvas.worldCamera = Camera.main;
    }

    private static void SetCurtainsGroup(ref CanvasGroup[] alphagroup)
    {
        alphagroup = GameObject.FindObjectsOfType<CanvasGroup>();
        if (alphagroup.Length < 0)
        {
            LogWarning("There is no canvas group in this scene");
            return;
        }

        foreach (var item in alphagroup)
        {
            item.alpha = 1;
            item.blocksRaycasts = false;
        }
    }

    private static void LogWarning(string msg) => Debug.LogWarning("[UI Tool] : " + msg);

    #endregion
}
