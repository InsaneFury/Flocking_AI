using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/FollowWaypointsBehaviour")]
public class FollowWaypointsBehavior : FilterFlockBehaviour
{
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public float minDistanceToNext = 5f;
    public int currentPoint = 0;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock,Transform[] waypoints)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;

        //Add all points together and average
        Vector3 direction = waypoints[currentPoint].position - agent.transform.position;

        float distanceOfPoint = Vector3.Distance(waypoints[currentPoint].position, agent.transform.position);
        if (distanceOfPoint <= minDistanceToNext)
        {
            currentPoint++;
            if (currentPoint >= waypoints.Length) currentPoint = 0;
            direction = waypoints[currentPoint].position - agent.transform.position;
        }

        return direction;
    }
}
