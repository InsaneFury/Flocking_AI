using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilterFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, Transform[] waypoints)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;

        //Add all points together and average
        Vector3 cohesionMove = Vector3.zero;

        //Adding Filter List of agents
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        for (int i = 0; i < filteredContext.Count; i++)
        {
            cohesionMove += context[i].position;
        }

        cohesionMove /= context.Count;

        //Create offset from agent position
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }
}
