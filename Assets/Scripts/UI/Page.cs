using System.Collections;
using UnityEngine;

public class Page : MonoBehaviour, IFlow
{
    public PageFlag state { get; private set; }
    public PageTypeEnum type;
    public bool useAnimation;
    private Animator animator;
    private Coroutine co_Animation;

    #region public functions

    public void Animate(bool on)
    {
        if (useAnimation)
        {
            animator.SetBool("on", on);
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
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
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
                Debug.LogWarning("You are missing an animator component OR have set the page to be using animation");
                return;
            }
        }
    }

    private void RegisterCoroutine(bool on) => co_Animation = StartCoroutine(AwaitAnimation(on));

    private void UnRegisterCoroutine()
    {
        if (co_Animation != null) StopCoroutine(co_Animation);
    }

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }

    #endregion

    private void OnEnable() => CheckAnimator();
}

public enum PageFlag
{
    FLAG_ON,
    FLAG_OFF,
    FLAG_NONE
}
