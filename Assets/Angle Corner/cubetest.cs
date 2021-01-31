using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubetest : MonoBehaviour
{
    // Start is called before the first frame update
      void Start()
    {
        UnityEngine.AI.NavMeshAgent m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Agent.destination = new Vector3(-6, 35, 13);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
