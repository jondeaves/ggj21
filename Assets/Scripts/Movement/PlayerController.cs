using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float m_MovementSpeed = 40;
    public float m_TurnSpeed = 240;
    public float m_ExtraRotationSpeed = 8;

    private GGJ21 m_PlayerControls;
    private Rigidbody m_RigidBody;
    private Vector2 m_MoveVector;
    private Animator m_Animator;
    private NavMeshAgent m_Agent;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();

        m_PlayerControls = new GGJ21();
        m_PlayerControls.Player.Move.started += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Move.performed += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Move.canceled += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        float step = m_MovementSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + (new Vector3(m_MoveVector.x, 0, m_MoveVector.y) * step);
        bool hasMoved = (m_MoveVector.x != 0 && m_MoveVector.y != 0);

        if (hasMoved)
        {
            m_Agent.destination = newPosition;
        }

        //m_RigidBody.MovePosition(newPosition);
        transform.LookAt(new Vector3(newPosition.x, newPosition.y, newPosition.z));

        if (hasMoved && m_Animator.GetBool("IsRunning") == false)
        {
            m_Animator.SetBool("IsRunning", true);
        }
        else if (!hasMoved && m_Animator.GetBool("IsRunning") == true)
        {
            m_Animator.SetBool("IsRunning", false);
        }

        //ExtraRotation();
    }

    private void OnEnable()
    {
        m_PlayerControls.Player.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Player.Disable();
    }

    void ExtraRotation()
    {
        Vector3 lookrotation = m_Agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), m_ExtraRotationSpeed * Time.deltaTime);
    }
}