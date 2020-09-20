using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackedBehaviour : StateMachineBehaviour
{
    //効果音設定用
    public AudioClip dmageSE;
    public AudioClip dmageVoice;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //食らい判定トリガーリセット
        //animator.ResetTrigger("Attacked");

        //攻撃判定トリガーリセット
        animator.ResetTrigger("Attack");

        //Ｈ攻撃判定トリガーリセット
        //animator.ResetTrigger("Attack_H");

        //攻撃判定オフ
        animator.GetComponent<PlayerController>().WeaponColOFF();

        //武器の軌跡エフェクトオフ
        animator.GetComponent<PlayerController>().TrailRendOFF();

        //食らいモーション中は移動速度を0.5ｆにする
        animator.GetComponent<PlayerController>().moveSpeed = 0f;

        //ダメージSE、ボイス再生
        AudioSource.PlayClipAtPoint(dmageSE, animator.gameObject.transform.position);
        AudioSource.PlayClipAtPoint(dmageVoice, animator.gameObject.transform.position);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerController>().moveSpeed = 6;
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
