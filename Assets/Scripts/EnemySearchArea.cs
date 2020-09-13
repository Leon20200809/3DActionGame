using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchArea : MonoBehaviour
{
    //判定用変数
    public bool isSearching;
    public GameObject player;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearching = true;
            player = col.gameObject;

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearching = false;
            player = null;
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
