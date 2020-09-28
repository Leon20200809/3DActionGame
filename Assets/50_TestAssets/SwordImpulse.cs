using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordImpulse : MonoBehaviour
{
    public GameObject swordImpulsePrefab;
    public float shotSpeed;
    public AudioClip shotSE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 斬撃波
    /// </summary>
    public void SwordImpulseShot()
    {
        GameObject swordImpulse = Instantiate(swordImpulsePrefab, transform.position, swordImpulsePrefab.transform.rotation);
        Rigidbody sIRb = swordImpulse.GetComponent<Rigidbody>();
        sIRb.AddForce(transform.forward * shotSpeed);
        AudioSource.PlayClipAtPoint(shotSE, transform.position);

        Destroy(swordImpulse, 3.0f);
    }

}
