using UnityEngine;

public interface IAnimate
{
    public abstract void Animate(Animator animator, SpriteRenderer spr, Vector2 inputs);
}
