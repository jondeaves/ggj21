using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : MonoBehaviour
{
    [Tooltip("This is the object that the actor will steal")]
    public GameObject objectToSteal;

    public bool HasItem()
    {
        return GameObject.ReferenceEquals(objectToSteal.GetComponent<Item>().m_HeldBy, this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!HasItem() && objectToSteal.GetComponent<Item>().m_IsLocked && GetComponent<HideAndAvoid>().m_IsFinished == false)
        {
            GetComponent<HideAndAvoid>().Cancel();
        }
        else if (!HasItem() && objectToSteal && !objectToSteal.GetComponent<Item>().m_IsLocked)
        {
            GetComponent<HideAndAvoid>().GoTo(objectToSteal.transform);
        }
    }
}
