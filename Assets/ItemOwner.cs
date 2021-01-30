using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOwner : MonoBehaviour
{
    [Tooltip("What item does this owner want returned to them")]
    public GameObject itemOwned;

    public bool HasItem { get
        {
            return (itemOwned && GameObject.ReferenceEquals(itemOwned.GetComponent<Item>().heldBy, this.gameObject));
        } }
}