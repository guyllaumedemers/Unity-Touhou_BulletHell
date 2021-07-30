using UnityEngine;

public class UnitAnimationBehaviour : IPointerExitHandler
{
    public void Animate(Animator animator, SpriteRenderer spr, Vector2 inputs)
    {
        if (!animator || !spr)
        {
            LogWarning($"A Component is missing on the active gameobject : Animator {animator.name} SpriteRenderer {spr.name}");
            return;
        }
        spr.flipX = inputs.x < 0;
        animator.SetFloat("vX", inputs.x);
        animator.SetFloat("vY", inputs.y);
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Unit Animation Behaviour] " + msg);
}
