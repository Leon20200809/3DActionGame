using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class TestPlayerController : MonoBehaviour
{
    //移動系初期設定
    float x;
    float z;
    public float moveSpeed;
    public float knockBackPower;
    public float hitBackPower;

    //プレイヤー攻撃判定用
    //public Collider weaponCollider;
    //public Collider weaponCollider2;

    //プレイヤーの状態
    public enum PlayerState
    {
        Normal,
        Attack,
        Special,
    }
    public PlayerState playerState = PlayerState.Normal;

    //コンポーネント宣言
    Rigidbody rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
            //前後左右移動入力
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()  //演算処理はここに書く
    {
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

        //移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        //キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        //移動アニメーション開始
        animator.SetFloat("Speed", rb.velocity.magnitude);

    }
}
