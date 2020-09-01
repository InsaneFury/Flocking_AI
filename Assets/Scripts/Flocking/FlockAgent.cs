using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{

    private Flock agentFlock;
    private Collider agentCollider;
    private Material material;
    public Flock GetAgentFlock => agentFlock;
    public Collider GetAgentCollider => agentCollider;
    public Material GetAgentMaterial => material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
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
}
