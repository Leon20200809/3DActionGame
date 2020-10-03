using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordImpulseDamager : MonoBehaviour
{
    public GameObject explosionPE;
    public AudioClip explosionSE;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT!");
        Debug.Log(collision.gameObject.name);
        GameObject exPe = Instantiate(explosionPE, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSE,transform.position);
        Destroy(this.gameObject);
        Destroy(exPe, 1.5f);

        Vector3 explosionPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, 10f);

        foreach(Collider hit in colliders)
        {
            Rigidbody hitRb = hit.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                hitRb.AddExplosionForce(5000f, explosionPos, 5f);
            }
        }

    }


}
