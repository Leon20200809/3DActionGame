using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    //移動系初期設定
    float x;
    float z;
    public float moveSpeed;

    //ステータス設定
    public PlayerUIManager playerUIManager;
    public int maxHp = 1000;
    public int hp;
    public int maxSp = 3000;
    public int sp;

    //死亡判定
    bool isDead = false;

    //落命テキスト取得
    public GameObject gameOverText;
    public AudioClip voiceSE2;

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
        //初期ステータス設定
        hp = maxHp;
        sp = maxSp;
        playerUIManager.Init(this);


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
        //死亡判定
        if (isDead)
        {
            return;
        }

        //前後左右移動入力
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //通常攻撃アクション入力
        if (Input.GetButtonDown("Fire1"))
        {
            LookAtTarget();
            animator.SetTrigger("Attack");
            
        }

        //特殊攻撃アクション入力 SP消費行動
        if (Input.GetButtonDown("Fire2"))
        {
            if (sp >= 1000)
            {
                sp -= 1000;
                playerUIManager.UpdateSP(sp);

                LookAtTarget();
                animator.SetTrigger("Attack_H");
            }
            
        }

        //パリィアクション入力 SP消費行動
        if (Input.GetButtonDown("Fire3"))
        {
            if (sp >= 1300)
            {
                sp -= 1300;
                playerUIManager.UpdateSP(sp);

                LookAtTarget();
                animator.SetTrigger("Parry");
            }
            
        }

        //回避アクション入力 SP消費行動
        if (Input.GetButtonDown("Jump"))
        {
            if (sp >= 600)
            {
                sp -= 600;
                playerUIManager.UpdateSP(sp);

                animator.SetTrigger("Dodge");
                Vector3 direction = transform.position + new Vector3(x, 0, z) * moveSpeed;
                transform.LookAt(direction);
            }
            
        }

        //SP自然回復
        SpRecover();

        void SpRecover()
        {
            sp++;
            if (sp >= maxSp)
            {
                sp = maxSp;
            }
            playerUIManager.UpdateSP(sp);
        }
    }

    
    private void FixedUpdate()  //演算処理はここに書く
    {
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

        //移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        //キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        //移動アニメーション開始
        animator.SetFloat("Speed", rb.velocity.magnitude);

        /*//移動先へ方向転換
        Vector3 direction = transform.position + new Vector3(x, 0, z) * moveSpeed;
        transform.LookAt(direction);

        //速度設定
        rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        animator.SetFloat("Speed", rb.velocity.magnitude);*/


    }

    //プレイヤーの食らい判定
    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }
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
            GenerateEffect(other.gameObject);
            Damage(damager.damage);
            
        }
    }

    //ダメージ管理
    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            animator.SetTrigger("Dead");
            rb.velocity = Vector3.zero;
            
        }
        playerUIManager.UpdateHP(hp);
        Debug.Log("Player残りHP：" + hp);

    }

    public void DeadEnd()
    {
        //やられボイス再生
        AudioSource.PlayClipAtPoint(voiceSE2, animator.gameObject.transform.position);
        animator.GetComponent<PlayerController>().gameOverText.SetActive(true);

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
        if (target != null)
        {
            //ターゲットとの距離が3未満の場合オートロックオン
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= 3)
            {
                transform.LookAt(target);
            }
        }
        

    }

    //居合い移動用
    public void IaiMove()
    {
        transform.DOLocalMove(transform.forward * 4, 1.5f).SetRelative();
    }

    //回避移動用
    public void DodgeMove()
    {
        transform.DOLocalMove(transform.forward * 5, 0.8f).SetRelative();
    }


    public void GenerateEffect(GameObject other)
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}

