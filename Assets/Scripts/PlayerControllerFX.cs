using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFX : MonoBehaviour
{
    [Header("現在のHP")]
    public int hp;

    [Header("移動速度")]
    public float moveSpeed;

    [Header("回転速度")]
    public float rotateSpeed;

    [Header("ジャンプ力")]
    public float jumpPower;

    [Header("地面判定用レイヤー")]
    public LayerMask groundLayer;

    public bool isGround;

    [Header("攻撃力")]
    public int attackPower;

    [Header("クリティカルの発生確率")]
    public int criticalSuccessRate;

    [Header("クリティカル時の攻撃力倍率")]
    public float criticalMultiplier;

    // タグ指定用
    string enemyweapon = "EnemyWeapon";

    //クラス宣言
    Rigidbody rb;
    Animator anim;
    public WeaponController weaponController;
    public EnemyWeaponController enemyWeaponController;
    public AudioClip weaponSound;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jumpメソッドを呼び出す
        Jump();

        //Attackメソッドを呼び出す
        Attack();
    }

    void FixedUpdate()
    {
        // 攻撃アニメ再生中は、以下の処理しない
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

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
    /// <summary> 
	/// ジャンプ
	/// </summary>
    void Jump()
    {
        //  Linecastでキャラの足元に地面があるか判定  地面があるときはTrueを返す
        isGround = Physics.Linecast(transform.position + transform.up * 1, transform.position - transform.up * 0.3f, groundLayer);

        //  着地していたとき、キー入力のJumpで反応（GetButton）スペースキー(GetKey)
        if (Input.GetButtonDown("Jump") && isGround)
        {
            //  着地判定をfalse
            isGround = false;

            //  Jumpステートへ遷移してジャンプアニメを再生
            anim.Play("Jump");

            //  AddForceにて上方向へ力を加える
            rb.AddForce(Vector3.up * jumpPower);
        }

    }
    /// <summary> 
    /// 通常攻撃
    /// </summary>
    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("Attack");
            AudioSource.PlayClipAtPoint(weaponSound, transform.position);
            weaponController.ActivateWeaponCollider(true);
            Debug.Log("敵に対して " + attackPower + " のダメージを与える");
            
        }
    }
    //食らい判定
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);
        // EnemyWeaponタグ以外は当たり判定として判定しない
        if (col.gameObject.name != enemyweapon)
        {
            return;
        }
        
        // AttackPowerを取得したいため、まずはWeaponゲームオブジェクトのWeaponControllerから、PlayerControllerを取得
        EnemyController enemyController = col.gameObject.GetComponent<EnemyWeaponController>().enemyController;
        
        // EnemyControllerクラスが変数に代入できているか確認
        Debug.Log(enemyController);

        // EnemyControllerのAttackPowerをHPから減算
        hp -= enemyController.attackPower;
        Debug.Log("残HP : " + hp);

        // 残りHPの確認
        if (hp <= 0)
        {
            Debug.Log("落命");
            Destroy(gameObject);
        }
    }
    void HIt()
    {
        
    }

    // Player側にクリティカルの判定を持たせる場合のメソッド例(EnemyController側で呼び出す)
    public int SetDamage()
    {
        // ダメージ決定用の変数。この値を戻り値にする
        int damage;

        // クリティカル判定用の値を取得
        int criticalValue = Random.Range(0, 100);

        // クリティカル判定
        if (criticalValue <= criticalSuccessRate)
        {
            // クリティカルした場合には、攻撃力にクリティカル倍率をかけた値をdamageとする
            damage = Mathf.CeilToInt(attackPower * criticalMultiplier);    // (int)  attackPower = int * criticalMultiplier = float 計算結果はfloatになるため、intに型変換するしながら小数点使者五洲(単純なintキャストの場合は小数点切り捨て)
        }
        else
        {
            // クリティカルしない場合には、攻撃力をそのままdamageとする
            damage = attackPower;
        }
        // damageを戻り値として戻す
        return damage;
    }

}
