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
        GameObject swordImpulse = Instantiate(swordImpulsePrefab, transform.position, Quaternion.identity);
        Rigidbody swordImpulseRb = swordImpulse.GetComponent<Rigidbody>();

        swordImpulseRb.AddForce(transform.forward * shotSpeed);

        Destroy(swordImpulse, 3f);

        AudioSource.PlayClipAtPoint(shotSE, transform.position);

    }

}
