using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidBody;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        _rigidBody.velocity = new Vector3(mH * speed, _rigidBody.velocity.y, mV * speed);
    }
}
