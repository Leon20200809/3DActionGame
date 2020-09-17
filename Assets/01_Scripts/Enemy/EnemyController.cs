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

    //HITエフェクト用
    public GameObject effectPrefab;

    void Start()
    {
        //コンポーネント格納
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        //行き先＝destination
        agent.destination = target.position;
        
    }

    void Update()
    {
        //ターゲットへ向かって移動
        agent.destination = target.position;

        //目的地までの距離が保存されているremainingDistance
        animator.SetFloat("Distance", agent.remainingDistance);
        //Debug.Log(agent.remainingDistance);
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

    //簡易ロックオン
    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    public void GenerateEffect(GameObject other)
    {
        //エフェクトを生成する
        GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
    }
}