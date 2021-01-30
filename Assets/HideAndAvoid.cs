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

    /*
    public void RunFrom()
    {

        // store the starting transform
        startTransform = transform;

        //temporarily point the object to look away from the player
        transform.rotation = Quaternion.LookRotation(transform.position - player.position);

        //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
        // for this if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = transform.position + transform.forward * multiplyBy;
        //Debug.Log("runTo = " + runTo);

        //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

        NavMeshHit hit;    // stores the output in a variable called hit

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default"));
        //Debug.Log("hit = " + hit + " hit.position = " + hit.position);

        // just used for testing - safe to ignore
        nextTurnTime = Time.time + 5;

        // reset the transform back to our start transform
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        // And get it to head towards the found NavMesh position
        myNMagent.SetDestination(hit.position);
    }
    */
}
