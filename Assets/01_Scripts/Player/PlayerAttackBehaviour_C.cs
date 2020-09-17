using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour_C : StateMachineBehaviour
{
    public AudioClip weaponSE;
    public AudioClip voiceSE;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //攻撃中は移動速度を0.5ｆにする
        animator.GetComponent<PlayerController>().moveSpeed = 0.5f;
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
        animator.GetComponent<PlayerController>().moveSpeed = 8f;
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
