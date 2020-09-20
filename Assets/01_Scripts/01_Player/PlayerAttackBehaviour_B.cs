using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour_B : StateMachineBehaviour
{
    public AudioClip weaponSE;
    public AudioClip voiceSE;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //このモーション中は攻撃力を100にする
        animator.gameObject.GetComponent<Damager>().damage = 100;
        AudioSource.PlayClipAtPoint(weaponSE, animator.gameObject.transform.position);
        AudioSource.PlayClipAtPoint(voiceSE, animator.gameObject.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.gameObject.GetComponent<Damager>().damage = 10;

        //食らい判定トリガーリセット
        animator.ResetTrigger("Attacked");
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
