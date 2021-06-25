using UnityEngine;

public class UnitAnimationBehaviour : IAnimate
{
    public void Animate(Animator animator, SpriteRenderer spr, Vector2 inputs)
    {
        spr.flipX = inputs.x < 0;                                                   // maybe not the best choice as it create more scripts just to handle this one difference
        animator.SetFloat("vX", inputs.x);
        animator.SetFloat("vY", inputs.y);
    }
}
