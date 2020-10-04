using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySuccess : MonoBehaviour
{
    Rigidbody rb;

    public GameObject parryColPrefab;
    public TestEnemyController testEnemyCont;

    public Animator animator;
    //成功エフェクト
    public ParticleSystem parrySuccessPat;
    //成功SE
    public AudioClip parrySuccessSE;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //testEnemyCont = GetComponent<TestEnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parry"))
        {
            testEnemyCont.isFataled = true;
            Debug.Log("パリィ成功！");
            GameObject gameObject = Instantiate(parryColPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 1.5f);
            animator.SetTrigger("ParrySuccess");
            AudioSource.PlayClipAtPoint(parrySuccessSE, transform.position);
            //エフェクトを生成する
            parrySuccessPat.Play();
        }
    }
}
