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
    float chargeTime = 0;
    public Text txtchargeTime;

    public Animator animator;
    //public GameObject root;

    //武器振り効果音
    public AudioClip weaponSE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            chargeTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (chargeTime > 2.0f)
            {
                //溜め攻撃
                animator.SetTrigger("ChargeAttack");
                Debug.Log("溜め攻撃");
                chargeTime = 0;
            }
            else
            {
                chargeTime = 0;
            }
        }

        txtchargeTime.text = chargeTime.ToString("F2");
    }

    void OnTriggerEnter(Collider other)
    {
        //当てたオブジェクトのタグが"Enemy"のとき
        if (other.CompareTag("Enemy"))
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            Debug.Log("HIT!");

            // 自分の位置と接触してきたオブジェクトの位置とを計算して、距離と方向を出して正規化(速度ベクトルを算出)
            Vector3 distination = (other.transform.position - transform.position).normalized;


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
    }

}
