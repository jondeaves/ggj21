using UnityEngine;

public class Item : MonoBehaviour
{
    [Tooltip("The object that is holding this item")]
    public GameObject m_HeldBy;

    [Tooltip("This is where the holder will hold... Like a handle or something")]
    public Vector3 m_HoldOffset;

    [Tooltip("If true, then can no longer change hands")]
    public bool m_IsLocked = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_HeldBy != null)
        {
            ItemHolder holder = m_HeldBy.GetComponent<ItemHolder>();
            this.transform.position = holder.m_HeldItemPosition.position + m_HoldOffset;
        }
    }

    public void ChangeOwner(GameObject owner, bool shouldLock = false)
    {
        if (m_IsLocked)
            return;

        m_HeldBy = owner;

        // Set item holder
        foreach(ItemHolder ih in GetComponents<ItemHolder>())
        {
            ih.m_ItemHeld = null;
        }
        owner.GetComponent<ItemHolder>().m_ItemHeld = this.gameObject;

        m_IsLocked = shouldLock;
    }
}
