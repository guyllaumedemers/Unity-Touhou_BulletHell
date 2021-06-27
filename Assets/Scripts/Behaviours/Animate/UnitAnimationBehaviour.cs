using UnityEngine;

public class UnitAnimationBehaviour : IAnimate
{
    public void Animate(Animator animator, SpriteRenderer spr, Vector2 inputs)
    {
        spr.flipX = inputs.x < 0;
        animator.SetFloat("vX", inputs.x);
        animator.SetFloat("vY", inputs.y);
    }
}
