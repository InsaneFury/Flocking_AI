using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{

    private Flock agentFlock;
    private Collider agentCollider;
    public Flock GetAgentFlock => agentFlock;
    public Collider GetAgentCollider => agentCollider;

    public GameObject threat;
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }

    public void TransformToThreat(float range)
    {
        Collider[] food = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < food.Length; i++)
        {
            Instantiate(threat, transform.position, Quaternion.identity);
            Destroy(food[i].gameObject);
        }
        Debug.Log($"Agent {gameObject.name} has transformed to an enemy");
    }
}
