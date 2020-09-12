using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public CapsuleCollider capsuleCollider;

    public PlayerControllerFX PlayerControllerFX;

    public float attackAnimeTime;

    public AudioClip weaponSound;

    // Start is called before the first frame update
    void Start()
    {
        // 構え中は当たり判定をオフにする
        capsuleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 武器のColliderのオン/オフ切り替え
    /// </summary>
    public void ActivateWeaponCollider(bool isSwitch)
    {
        AudioSource.PlayClipAtPoint(weaponSound, transform.position);
        capsuleCollider.enabled = isSwitch;
        StartCoroutine(AttackTime());
        Debug.Log(capsuleCollider);
        
    }

    /// <summary>
    /// 攻撃のアニメーションの時間に合わせてCollierを有効化
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackTime()    //反復処理をサポートするインターフェース
    {
        //attackAnimeTime秒数の間コルーチンの実行を待つ
        yield return new WaitForSeconds(attackAnimeTime);
        ActivateWeaponCollider(false);
    }
}
