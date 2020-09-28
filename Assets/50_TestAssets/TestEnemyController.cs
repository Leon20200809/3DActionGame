using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyController : MonoBehaviour
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
    //public int maxHp;
    //public int hp;
    //public EnemyUIManager enemyUIManager;

    //死亡判定、死亡カウント用
    //GameObject destroyEnemyNum;
    //bool isDead = false;

    //HIT、死亡エフェクト用
    public GameObject effectPrefab;
    //public GameObject effectPrefab2;

    //被ダメージSE
    public AudioClip dmageSE;

    //乱数用
    //public float randomAttack;

    void Start()
    {

        //destroyEnemyNum = GameObject.Find("StageManager");

        //タグでゲームオブジェクトを検索
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Transform型に変換
        target = player.transform;
        
        //hp = maxHp;

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
        if (target != null)
        {
            //ターゲットへ向かって移動
            agent.destination = target.position;

            //目的地までの距離が保存されているremainingDistance
            animator.SetFloat("Distance", agent.remainingDistance);

            /*if (agent.remainingDistance < 1.5f)
            {
                RandomAttackIndex();
            }*/
                        
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        //敵の食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる
        if (other.gameObject.TryGetComponent(out PlayerDamager damager) && (other.CompareTag("PlayerWeapon")))
        {
            //
            float culs = agent.speed;
            agent.speed = 0f;

            //食らいモーション再生
            animator.SetTrigger("Attacked");

            //エフェクト再生
            GenerateEffect(other.gameObject);
            AudioSource.PlayClipAtPoint(dmageSE, transform.position);

            //ダメージ更新
            //Damage(damager.damage);

            //
            agent.speed = culs;

        }
    }

    public void GenerateEffect(GameObject other)
    {
        //食らいエフェクトを生成する
        GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }


    /// <summary>
    /// ダメージ管理
    /// </summary>
    /// <param name="damage">ダメージ値</param>



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
    /// 乱数生成＠攻撃アニメーションランダム再生
    /// </summary>
    /*public void RandomAttackIndex()
    {
        randomAttack = Random.value;
        animator.SetFloat("RandomAttackIndex", randomAttack);
        Debug.Log("乱数 : " + randomAttack);
    }*/
}