using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [Tooltip("This is part of the holder that they hold with... Like a hand or something")]
    public Transform m_HeldItemPosition;

    [Tooltip("If provided, then the holder can only hold this Game Object")]
    public GameObject m_ItemConstraint;

    [Tooltip("The item this holder is currently holding")]
    public GameObject m_ItemHeld;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        // Collided with an item we can hold
        if (
            col.gameObject.CompareTag("Item") &&
            col.gameObject.GetComponent<Item>().m_IsLocked == false &&
            (m_ItemConstraint == null || GameObject.ReferenceEquals(m_ItemConstraint, col.gameObject))
        )
        {
            col.gameObject.GetComponent<Item>().ChangeOwner(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // Collided with owner while holding item
        if (col.gameObject.CompareTag("Owner") && m_ItemHeld)
        {
            bool isOwnItem = GameObject.ReferenceEquals(col.gameObject.GetComponent<ItemOwner>().m_ItemOwned, m_ItemHeld);

            if (isOwnItem)
            {
                m_ItemHeld.GetComponent<Item>().ChangeOwner(col.gameObject, true);
            }
        }
    }
}
