using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{

    public LayerMask foodLayer;
    private Flock agentFlock;
    private Collider agentCollider;
    public Flock GetAgentFlock => agentFlock;
    public Collider GetAgentCollider => agentCollider;
    public GameObject threat;

    public int foodEaten = 0;
    private int foodLimit = 5;

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
        Collider[] foodNear = Physics.OverlapSphere(transform.position, range, foodLayer);
        if(foodNear.Length > 0)
        {
            foodNear[0].gameObject.SetActive(false);
            foodEaten++;
            Debug.Log($"Agent {gameObject.name} has eaten {foodEaten} foods");
        }

        if (CanDie()) 
        { 
            Instantiate(threat, transform.position, Quaternion.identity);
            Debug.Log($"Agent {gameObject.name} has transformed to an enemy");
        }
    }

    public bool CanDie()
    {
        if(foodEaten >= foodLimit)
        return true;
        else
        return false;
    }
}
