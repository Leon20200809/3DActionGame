using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // タグ指定用
    string weapon = "Weapon";

    [Header("現在のHP")]
    public int hp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        // Weaponタグ以外は当たり判定として判定しない
        if (col.gameObject.tag != weapon)
        {
            return;
        }

        // AttackPowerを取得したいため、まずはWeaponゲームオブジェクトのWeaponControllerから、PlayerControllerを取得
        PlayerControllerFX playerController = col.gameObject.GetComponent<WeaponController>().PlayerControllerFX;

        // PlayerControllerクラスが変数に代入できているか確認
        Debug.Log(playerController);

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
}
