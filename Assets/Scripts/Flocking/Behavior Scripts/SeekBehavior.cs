using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SeekBehavior")]
public class SeekBehavior : FilterFlockBehaviour
{
    public float seekRadius = 5f;
    public float eatRadius = 4f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, Transform[] waypoints)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;

        //Add all points together and average
        Vector3 avoidanceMove = Vector3.zero;

        //Adding Filter List of agents
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        for (int i = 0; i < filteredContext.Count; i++)
        {
            Vector3 closestPoint = filteredContext[i].gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position);
            if (Vector3.SqrMagnitude(closestPoint - agent.transform.position) < flock.GetSquareAvoidanceRadius + seekRadius)
            {
                if (Vector3.SqrMagnitude(closestPoint - agent.transform.position) < eatRadius)
                {
                    
                    flock.agents.Remove(agent);
                    agent.TransformToThreat(eatRadius);
                    return Vector3.zero;
                }
                avoidanceMove +=  closestPoint - agent.transform.position;
            }
        }

        return avoidanceMove;
    }
}
