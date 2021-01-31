using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LameFollowCamera : MonoBehaviour
{
    [Tooltip("The thing the camera follows")]
    public Transform m_Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            m_Target.position.x,
            transform.position.y,
            m_Target.position.z
        );
    }
}
