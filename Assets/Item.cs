using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Tooltip("The object that is holding this item")]
    public GameObject heldBy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heldBy != null)
        {
            this.transform.position = heldBy.transform.position + new Vector3(0, 1f, 0);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            heldBy = col.gameObject;
        }
        else if (col.gameObject.CompareTag("Thief"))
        {

            bool wantsItem = GameObject.ReferenceEquals(col.gameObject.GetComponent<Steal>().objectToSteal, this.gameObject);

            if (wantsItem)
            {
                heldBy = col.gameObject;
                col.gameObject.GetComponent<HideAndAvoid>().GoToRandom();
            }
        }
    }
}
