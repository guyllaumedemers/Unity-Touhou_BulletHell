using UnityEngine;

public class PlayerAnimationBehaviour : IAnimate
{
    public void Animate(Animator animator, SpriteRenderer spr, Vector2 inputs)
    {
        animator.SetFloat("vX", inputs.x);
        animator.SetFloat("vY", inputs.y);
    }
}
