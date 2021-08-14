using System.Linq;
using UnityEngine;

public class StaircaseDecorator : PanelDecorator
{
    public override void PlayGraphicAnimation()
    {
        base.PlayGraphicAnimation();
        StaircaseAnimation();
    }

    #region private functions

    private void StaircaseAnimation()
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "StaircaseAnimation").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.StaircaseAnimation());
    }

    #endregion
}
