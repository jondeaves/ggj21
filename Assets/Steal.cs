using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : MonoBehaviour
{
    [Tooltip("This is the object that the actor will steal")]
    public GameObject objectToSteal;

    public bool HasItem()
    {
        return GameObject.ReferenceEquals(objectToSteal.GetComponent<Item>().heldBy, this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasItem() && objectToSteal)
        {
            GetComponent<HideAndAvoid>().GoTo(objectToSteal.transform.position);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // If we have item and hit the player, give the item to the player
        if(HasItem() &&  col.gameObject.CompareTag("Player"))
        {
            objectToSteal.GetComponent<Item>().heldBy = col.gameObject;
        }
    }
}
