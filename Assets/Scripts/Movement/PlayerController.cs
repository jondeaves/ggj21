using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float m_MovementSpeed = 40;
    public float m_TurnSpeed = 240;

    private GGJ21 m_PlayerControls;
    private Rigidbody m_RigidBody;
    private GameObject m_ItemHeld;
    private Vector2 m_MoveVector;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        
        m_PlayerControls = new GGJ21();
        m_PlayerControls.Player.Move.started += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Move.performed += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Move.canceled += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        float step = m_MovementSpeed * Time.smoothDeltaTime;
        Vector3 newPosition = transform.position + (new Vector3(m_MoveVector.x, 0, m_MoveVector.y) * step);

        m_RigidBody.MovePosition(newPosition);
        //transform.LookAt(newPosition);
    }

    private void OnEnable()
    {
        m_PlayerControls.Player.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Player.Disable();
    }
}