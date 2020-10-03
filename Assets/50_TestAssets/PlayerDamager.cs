using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDamager : MonoBehaviour
{
    //ダメージ管理用
    public int damage;
    public bool isKnokcBack = false;
    public int knockBackPower;

    public Animator animator;
    //public GameObject root;

    //武器振り効果音
    public AudioClip weaponSE;

    Collider weaponController;
    public Collider parryController;

    // Start is called before the first frame update
    void Start()
    {
        weaponController = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        //当てたオブジェクトのタグが"Enemy"のとき
        if (other.CompareTag("Enemy"))
        {
            weaponController.enabled = false;
            StartCoroutine(attackHitStop(0.1f));
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            //Animator oterAnim = other.GetComponent<Animator>();
            Debug.Log("HIT!");
            //Debug.Log(other.transform.position);

            //oterAnim.SetTrigger("Attacked");

            // 自分の位置と接触してきたオブジェクトの位置とを計算して、距離と方向を出して正規化(速度ベクトルを算出)
            Vector3 distination = (other.transform.position - transform.position).normalized;
            distination = new Vector3(distination.x, distination.y, 0f);


            if (isKnokcBack == true)
            {
                Debug.Log("蹴りHIT！");
                otherRb.AddForce(distination * knockBackPower * 5, ForceMode.Impulse);
            }
            else
            {
                //プレイヤー位置を初期化
                //rb.velocity = Vector3.zero;

                //ノックバック距離
                otherRb.AddForce(distination * knockBackPower, ForceMode.Impulse);
            }
        }

        if (other.CompareTag("Kumiuchi"))
        {
            parryController.enabled = false;
            Debug.Log(parryController);
        }

    }

    /// <summary>
    /// ヒットストップ
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator attackHitStop(float time)
    {
        animator.speed = 0.1f;
        yield return new WaitForSeconds(time);
        animator.speed = 1.0f;
    }

    void SwordImpulseShot()
    {
    }


}
