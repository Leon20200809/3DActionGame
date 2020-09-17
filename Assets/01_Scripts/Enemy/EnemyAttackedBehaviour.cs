using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackedBehaviour : StateMachineBehaviour
{
    public AudioClip damageSE;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //食らい判定トリガーリセット
        animator.ResetTrigger("Attacked");

        //攻撃判定オフ
        animator.GetComponent<EnemyController>().WeaponColOFF();


        //攻撃中は移動速度を0ｆにする
        animator.GetComponent<NavMeshAgent>().speed = 0;
        AudioSource.PlayClipAtPoint(damageSE, animator.gameObject.transform.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //攻撃モーション終了後移動速度を戻す
        animator.GetComponent<NavMeshAgent>().speed = 2.5f;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
