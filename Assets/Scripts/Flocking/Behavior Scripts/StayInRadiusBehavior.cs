using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Transform center;
    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = center.position - agent.transform.position;
        float t = centerOffset.magnitude / radius;
        float ninetyPercent = 0.9f;

        if(t < ninetyPercent)
        {
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }
}
