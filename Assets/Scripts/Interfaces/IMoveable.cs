using UnityEngine;

public interface IMoveable
{
    public abstract Vector3 Move(float angle, float speed, float t, params Vector3[] pos);

    /* Units are going to move from outside the screen to a specific location on the screen => lerping to it 
     * Points are going to be specific to the level phase we are currently in
     * 
     * Units are going to spawn at interval on both sides => spaced position
     * When a unit reach its destination
     *          he will play his firing animation => IPattern.Play() => OR a chain of pattern => chain of responsability? One pattern play trigger another pattern?
     *          than travel out to an exit point
     * 
     * Some Units fire only when going back
     * Some Units fire before reaching the destination points
     * Some Untis have multiple pattern to play
     * 
     */
}
