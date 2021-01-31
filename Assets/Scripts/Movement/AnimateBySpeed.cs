using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateBySpeed : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.autoBraking = false;

        if (!m_Animator)
        {
            m_Animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update animations based on movement speed
        // Should attack if our item is 'locked'
        if (GetComponent<Steal>() != null && GetComponent<Steal>().objectToSteal.GetComponent<Item>().m_IsLocked && m_Agent.velocity.magnitude == 0 && m_Animator.GetBool("isAttacking") == false)
        {
            m_Animator.SetBool("isIdle", false);
            m_Animator.SetBool("isWalking", false);
            m_Animator.SetBool("isRunning", false);
            m_Animator.SetBool("isAttacking", true);

        }
        else if (m_Agent.velocity.magnitude == 0 && m_Animator.GetBool("isIdle") == false)
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

    }
}
