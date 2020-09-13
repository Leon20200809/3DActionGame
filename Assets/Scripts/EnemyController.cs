using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // タグ指定用
    string weapon = "Weapon";

    [Header("現在のHP")]
    public int hp;

    [Header("クリティカルの発生確率")]
    public int criticalSuccessRate;

    [Header("クリティカル時の攻撃力倍率")]
    public float criticalMultiplier;

    [Header("移動速度の最小値")]
    public float minMoveSpeed;
    [Header("移動速度の最大値")]
    public float maxMoveSpeed;

    [Header("攻撃力の最小値")]
    public int minAttackPower;
    [Header("攻撃力の最大値")]
    public int maxAttackPower;

    // 適用する移動速度
    float moveSpeed;
    // 適用する攻撃力
    public int attackPower;

    public enum ENEMY_STATE
    {
        SET_UP,
        WAIT,
        MOVE,
        ATTACK,
        READY
    }
    public ENEMY_STATE enemyState;

    float actionTime;
    Rigidbody rb;
    Animator anim;
    public EnemyWeaponController enemyweaponController;
    public AudioClip weaponSound;

    //索敵範囲クラス
    public EnemySearchArea searchArea;

    // 索敵時の移動の目的地(Playerの位置情報)
    Vector3 destinationPos;

    // 移動する際の方向
    Vector3 direction;
    
    /// <summary>
    /// 敵のパラメータ(移動速度や攻撃力など)と状態を設定
    /// </summary>
    void SetUpEnemyParameter()
    {
        enemyState = ENEMY_STATE.SET_UP;
        

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        attackPower = Random.Range(minAttackPower, maxAttackPower);
        actionTime = Random.Range(0.1f, 0.5f);

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        enemyState = ENEMY_STATE.WAIT;

        Debug.Log(enemyState);
        //Debug.Log("MoveSpeed : " + moveSpeed);
        //Debug.Log("AttakPower : " + attackPower);
    }
    /// <summary>
    /// クリティカルの判定
    /// </summary>
    /// <returns></returns>
    bool JudgeCritialHit()
    {
        //判定用変数
        bool isCritical = false;

        // 乱数を１つ取得
        int randomValue = Random.Range(0, 100);

        // 乱数とクリティカルの確率を比べ、クリティカル発生確率よりも乱数の方が低いならクリティカルしたことにする
        if (randomValue <= criticalSuccessRate)
        {
            Debug.Log("Critical!!");
            return true;
        }
        return isCritical;
    }
    

    // 当たり判定
    void OnTriggerEnter(Collider col)
    {
        // Weaponタグ以外は当たり判定として判定しない
        if (col.gameObject.tag != weapon)
        {
            return;
        }

        // 攻撃力への初期倍率
        float attackPowerRate = 1.0f;

        // クリティカルの判定を戻り値で行う。Trueが戻ってきたらクリティカルした判定
        if (JudgeCritialHit())
        {
            // クリティカルした判定の場合、攻撃倍率を変更
            attackPowerRate = criticalMultiplier;
        }

        // AttackPowerを取得したいため、まずはWeaponゲームオブジェクトのWeaponControllerから、PlayerControllerを取得
        PlayerControllerFX playerController = col.gameObject.GetComponent<WeaponController>().PlayerControllerFX;

        // PlayerControllerクラスが変数に代入できているか確認
        Debug.Log(playerController);

        // 最終的な攻撃力を算出(小数点は切り捨て)　右辺はFloat型同士での計算式のため計算できるが、計算結果がFloatになるため、そのままだと左辺のInt型と型が合わない。
        // 左辺と合わせるために、計算後にInt型に明示的に型の変換をしている(これをキャストといいます)
        int damage = (int)Mathf.Floor(playerController.attackPower * attackPowerRate);
        Debug.Log(damage);

        // PlayerControllerのAttackPowerをHPから減算
        hp -= playerController.attackPower;
        Debug.Log("残りHP : " + hp);

        // 残りHPの確認
        if (hp <= 0)
        {
            Debug.Log("敵を倒した");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 移動の処理
    /// </summary>
    void Move()
    {
        rb.AddForce(-direction * moveSpeed);
        //Debug.Log(rb.velocity);
    }

    /// <summary>
    /// 攻撃の処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        anim.Play("Attack");
        AudioSource.PlayClipAtPoint(weaponSound, transform.position);
        enemyweaponController.ActivateEnemyWeaponCollider(true);
        Debug.Log("プレイヤーに対して " + attackPower + " のダメージを与える");
        yield return new WaitForSeconds(0.5f);
        NextWait();
    }

    
    /// <summary>
    /// 次の行動を確認する
    /// </summary>
    void CheckNextAction()
    {
        // 索敵範囲内にPlayerがいる場合
        if (searchArea.isSearching)
        {
            if (searchArea.player != null)
            {
                // Playerの位置まで移動して攻撃をする
                MoveAttack(searchArea.player);
                Debug.Log("移動して攻撃する");
            }
        }
        else
        {
            // 索敵範囲内にPlayerがいない場合、移動の準備をする
            PraparateMove();
            Debug.Log("移動準備");
        }
    }

    /// <summary>
    /// 索敵範囲内のPlayerの位置を目的地とし、移動する
    /// </summary>
    /// <param name="player"></param>
    void MoveAttack(GameObject player)
    {
        // 目的地を設定しておく
        destinationPos = player.transform.position;
        // 移動する方向を設定する
        direction = (transform.position - destinationPos).normalized;
        // 移動する方向を向く
        transform.LookAt(destinationPos);
        // UpdateのAttackの部分で移動させる
        enemyState = ENEMY_STATE.ATTACK;
        anim.SetBool("Move", true);
    }

    /// <summary>
    /// 移動の準備
    /// </summary>
    void PraparateMove()
    {
        // ランダムな方向を向く
        transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), new Vector3(0, 1, 0));
        direction = transform.position.normalized;
        enemyState = ENEMY_STATE.MOVE;
        actionTime = Random.Range(0.1f, 0.5f);
        anim.SetBool("Move", true);
    }

    /// <summary>
    /// 次回の行動までの待機
    /// </summary>
    /// <param name="nextState">ENEMY_STATE</param>
    void NextWait()
    {
        anim.SetBool("Move", false);
        // 待機
        actionTime = Random.Range(0.1f, 0.5f);
        enemyState = ENEMY_STATE.WAIT;
    }

    // Start is called before the first frame update
    void Start()
    {
        //敵キャラクターのステータス設定
        SetUpEnemyParameter();
    }

    // Update is called once per frame
    void Update()
    {
        // 準備状態。敵のパラメータの準備が終了していない場合にはUpdateを処理しない
        if (enemyState == ENEMY_STATE.SET_UP)
        {
            return;
        }

        // 行動用タイマーをカウントダウン
        actionTime -= Time.deltaTime;

        //待機状態
        if (enemyState == ENEMY_STATE.WAIT)
        {
            //Debug.Log("待機中 あと : " + actionTime + " 秒");
            if (actionTime <= 0)
            {
                CheckNextAction();
                Debug.Log("待機終了");
            }
        }

        //移動状態
        if (enemyState == ENEMY_STATE.MOVE)
        {
            Move();
            Debug.Log("移動中 あと : " + actionTime + " 秒");
            if (actionTime <= 0)
            {
                NextWait();
            }
        }

        // 索敵範囲内で目的地(Playerの位置)がある場合、目的地まで移動する
        if (enemyState == ENEMY_STATE.ATTACK)
        {
            //Debug.Log(Vector3.Distance(transform.position, destinationPos));
            if (Vector3.Distance(transform.position, destinationPos) > 0.8f)
            {
                Move();
                Debug.Log("移動攻撃中");

            }
            else
            {
                // 目的地に着いたら攻撃
                enemyState = ENEMY_STATE.READY;
                StartCoroutine(Attack());
                Debug.Log("攻撃");
                return;
            }
        }
    }

}
