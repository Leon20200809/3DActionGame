using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //移動系初期設定
    float x;
    float z;
    public float moveSpeed;
    public float knockBackPower;
    public float hitBackPower;

    //ステータス設定
    public PlayerUIManager playerUIManager;
    public int maxHp;
    public int hp;
    public int maxSp;
    public int sp;

    //お薬所持数
    public int maxElixir;
    public int elixir;
    public Text ElixirLabel;

    //死亡判定
    bool isDead = false;

    //落命テキスト取得
    public GameObject rakumei;
    public AudioClip voiceSE2;

    //食らい判定用
    public Collider weaponCollider;
    public Collider weaponCollider2;

    //ダメージ管理用
    public Damager damager;

    //HITエフェクト用
    public GameObject effectPrefab;

    //被ダメージSE
    public AudioClip dmageSE;


    //武器軌跡用
    public TrailRenderer trail;

    //敵オブジェクト識別用
    public Transform target = null;
    GameObject enemy;

    //コンポーネント宣言
    Rigidbody rb;
    Animator animator;

  

    //プレイヤーの状態
    public enum PlayerState
    {
        Normal,
        Attack,
        Special,
    }

    public PlayerState playerState = PlayerState.Normal;


    void Start()
    {
        //初期ステータス設定
        hp = maxHp;
        sp = maxSp;
        
        elixir = maxElixir;
        playerUIManager.Init(this);
        playerUIManager.UpdateDisplayElixirCount(elixir);

        //コンポーネント取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        //武器の当たり判定オフ
        weaponCollider.enabled = false;
        weaponCollider2.enabled = false;

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

        x = 0;
        z = 0;

        if (playerState != PlayerState.Attack)
        {
            //前後左右移動入力
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            //斜め移動速度超過制限
            var moveVector = new Vector3(x, 0, z);
            if (moveVector.magnitude > 1)
            {
                moveVector.Normalize();
            }
            
        }
        

        //通常攻撃アクション入力
        if (Input.GetButtonDown("Fire1"))
        {
            //移動制限
            playerState = PlayerState.Attack;
            rb.velocity = Vector3.zero;
            LookAtTarget();
            animator.SetTrigger("Attack");
        }

        //特殊攻撃アクション入力 SP消費行動
        if (Input.GetButtonDown("Fire2"))
        {

            if (sp >= 500)
            {
                
                if(sp >= 1000)
                {
                    //移動制限
                    playerState = PlayerState.Attack;
                    playerUIManager.UpdateSP(sp);
                    rb.velocity = Vector3.zero;
                    LookAtTarget();
                    animator.SetTrigger("Iai");
                    animator.SetTrigger("Attack_H");
                }
                else
                {
                    //移動制限
                    playerState = PlayerState.Attack;
                    playerUIManager.UpdateSP(sp);
                    rb.velocity = Vector3.zero;
                    LookAtTarget();
                    animator.SetTrigger("Attack_H");
                }
            }
            
        }

        //パリィアクション入力 SP消費行動
        if (Input.GetButtonDown("Fire3"))
        {
            if (sp >= 1100)
            {
                //移動制限
                playerState = PlayerState.Attack;
                playerUIManager.UpdateSP(sp);
                rb.velocity = Vector3.zero;
                LookAtTarget();
                animator.SetTrigger("Parry");
            }
            
        }

        //回避アクション入力 SP消費行動
        if (Input.GetButtonDown("Jump"))
        {
            if (sp >= 300)
            {
                //移動制限
                playerState = PlayerState.Attack;
                playerUIManager.UpdateSP(sp);
                rb.velocity = Vector3.zero;
                //Vector3 direction = transform.position + new Vector3(x, 0, z) * moveSpeed;
                //transform.LookAt(direction);
                animator.SetTrigger("Dodge");
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

        //お薬使用
        if (Input.GetButtonDown("elixir"))
        {
            if (elixir > 0 && hp < maxHp)
            {
                //移動制限
                playerState = PlayerState.Attack;
                rb.velocity = Vector3.zero;
                animator.SetTrigger("Elixir");
                elixir--;
                ElixirLabel.text = "" + elixir;

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

        //食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる
        if (other.gameObject.TryGetComponent(out Damager damager))
        {
            //食らいモーション再生
            animator.SetTrigger("Attacked");

            //移動制限
            playerState = PlayerState.Attack;


            //無敵時間発生
            StartCoroutine(InvTime());

            //プレイヤー位置を初期化
            rb.velocity = Vector3.zero;

            // 自分の位置と接触してきたオブジェクトの位置とを計算して、距離と方向を出して正規化(速度ベクトルを算出)
            Vector3 distination = (transform.position - other.transform.position).normalized;

            //ノックバック距離
            rb.AddForce(distination * knockBackPower, ForceMode.VelocityChange);

            //エフェクト再生とダメージ値をＨＰに反映
            AudioSource.PlayClipAtPoint(dmageSE, transform.position);

            GenerateEffect(other.gameObject);
            Damage(damager.damage);

        }
    }

    //食らい中無敵
    private IEnumerator InvTime()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        yield return new WaitForSeconds(0.7f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    //ダメージ管理
    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            x = 0;
            z = 0;
            rb.velocity = Vector3.zero;
            animator.SetTrigger("Dead");

        }
        playerUIManager.UpdateHP(hp);
        Debug.Log("Player残りHP：" + hp);

    }

    //プレイヤー死亡時
    public void DeadEnd()
    {
        //やられボイス再生
        AudioSource.PlayClipAtPoint(voiceSE2, Camera.main.transform.position);
        animator.GetComponent<PlayerController>().rakumei.SetActive(true);
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

    //簡易ロックオン
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

    //通常攻撃移動用
    public void AttackMove()
    {
        rb.velocity = Vector3.zero;
        transform.DOLocalMove(transform.forward * 0.3f, 0.2f).SetRelative();
        rb.velocity = Vector3.zero;
    }

    //居合い移動用
    public void IaiMove()
    {
        rb.velocity = Vector3.zero;
        transform.DOLocalMove(transform.forward * 3f, 0.3f).SetRelative();
        rb.velocity = Vector3.zero;
    }

    //残月移動用
    public void ZangetsuMove()
    {
        rb.velocity = Vector3.zero;
        transform.DOLocalMove(transform.forward * 1.1f, 0.3f).SetRelative();
        rb.velocity = Vector3.zero;
    }


    //回避移動用
    public void DodgeMove()
    {
        rb.velocity = Vector3.zero;
        transform.DOLocalMove(transform.forward * 3f, 0.5f).SetRelative();
        //無敵時間発生
        StartCoroutine(InvTime());
        rb.velocity = Vector3.zero;
    }

    //吹き飛ばしテスト
    public void KickHit()
    {
        //吹き飛ばし距離
        rb.AddForce(Vector3.forward * hitBackPower, ForceMode.Impulse);
    }



    public void GenerateEffect(GameObject other)
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}

