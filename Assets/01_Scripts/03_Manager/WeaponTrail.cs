using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    public GameObject trailPrefab;

    public void TrailRendON()
    {
        //trail.enabled = true;
        //エフェクトを生成する
        Instantiate(trailPrefab, transform.position, Quaternion.identity);
    }

    public void TrailRendOFF()
    {
        //trail.enabled = false;
        Destroy(this.gameObject);

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
