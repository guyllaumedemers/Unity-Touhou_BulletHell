using UnityEngine;

public interface IPointerExitHandler
{
    public abstract void Animate(Animator animator, SpriteRenderer spr, Vector2 inputs);
}
