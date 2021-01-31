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

    [Tooltip("Will walk when 'hunting' for an item to steal")]
    public float m_WalkSpeed = 3;

    [Tooltip("Will run when they have stolen an item")]
    public float m_RunSpeed = 9;

    [Tooltip("Number of seconds before moving to a new spot")]
    public float m_MoveTime = 20;

    [Tooltip("Speeds up turns")]
    public float m_ExtraRotationSpeed = 8;

    [Tooltip("True when the thief has lost their item to the original owner")]
    public bool m_IsFinished = false;

    private Transform _target;
    private NavMeshAgent _agent;

    private bool _firstRun = true;
    private Transform _finalDestination;

    private float m_MoveTimer = 0;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // If Cancelled, nothing else to do on the matter
        if (_finalDestination && _agent.remainingDistance <= 10)
        {
            transform.LookAt(_finalDestination);

            m_IsFinished = true;

            _agent.isStopped = true;
            _agent.speed = 0;
            _target = null;
            return;
        }

        // If actor reaches it's hiding spot, then re-set.
        if (_target != null && _agent.remainingDistance <= 2)
        {
            _target = null;

            if (_firstRun)
            {
                GoToRandom();
            }
        }


        // Run fast after stealing something
        GetComponent<NavMeshAgent>().speed = GetComponent<ItemHolder>().m_ItemHeld != null ? m_RunSpeed : m_WalkSpeed;


        // If no item in hand/paw/hoof then the next part is irrelevant
        // Or if we don't need to avoid anyone
        if (GetComponent<ItemHolder>().m_ItemHeld == null || objectToAvoid == null)
        {
            return;
        }


        // Go to a new place every [x] seconds
        m_MoveTimer += Time.deltaTime;
        if (m_MoveTimer >= m_MoveTime)
        {
            GoToRandom();
            m_MoveTimer = 0f;
        }


        // If actor and object to avoid are in the danger zone then run away
        float distanceToObject = Vector3.Distance(this.transform.position, objectToAvoid.transform.position);
        if (distanceToObject < dangerZoneDistance)
        {
            GoToRandom();
            /*
            Vector3 dirToPlayer = transform.position - objectToAvoid.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;

            GameObject.FindGameObjectWithTag("RunMarker").transform.position = newPos;
            GoTo(GameObject.FindGameObjectWithTag("RunMarker").transform);
            */

            // Reset move timer when running
            m_MoveTimer = 0f;
        }

        ExtraRotation();
    }

    public void GoTo(Transform target)
    {
        _target = target;
        _agent.destination = target.position;
    }

    public void GoToRandom()
    {
        _firstRun = false;
        _target = hidingSpots[UnityEngine.Random.Range(0, hidingSpots.Count)];
        _agent.destination = _target.position;
    }

    public void Cancel()
    {
        // Set final destination to the target, to go and cry at
        _finalDestination = GetComponent<Steal>().objectToSteal.GetComponent<Item>().transform;
        GoTo(_finalDestination);
    }

    
    public void RunFrom()
    {
        float multiplyBy = 1f;

        // store the starting transform
        Transform startTransform = transform;

        //temporarily point the object to look away from the player
        transform.rotation = Quaternion.LookRotation(transform.position - objectToAvoid.transform.position);

        //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
        // for this if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = transform.position + transform.forward * multiplyBy;
        Debug.Log("runTo = " + runTo);

        //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        NavMesh.SamplePosition(runTo, out NavMeshHit hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
        //Debug.Log("hit = " + hit + " hit.position = " + hit.position);

        // reset the transform back to our start transform
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        // And get it to head towards the found NavMesh position
        GameObject.FindGameObjectWithTag("RunMarker").transform.position = hit.position;
        GoTo(GameObject.FindGameObjectWithTag("RunMarker").transform);
    }

    void ExtraRotation()
    {
        Vector3 lookrotation = _agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), m_ExtraRotationSpeed * Time.deltaTime);
    }
}
