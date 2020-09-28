using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //追跡（攻撃）対象
    public Transform target;
    NavMeshAgent agent;
    Animator animator;

    //食らい判定用
    public Collider weaponCollider;
    Rigidbody rb;
    public float knockBackPower;

    //ステータス設定
    public int maxHp;
    public int hp;
    public EnemyUIManager enemyUIManager;

    //死亡判定、死亡カウント用
    GameObject destroyEnemyNum;
    bool isDead = false;

    //HIT、死亡エフェクト用
    public GameObject effectPrefab;
    public GameObject effectPrefab2;

    //被ダメージSE
    public AudioClip dmageSE;

    //乱数用
    public float randomAttack;

    void Start()
    {

        destroyEnemyNum = GameObject.Find("StageManager");

        //タグでゲームオブジェクトを検索
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Transform型に変換
        target = player.transform;
        
        hp = maxHp;
        enemyUIManager.Init(this);

        //コンポーネント格納
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        //行き先＝destination
        agent.destination = target.position;

        //武器の当たり判定オフ
        WeaponColOFF();

    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (target != null)
        {
            //ターゲットへ向かって移動
            agent.destination = target.position;

            //目的地までの距離が保存されているremainingDistance
            animator.SetFloat("Distance", agent.remainingDistance);

            if (agent.remainingDistance < 1.5f)
            {
                RandomAttackIndex();
            }
                        
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }

        //敵の食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる
        if (other.gameObject.TryGetComponent(out Damager damager) && (other.CompareTag("Weapon")))
        {
            //
            float culs = agent.speed;
            agent.speed = 0f;

            //食らいモーション再生
            animator.SetTrigger("Attacked");

            //位置を初期化
            rb.velocity = Vector3.zero;

            //ヒットバック
            // 自分の位置と接触してきたオブジェクトの位置とを計算して、距離と方向を出して正規化(速度ベクトルを算出)
            Vector3 distination = (transform.position - other.transform.position).normalized;

            if (damager.isKnokcBack == true)
            {
                knockBackPower *= 1f;
            }

            //ノックバック距離
            rb.AddForce(distination * knockBackPower, ForceMode.VelocityChange);
            
            //エフェクト再生
            GenerateEffect(other.gameObject);
            AudioSource.PlayClipAtPoint(dmageSE, transform.position);

            //ダメージ更新
            Damage(damager.damage);

            //
            agent.speed = culs;

        }
    }

    /// <summary>
    /// ダメージ管理
    /// </summary>
    /// <param name="damage">ダメージ値</param>
    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            agent.speed = 0f;
            hp = 0;
            animator.SetTrigger("Dead");
        }
        enemyUIManager.UpdateHP(hp);
    }


    /// <summary>
    /// Enemy削除、撃破カウント追加
    /// </summary>
    public void EnemyDestroy()
    {
        destroyEnemyNum.GetComponent<StageManager>().DestroyEnemyNum();
        transform.DOScale(new Vector3(0, 0, 0) ,1.5f);
        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// 武器の攻撃判定オン
    /// </summary>
    public void WeaponColON()
    {
        weaponCollider.enabled = true;
    }

    /// <summary>
    /// 武器の攻撃判定オフ
    /// </summary>
    public void WeaponColOFF()
    {
        weaponCollider.enabled = false;
    }

    /// <summary>
    /// 簡易ロックオン
    /// </summary>
    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    /// <summary>
    /// 食らいエフェクトを生成する
    /// </summary>
    /// <param name="other">当たった位置</param>
    public void GenerateEffect(GameObject other)
    {
        GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

    /// <summary>
    /// 死亡エフェクトを生成する
    /// </summary>
    /// <param name="other">死んだ場所</param>
    public void GenerateEffect2(GameObject other)
    {
        GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
        Destroy(effect2, 0.5f);

    }

    /// <summary>
    /// 乱数生成＠攻撃アニメーションランダム再生
    /// </summary>
    public void RandomAttackIndex()
    {
        randomAttack = Random.value;
        animator.SetFloat("RandomAttackIndex", randomAttack);
        Debug.Log("乱数 : " + randomAttack);
    }
}