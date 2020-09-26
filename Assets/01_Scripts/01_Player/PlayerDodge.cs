using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : StateMachineBehaviour
{
    public AudioClip voiceSE;
    private PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        {
            // PlayerControllerを取得していない場合には取得する
            if (playerController == null)
            {
                playerController = animator.gameObject.GetComponent<PlayerController>();
            }

            //現在のSPからモーションに応じてSPを減らす
            playerController.sp -= 300;

            //攻撃判定オフ
            playerController.WeaponColOFF();

            AudioSource.PlayClipAtPoint(voiceSE, animator.gameObject.transform.position);

            playerController.playerState = PlayerController.PlayerState.Attack;

        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.playerState = PlayerController.PlayerState.Normal;

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
