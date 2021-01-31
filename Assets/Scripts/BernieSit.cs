using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BernieSit : MonoBehaviour
{
    public GameObject chair;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= 4 && GetComponent<Animator>().GetBool("IsSitting") == false)
        {
            chair.SetActive(true);
            GetComponent<Animator>().SetBool("IsSitting", true);
        }


    }
}
