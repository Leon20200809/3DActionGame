using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class StartGame : MonoBehaviour    
{
    public AudioClip startvoice;

    public void OnStartButton()
    {
        AudioSource.PlayClipAtPoint(startvoice, gameObject.transform.position);

        //コルーチンメソッド呼び出し
        StartCoroutine(Kankaku());
    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(2.2f);
        Debug.Log("Wait");

        //ゲームシーンへ移行
        SceneManager.LoadScene("GameScene");

    }
}
