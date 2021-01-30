using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float m_MovementSpeed = 5;
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
        transform.LookAt(newPosition);


        /*
        Debug.Log(m_MoveVector);
        // Rotation
        float turnVelocity = Input.GetAxis("Horizontal") * Time.deltaTime * m_TurnSpeed;
        transform.Rotate(Vector3.up, turnVelocity);


        // Movement
        float forwardVelocity = Input.GetAxis("Vertical");

        Vector3 velocity = transform.forward * forwardVelocity;
        velocity.Normalize();

        m_RigidBody.MovePosition(transform.position + (velocity * m_MovementSpeed * Time.deltaTime));
        */





        /*
       Vector3 tempVect = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (tempVect == Vector3.zero)
        {
            return;
        }

        tempVect = tempVect.normalized * m_Speed * Time.deltaTime;

        m_RigidBody.MovePosition(transform.position + tempVect);
         */

        //transform.rotation = Quaternion.LookRotation(transform.position + tempVect);
        //transform.LookAt(transform.position + tempVect);
    }

    /*
public void OnMove(InputValue value)
    {
        m_MoveVector = value.Get<Vector2>();
        m_MoveVector.Normalize();
    } 
     */

    private void OnEnable()
    {
        m_PlayerControls.Player.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Player.Disable();
    }

    void OnCollisionEnter(Collision col)
    {
        // Collided with owner while holding item
        if (col.gameObject.CompareTag("Owner") && m_ItemHeld)
        {
            bool isOwnItem = GameObject.ReferenceEquals(col.gameObject.GetComponent<ItemOwner>().itemOwned, m_ItemHeld);

            if (isOwnItem)
            {
                m_ItemHeld.GetComponent<Item>().ChangeOwner(col.gameObject);
                m_ItemHeld = null;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // Collided with an item we can hold
        if (col.gameObject.CompareTag("Item"))
        {
            col.gameObject.GetComponent<Item>().ChangeOwner(this.gameObject);
            m_ItemHeld = col.gameObject;
        }
    }
}


/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    private const float INPUT_DEADZONE = 0.19f;

    [SerializeField]
    [Tooltip("Base movement speed")]
    public float m_Speed = 0.1f;

    [SerializeField]
    [Tooltip("Base turn speed")]
    public float m_TurnSpeed = 5f;

    [SerializeField]
    [Tooltip("Which gamepad player will be bound to")]
    public int PlayerNumber = 1;

    private bool m_IsMovingPreviousFrame;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_IsMovingPreviousFrame = false;
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = Input.GetAxis("Horizontal " + PlayerNumber) > INPUT_DEADZONE || Input.GetAxis("Horizontal " + PlayerNumber) < -INPUT_DEADZONE ||
            Input.GetAxis("Vertical " + PlayerNumber) > INPUT_DEADZONE || Input.GetAxis("Vertical " + PlayerNumber) < -INPUT_DEADZONE;

        if (isMoving != m_IsMovingPreviousFrame)
        {
            m_Animator.SetBool("isWalking", isMoving);
            m_IsMovingPreviousFrame = isMoving;
        }

        if (!isMoving)
        {
            return;
        }

        // m_Animator.SetBool("isWalking", true);
        Vector3 forwardVector = new Vector3(
            Input.GetAxis("Horizontal " + PlayerNumber),
            0,
            Input.GetAxis("Vertical " + PlayerNumber)
        );

        transform.position += (forwardVector * m_Speed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forwardVector), Time.time * m_TurnSpeed);
    }
}


*/