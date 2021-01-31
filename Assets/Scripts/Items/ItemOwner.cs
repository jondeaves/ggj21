using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOwner : MonoBehaviour
{
    [Tooltip("What item does this owner want returned to them")]
    public GameObject m_ItemOwned;

    public bool HasItem { get
        {
            return (m_ItemOwned && GameObject.ReferenceEquals(m_ItemOwned.GetComponent<Item>().m_HeldBy, this.gameObject));
        } }
}