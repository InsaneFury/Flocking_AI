using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
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

    [Header("Waypoints Behavior")]
    public Transform[] points;

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
            GameObject agentToSpawn = Instantiate(
                agentPrefab.gameObject,
                Random.insideUnitSphere * startingCount * agentDensity + spawnPosition.position,
                Random.rotation,
                transform
                );
            agentToSpawn.name = $"Agent {i}";
            agentToSpawn.GetComponent<FlockAgent>().Initialize(this);
            agents.Add(agentToSpawn.GetComponent<FlockAgent>());
        }   
    }
    public float GetSquareAvoidanceRadius => squareAvoidanceRadius;

    void Update()
    {
        
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i])
            {
                List<Transform> context = GetNearByObjects(agents[i]);

                Vector3 move = behavior.CalculateMove(agents[i], context, this,points);
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
                agents[i].Move(move);
            }
        }
        
    }

    List<Transform>GetNearByObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        if (agent)
        {
            Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
            foreach (Collider collider in contextColliders)
            {
                if(collider != agent.GetAgentCollider)
                {
                    context.Add(collider.transform);
                }
            }
        }
        return context;
    }
}
