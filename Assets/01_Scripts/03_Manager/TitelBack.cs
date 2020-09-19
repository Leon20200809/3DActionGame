using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class TitelBack : MonoBehaviour
{
    public AudioClip reStartvoice;

    public void OnStartButton()
    {
        AudioSource.PlayClipAtPoint(reStartvoice, gameObject.transform.position);

        //コルーチンメソッド呼び出し
        StartCoroutine(Kankaku());
    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Wait");

        //タイトルシーンへ移行
        SceneManager.LoadScene("Title");
    }
}
