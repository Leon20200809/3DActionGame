using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移動系初期設定
    float x;
    float z;
    public float moveSpeed;

    //食らい判定用
    public Collider weaponCollider;

    //HITエフェクト用
    public GameObject effectPrefab;

    //武器軌跡用
    //public GameObject trailPrefab;
    public TrailRenderer trail;

    //敵オブジェクト識別用
    //public GameObject target = GameObject.FindGameObjectWithTag("Enemy");
    public Transform target;

    //コンポーネント宣言
    Rigidbody rb;
    Animator animator;
    



    void Start()
    {
        //コンポーネント取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        //武器の当たり判定オフ
        weaponCollider.enabled = false;

        //武器の軌跡エフェクトオフ
        trail.enabled = false;

    }

    void Update()
    {
        //前後左右移動入力
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //通常攻撃アクション入力
        if (Input.GetButtonDown("Fire1"))
        {
            LookAtTarget();
            animator.SetTrigger("Attack");
            
        }

        //特殊攻撃アクション入力
        if (Input.GetButtonDown("Fire2"))
        {
            LookAtTarget();
            animator.SetTrigger("Attack_H");
        }

        //パリィアクション入力
        if (Input.GetButtonDown("Fire3"))
        {
            LookAtTarget();
            animator.SetTrigger("Parry");
        }

        //回避アクション入力
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Dodge");
            Vector3 direction = transform.position + new Vector3(x, 0, z) * moveSpeed;
            transform.LookAt(direction);
        }
    }

    private void FixedUpdate()
    {
        //移動先へ方向転換
        Vector3 direction = transform.position + new Vector3(x, 0, z) * moveSpeed;
        transform.LookAt(direction);

        //速度設定
        rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    //プレイヤーの食らい判定
    private void OnTriggerEnter(Collider other)
    {
        //敵の食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる

        /*Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {

            animator.SetTrigger("Attacked");
            GenerateEffect(gameObject);
        }*/

        //食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる
        if (other.gameObject.TryGetComponent(out Damager damager))
        {
            //食らいモーション再生
            animator.SetTrigger("Attacked");
            GenerateEffect(other.gameObject);    // gameObject ですと、このスクリプトがアタッチされているゲームオブジェクトになります。 
        }
    }

    //武器の攻撃判定オン/オフ
    public void WeaponColON()
    {
        weaponCollider.enabled = true;
    }

    //武器の攻撃判定オン/オフ
    public void WeaponColOFF()
    {
        weaponCollider.enabled = false;
    }

    //武器の軌跡オン/オフ
    public void TrailRendON()
    {
        trail.enabled = true;
    }

    //武器の軌跡オン/オフ
    public void TrailRendOFF()
    {
        trail.enabled = false;
    }

    //簡易ロックオン
    public void LookAtTarget()
    {
        //ターゲットとの距離が3未満の場合オートロックオン
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= 3)
        {
            transform.LookAt(target);
        }

    }

    //居合い移動用
    public void IaiMove()
    {

    }

    public void GenerateEffect(GameObject other)
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
    }
}

