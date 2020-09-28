using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyController : MonoBehaviour
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
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }

        //敵の食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる
        if (other.gameObject.TryGetComponent(out Damager damager) && other.CompareTag("Weapon"))
        {
            //食らいモーション再生
            animator.SetTrigger("Attacked");

            //位置を初期化
            rb.velocity = Vector3.zero;

            //ヒットバック
            // 自分の位置と接触してきたオブジェクトの位置とを計算して、距離と方向を出して正規化(速度ベクトルを算出)
            Vector3 distination = (transform.position - other.transform.position).normalized;

            //ノックバック距離
            rb.AddForce(distination * knockBackPower, ForceMode.VelocityChange);
            

            //エフェクト再生
            GenerateEffect(other.gameObject);
            AudioSource.PlayClipAtPoint(dmageSE, transform.position);


            //ダメージ更新
            Damage(damager.damage); 
        }
    }


    /// <summary>
    /// ダメージ判定処理
    /// </summary>
    /// <param name="damage"></param>
    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            //Enemy撃破時の処理
            agent.speed = 0f;
            hp = 0;
            animator.SetTrigger("Dead");

        }
        enemyUIManager.UpdateHP(hp);
    }

    /// <summary>
    /// 敵撃破時の処理
    /// </summary>
    public void EnemyDestroy()
    {
        //撃破カウント追加
        destroyEnemyNum.GetComponent<StageManager>().DestroyEnemyNum();
        transform.DOScale(new Vector3(0, 0, 0) ,1.5f);
        Destroy(gameObject, 1.5f);
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

    //簡易ロックオン
    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    public void GenerateEffect(GameObject other)
    {
        //食らいエフェクトを生成する
        GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

    public void GenerateEffect2(GameObject other)
    {
        //死亡エフェクトを生成する
        GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
        Destroy(effect2, 5f);

    }
}