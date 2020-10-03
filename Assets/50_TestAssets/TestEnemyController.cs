﻿using DG.Tweening;
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

    //パリィ成功時専用コライダー
    public Collider parryedCollider;

    Rigidbody rb;
    public float knockBackPower;

    public bool isFatal = false;

    //ステータス設定
    //public int maxHp;
    //public int hp;
    //public EnemyUIManager enemyUIManager;

    //死亡判定、死亡カウント用
    //GameObject destroyEnemyNum;
    //bool isDead = false;

    //HIT、死亡エフェクト用
    public GameObject effectPrefab;
    public Vector3 effecOfset;
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
        if (other.gameObject.TryGetComponent(out PlayerDamager damager) && other.CompareTag("PlayerWeapon") || other.CompareTag("Kick"))
        {
            if (isFatal == true)
            {
                IsFatalAttacked(other);

            }

            else if (other.CompareTag("Kick"))
            {
                Debug.Log("蹴りＨＩＴ");
                StartCoroutine(attackHitStop(0.1f));
                //
                float culs = agent.speed;
                agent.speed = 0f;

                //食らいモーション再生
                animator.SetTrigger("Down");

                //エフェクト再生
                GenerateEffect(other.gameObject);
                AudioSource.PlayClipAtPoint(dmageSE, transform.position);

                //ダメージ更新
                //Damage(damager.damage);

                //
                agent.speed = culs;

            }

            else
            {
                Debug.Log("通常攻撃ＨＩＴ");
                StartCoroutine(attackHitStop(0.1f));
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


    void OnTriggerStay(Collider parryedCollider)
    {
        //敵の食らい判定 Damagerスクリプトを持つゲームオブジェクトにぶつかる
        if (parryedCollider.gameObject.TryGetComponent(out PlayerDamager damager) && (parryedCollider.CompareTag("PlayerWeapon")))
        {
             //食らいモーション再生
            animator.SetTrigger("Kumiuchi");
        }
    }

    public void GenerateEffect(GameObject other)
    {
        //食らいエフェクトを生成する
        GameObject effect = Instantiate(effectPrefab, transform.position + effecOfset, transform.rotation);
        Destroy(effect, 2f);
    }


    /// <summary>
    /// ダメージ管理
    /// </summary>
    /// <param name="damage">ダメージ値</param>

    
 

    /// <summary>
    /// パリィ専用コライダーオン
    /// </summary>
    public void ParryedColON()
    {
        parryedCollider.enabled = true;
    }

    /// <summary>
    /// パリィ専用コライダーオフ
    /// </summary>
    public void ParryedColOFF()
    {
        parryedCollider.enabled = false;
    }
    /// <summary>
    /// 武器コライダーオン
    /// </summary>
    public void WeaponColON()
    {
        weaponCollider.enabled = true;
    }

    /// <summary>
    /// 武器コライダーオフ
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
    /// 被致命攻撃
    /// </summary>
    void IsFatalAttacked(Collider other)
    {
        animator.SetTrigger("Kumiuchi");
        isFatal = false;
        StartCoroutine(attackHitStop(0.1f));
        GenerateEffect(other.gameObject);
        AudioSource.PlayClipAtPoint(dmageSE, transform.position);

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