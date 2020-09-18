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

    //ステータス設定
    public int maxHp = 100;
    public int hp;
    public EnemyUIManager enemyUIManager;

    //死亡判定
    bool isDead = false;

    //HIT、死亡エフェクト用
    public GameObject effectPrefab;
    public GameObject effectPrefab2;

    void Start()
    {
        hp = maxHp;
        enemyUIManager.Init(this);

        //コンポーネント格納
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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
            //Debug.Log(agent.remainingDistance);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //敵の食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる

        /*Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {

            animator.SetTrigger("Attacked");
            GenerateEffect(gameObject);
        }*/

        //敵の食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる
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
            isDead = true;
            //Enemy撃破時の処理
            agent.speed = 0f;
            hp = 0;
            animator.SetTrigger("Dead");
        }
        enemyUIManager.UpdateHP(hp);
    }

    //Enemy削除用
    public void EnemyDestroy()
    {
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
        Destroy(effect.gameObject, 1f);
    }

    public void GenerateEffect2(GameObject other)
    {
        //死亡エフェクトを生成する
        GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
        Destroy(effect2.gameObject, 0.5f);

    }
}