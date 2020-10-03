using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class TestPlayerController : MonoBehaviour
{
    //移動系初期設定
    float x;
    float z;
    public float moveSpeed;
    public float knockBackPower;
    public float hitBackPower;

    //プレイヤー攻撃判定用
    //public Collider weaponCollider2;

    public GameObject sIgameObject;
        

    //プレイヤーの状態
    public enum PlayerState
    {
        Normal,
        Attack,
        Special,
        Battou,
        Noutou,
    }
    public PlayerState playerState = PlayerState.Normal;

    //コンポーネント宣言
    Rigidbody rb;
    Animator animator;

    //致命攻撃
    public bool isFatal = false;

    //溜め攻撃用
    float chargeTime = 0;
    public Text txtchargeTime;

    //武器切り替え
    public bool battou;
    public AnimatorOverrideController animatorOverride;


    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //武器の当たり判定、軌跡オフ
        weaponCollider.enabled = false;
        weaponCollider2.enabled = false;
        parryCollider.enabled = false;
        trail.enabled = false;
        trailparry.enabled = false;
        battou = true;

    }

    // Update is called once per frame
    void Update()
    {
        x = 0;
        z = 0;

        if (playerState != PlayerState.Attack)
        {
            //前後左右移動入力
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        //通常攻撃アクション入力
        if (Input.GetButtonDown("Fire1"))
        {
            if (isFatal == true)
            {
                playerState = PlayerState.Attack;
                animator.SetTrigger("Kumiuchi");

            }
            else
            {
                playerState = PlayerState.Attack;
                LookAtTarget();
                animator.SetTrigger("Attack");
            }

        }

        //特殊攻撃アクション入力
        if (Input.GetButtonDown("Fire2"))
        {
            playerState = PlayerState.Attack;
            LookAtTarget();
            animator.SetTrigger("SpecialAttack");
            //animator.SetInteger("AttackType", 1);

        }

        //パリィアクション入力 SP消費行動
        if (Input.GetButtonDown("Fire3"))
        {
            playerState = PlayerState.Attack;
            //移動制限
            LookAtTarget();
            animator.SetTrigger("Parry");
        }


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


        //武器切り替え
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (battou == false)
            {
                animator.SetTrigger("Battou");
                Debug.Log("抜刀");
                battou = true;
                //animator.runtimeAnimatorController = animatorOverride;
            }
            else if (battou == true)
            {
                Debug.Log("納刀");
                animator.SetTrigger("Noutou");
                battou = false;
                //animator.runtimeAnimatorController = animatorOverride.runtimeAnimatorController;
            }

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

    }

    //食らい判定用
    public Collider weaponCollider;
    public Collider weaponCollider2;
    public Collider parryCollider;

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

    //蹴り用の攻撃判定オン
    public void WeaponCol2ON()
    {
        weaponCollider2.enabled = true;
    }

    //蹴りの攻撃判定オフ
    public void WeaponCol2OFF()
    {
        weaponCollider2.enabled = false;
    }

    //パリィ判定オン
    public void ParryColON()
    {
        parryCollider.enabled = true;
    }

    //パリィ判定オフ
    public void ParryColOFF()
    {
        parryCollider.enabled = false;
    }

    //武器軌跡用
    public TrailRenderer trail;
    public TrailRenderer trailparry;

    //武器の軌跡オン
    public void TrailRendON()
    {
        trail.enabled = true;
    }

    //武器の軌跡オフ
    public void TrailRendOFF()
    {
        trail.enabled = false;
    }

    public void TrailParryON()
    {
        trail.enabled = true;
    }

    //武器の軌跡オフ
    public void TrailparryOFF()
    {
        trail.enabled = false;
    }

    /// <summary>
    /// 斬撃波
    /// </summary>
    public void SwIp()
    {
        sIgameObject.GetComponent<SwordImpulse>().SwordImpulseShot();
    }

    //敵オブジェクト識別用
    public Transform target = null;

    /// <summary>
    /// 簡易ロックオン
    /// </summary>
    public void LookAtTarget()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Debug.Log(enemy);
        if (enemy != null)
        {
            target = enemy.transform;
            //ターゲットとの距離が3未満の場合オートロックオン
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= 3)
            {
                transform.LookAt(target);
            }
            else
            {
                target = null;
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        //当てたオブジェクトのタグが""のとき
        if (other.CompareTag("Kumiuchi"))
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            Debug.Log("致命攻撃範囲内");
            isFatal = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Kumiuchi"))
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            Debug.Log("致命攻撃範囲外");
            animator.ResetTrigger("Kumiuchi");
            isFatal = false;
        }
    }

}
