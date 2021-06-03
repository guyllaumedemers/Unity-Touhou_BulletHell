using UnityEngine;

public class MoveableBossBehaviour : IMoveable
{
    public Vector3 Move(Vector3 pos, float angle, float speed)
    {
        /*  How do I set the target position if the Vector3 pass as argument is the reference for my current position from which I update
         *  Maybe I could create a Waypoint System?
         *  
         *  Units would be moving to these waypoints as well as the Boss
         *  
         *  I could than set the order in which the waypoints are set has target depending on the Unit Type
         */
        return Vector3.zero;
    }
}
