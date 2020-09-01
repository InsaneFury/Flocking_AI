using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Header("Spawning Settings")]
    [Range(10, 500)]
    public int startingCount = 250;
    public float agentDensity = 5f;
    public Transform spawnPosition;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 20f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed = 1f;
    float squareNeighborRadius = 1f;
    float squareAvoidanceRadius = 1f;


    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareAvoidanceRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent agentToSpawn = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * agentDensity + spawnPosition.position,
                Random.rotation,
                transform
                );
            agentToSpawn.name = $"Agent {i}";
            agentToSpawn.Initialize(this);
            agents.Add(agentToSpawn);
        }   
    }
    public float GetSquareAvoidanceRadius => squareAvoidanceRadius;

    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearByObjects(agent);

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform>GetNearByObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider collider in contextColliders)
        {
            if(collider != agent.GetAgentCollider)
            {
                context.Add(collider.transform);
            }
        }
        return context;
    }
}
