using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFX : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed;

    [Header("回転速度")]
    public float rotateSpeed;

    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //キー入力
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Moveメソッドを呼び出す
        Move(x, z);

    }
    ///<summary>
    ///プレイヤーを移動する
    /// </summary>
    /// <param name="x">X軸の移動値</param>
    /// <param name="z">Z軸の移動値</param>
    void Move(float x, float z)
    {
        // 入力値を正規化( normalized データを扱いやすくすること)
        Vector3 moveDir = new Vector3(x, 0, z).normalized;

        // RigidbodyのAddforceメソッドを利用して移動
        rb.AddForce(moveDir * moveSpeed);

        //アニメーション部分
        if (moveDir != Vector3.zero)
        {
            // 移動中か判定する。移動中なら
            anim.SetFloat("Speed", 0.8f);
        }
        else
        {
            // 停止中なら
            anim.SetFloat("Speed", 0.0f);
        }

        //移動に合わせて向きを変える
        LookDirection(moveDir);
        
    }
    /// <summary>
    /// Plsyerの向きを変える
    /// </summary>
    /// <param name="dir"></param>
    void LookDirection(Vector3 dir)
    {
        // ベクトル(向きと大きさ)の2乗の長さをfloatで戻す = Playerが移動しているかどうかの確認
        if (dir.sqrMagnitude <= 0f)
        {
            return;
        }
        // 「球面線形補間」Slerp(始まりの位置, 終わりの位置, 時間）　なめらかに回転する
        Vector3 forward = Vector3.Slerp(transform.forward, dir, rotateSpeed * Time.deltaTime);

        // 引数はVector3　Playerの向きを、自分を中心に変える
        transform.LookAt(transform.position + forward);
    }

}
