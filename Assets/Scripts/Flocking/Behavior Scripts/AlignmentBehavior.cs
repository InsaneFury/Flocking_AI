using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilterFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, Transform[] waypoints)
    {
        //if no neighbors, maintain current alignment
        if (context.Count == 0)
            return agent.transform.forward;

        //Add all points together and average
        Vector3 alignmentMove = Vector3.zero;

        //Adding Filter List of agents
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        for (int i = 0; i < filteredContext.Count; i++)
        {
            alignmentMove += filteredContext[i].forward;
        }

        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
