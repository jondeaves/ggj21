using UnityEngine;
using UnityEngine.AI;

public class AIWander : MonoBehaviour
{
    public Transform[] m_PatrolPoints;
    public float m_ExtraRotationSpeed;

    private NavMeshAgent m_Agent;
    private int m_DestPoint = 0;

    public AIState m_CurrentState = AIState.Wandering;

    public enum AIState
    {
        Idle,
        Wandering
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        m_Agent.autoBraking = false;

        GotoNextPoint();
    }


    void Update()
    {
        bool hasItem = GetComponent<ItemOwner>().HasItem;
        if (hasItem)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            m_CurrentState = AIState.Idle;
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            m_CurrentState = AIState.Wandering;
        }

        switch (m_CurrentState)
        {
            case AIState.Idle:
                m_Agent.isStopped = true;
                break;
            case AIState.Wandering:
                // Choose the next destination point when the agent gets
                // close to the current one.
                m_Agent.isStopped = false;
                if (!m_Agent.pathPending && m_Agent.remainingDistance < 0.5f)
                    GotoNextPoint();

                ExtraRotation();
                break;
        }
    }

    void GotoNextPoint()
    {
        // Nothing to do if no points have been set up
        if (m_PatrolPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        m_Agent.destination = m_PatrolPoints[m_DestPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        m_DestPoint = (m_DestPoint + 1) % m_PatrolPoints.Length;
    }

    void ExtraRotation()
    {
        Vector3 lookrotation = m_Agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), m_ExtraRotationSpeed * Time.deltaTime);
    }
}
