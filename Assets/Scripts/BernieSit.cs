using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BernieSit : MonoBehaviour
{
    public bool m_IsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<ItemHolder>().m_ItemHeld)
        {
            m_IsActive = true;
        }
        else
        {
            m_IsActive = false;

            if (GetComponent<Animator>().GetBool("IsSitting") == true)
            {
                GetComponent<Animator>().SetBool("IsSitting", false);
            }
        }


        if (m_IsActive && GetComponent<Animator>().GetBool("IsSitting") == false)
        {
            GetComponent<Animator>().SetBool("IsSitting", true);
        }
    }
}
