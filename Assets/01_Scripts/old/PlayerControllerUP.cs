using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUP : MonoBehaviour
{
	private CharacterController characterController;
	private Animator animator;
	private Vector3 velocity;
	[SerializeField]
	private float walkSpeed = 1.5f;

	// Use this for initialization
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		velocity = Vector3.zero;
	}

	// Update is called once per frame
	void Update()
	{

		if (characterController.isGrounded)
		{
			velocity = Vector3.zero;

			if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

				if (input.magnitude > 0f)
				{
					transform.LookAt(transform.position + input);
					animator.SetFloat("Speed", input.magnitude);
					velocity = input * walkSpeed;
				}
				else
				{
					animator.SetFloat("Speed", 0f);
				}
			}
		}

		//　モーションの移動値で地面から離れる可能性がある為、接地条件外で判別
		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack");
		}
		//　Attackタグに遷移した時は移動値を重力だけにする	
		if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
		{
			velocity = new Vector3(0f, velocity.y, 0f);
		}

		velocity.y += Physics.gravity.y * Time.deltaTime;
		characterController.Move(velocity * Time.deltaTime);
	}

}
