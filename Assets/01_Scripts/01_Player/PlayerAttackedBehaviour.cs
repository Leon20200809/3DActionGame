using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackedBehaviour : StateMachineBehaviour
{
    //効果音設定用
    public AudioClip dmageSE;
    public AudioClip dmageVoice;

    private PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // PlayerControllerを取得していない場合には取得する
        if (playerController == null)
        {
            playerController = animator.gameObject.GetComponent<PlayerController>();
        }

        //攻撃判定トリガーリセット
        animator.ResetTrigger("Attack");

        //Ｈ攻撃判定トリガーリセット
        //animator.ResetTrigger("Attack_H");

        //攻撃判定オフ
        playerController.WeaponColOFF();

        //武器の軌跡エフェクトオフ
        playerController.TrailRendOFF();

        //ダメージSE、ボイス再生
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
