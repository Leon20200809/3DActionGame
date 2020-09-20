using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class TitelBackEsc : MonoBehaviour
{
   

    public void OnStartButton()
    {
        //コルーチンメソッド呼び出し
        StartCoroutine(Kankaku());
    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Wait");

        //タイトルシーンへ移行
        SceneManager.LoadScene("Title");
    }
}
