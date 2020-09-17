using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h, v;

    Animator anim;

    void Start()
    {

        anim = GetComponent<Animator>();
    }

    void Update()
    {


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (v != 0f || h != 0f)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

    }

}
