using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirBehaviour : StateMachineBehaviour
{
    public AudioClip elxirSE;
    public AudioClip voiceSE;
    public GameObject effectPrefab;

    private PlayerController playerController;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // PlayerControllerを取得していない場合には取得する
        if (playerController == null)
        {
            playerController = animator.gameObject.GetComponent<PlayerController>();
        }

        //攻撃判定オフ
        playerController.WeaponColOFF();

        AudioSource.PlayClipAtPoint(voiceSE, animator.gameObject.transform.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //HP回復
        playerController.hp += 600;
        if (playerController.hp >= playerController.maxHp)
        {
            playerController.hp = playerController.maxHp;
        }

        GameObject effect = Instantiate(effectPrefab, animator.gameObject.transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        playerController.playerUIManager.UpdateHP(playerController.hp);


        AudioSource.PlayClipAtPoint(elxirSE, animator.gameObject.transform.position);

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
