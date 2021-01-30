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
            ItemHolder holder = heldBy.GetComponent<ItemHolder>();
            this.transform.position = holder.m_HeldItemPosition.position;
        }
    }

    /*
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ChangeOwner(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Thief"))
        {

            bool wantsItem = GameObject.ReferenceEquals(col.gameObject.GetComponent<Steal>().objectToSteal, this.gameObject);

            if (wantsItem)
            {
                ChangeOwner(col.gameObject);
                col.gameObject.GetComponent<HideAndAvoid>().GoToRandom();
            }
        }
    }
    */

    public void ChangeOwner(GameObject owner)
    {
        heldBy = owner;
    }
}
