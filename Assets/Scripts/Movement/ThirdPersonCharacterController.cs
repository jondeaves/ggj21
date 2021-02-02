using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [Tooltip("How fast the player moves")]
    [SerializeField]
    private float m_Speed = 10f;

    [Tooltip("How fast the player turns")]
    [SerializeField]
    private float m_RotationSpeed = 10f;

    private Animator m_Animator;
    private GGJ21 m_PlayerControls;
    private Vector2 m_MoveVector;
    private Vector2 m_LookVector;

    // Start is called before the first frame update
    void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();

        m_PlayerControls = new GGJ21();
        m_PlayerControls.Player.Move.started += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Move.performed += ctx => m_MoveVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Move.canceled += ctx => m_MoveVector = Vector2.zero;

        m_PlayerControls.Player.Look.started+= ctx => m_LookVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Look.performed += ctx => m_LookVector = ctx.ReadValue<Vector2>();
        m_PlayerControls.Player.Look.canceled += ctx => m_LookVector = Vector2.zero;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        float moveStep = m_Speed * Time.deltaTime;
        Vector3 playerMovement = new Vector3(m_MoveVector.x, 0, m_MoveVector.y) * moveStep;
        transform.Translate(playerMovement, Space.Self);

        // Rotate
        float rotateStep = (m_RotationSpeed * 10) * Time.deltaTime;
        Vector3 playerRotation = new Vector3(0, m_LookVector.x, 0) * rotateStep;
        transform.Rotate(playerRotation, Space.Self);

        // Animation
        bool hasMoved = m_MoveVector.magnitude > 0;
        if (hasMoved && m_Animator.GetBool("IsRunning") == false)
        {
            m_Animator.SetBool("IsRunning", true);
        }
        else if (!hasMoved && m_Animator.GetBool("IsRunning") == true)
        {
            m_Animator.SetBool("IsRunning", false);
        }
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
