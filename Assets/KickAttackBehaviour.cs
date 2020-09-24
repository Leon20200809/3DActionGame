﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAttackBehaviour : StateMachineBehaviour
{
    public AudioClip weaponSE;
    public AudioClip voiceSE;
    public AudioClip hitSE;

    private PlayerController playerController;
    Collider kickCollider;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource.PlayClipAtPoint(weaponSE, animator.gameObject.transform.position);
        AudioSource.PlayClipAtPoint(voiceSE, animator.gameObject.transform.position);

        // PlayerControllerを取得していない場合には取得する
        if (playerController == null)
        {
            playerController = animator.gameObject.GetComponent<PlayerController>();

        }
    }   

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("当たった？");
        }
    }

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
