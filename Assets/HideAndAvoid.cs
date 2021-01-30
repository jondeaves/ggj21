using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HideAndAvoid : MonoBehaviour
{
    [Tooltip("This is the object that the actor will avoid")]
    public GameObject objectToAvoid;

    [Tooltip("If the object to avoid gets within this distance, the actor will find a hiding spot to run to")]
    public float dangerZoneDistance = 5f;

    [Tooltip("These are spots that the actor choose from when finding a hiding spot")]
    public List<Transform> hidingSpots = new List<Transform>();

    private Transform _target;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // If actor reaches it's hiding spot, then re-set.
        if (_target != null && _agent.remainingDistance <= 0)
        {
            _target = null;
        }


        // If no item in hand/paw/hoof then the next part is irrelevant
        // Or if we don't need to avoid anyone
        if (!GetComponent<Steal>().HasItem() || objectToAvoid == null)
        {
            return;
        }


        // If actor and object to avoid are in the danger zone then run away
        float distanceToObject = Vector3.Distance(this.transform.position, objectToAvoid.transform.position);
        if (distanceToObject < dangerZoneDistance && !_target)
        {
            GoToRandom();
        }
    }

    public void GoTo(Vector3 position)
    {
        _agent.destination = position;
    }

    public void GoToRandom()
    {
        _target = hidingSpots[UnityEngine.Random.Range(0, hidingSpots.Count)];
        _agent.destination = _target.position;
    }
}
