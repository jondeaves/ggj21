using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public Animator m_Animator;

    public float m_ExtraRotationSpeed = 8;

    private Vector3[] m_PatrolPoints;

    public Vector3 firstPoint = new Vector3(4, 0, 4);
    public Vector3 secondPoint = new Vector3(16, 0, 4);

    public float walkSpeed = 3f;
    public float runSpeed = 9f;

    private int m_DestPoint = 0;
    private int lapNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.autoBraking = false;

        if(!m_Animator)
        {
            m_Animator = GetComponentInChildren<Animator>();
        }

        m_PatrolPoints = new Vector3[]
        {
            firstPoint,
            secondPoint,
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Update animations based on movement speed
        if (m_Agent.velocity.magnitude == 0 && m_Animator.GetBool("isIdle") == false)
        {
            // Idle
            m_Animator.SetBool("isIdle", true);
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isRunning", false);
            m_Animator.SetBool("isAttacking", false);
        }
        else if (m_Agent.velocity.magnitude > 0 && m_Agent.velocity.magnitude <= 3 && m_Animator.GetBool("isWalking") == false)
        {
            // Walk
            m_Animator.SetBool("isIdle", false);
            m_Animator.SetBool("isWalking", true);
            m_Animator.SetBool("isRunning", false);
            m_Animator.SetBool("isAttacking", false);
        }
        else if (m_Agent.velocity.magnitude > 5 && m_Animator.GetBool("isRunning") == false)
        {
            // Run
            m_Animator.SetBool("isIdle", false);
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isRunning", true);
            m_Animator.SetBool("isAttacking", false);
        }

        if (!m_Agent.pathPending && m_Agent.remainingDistance < 1f)
            GotoNextPoint();

        ExtraRotation();
    }

    void GotoNextPoint()
    {
        // Nothing to do if no points have been set up
        if (m_PatrolPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        m_Agent.destination = m_PatrolPoints[m_DestPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        if (m_DestPoint == 0)
        {
            lapNumber += 1;

            m_Agent.speed = lapNumber % 2 == 0 ? runSpeed : walkSpeed;
        }
        m_DestPoint = (m_DestPoint + 1) % m_PatrolPoints.Length;
    }

    void ExtraRotation()
    {
        Vector3 lookrotation = m_Agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), m_ExtraRotationSpeed * Time.deltaTime);
    }
}
