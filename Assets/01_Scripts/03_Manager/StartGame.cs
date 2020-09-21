using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class StartGame : MonoBehaviour    
{
    public AudioClip startvoice;
    public GameObject particle;
    public Transform canvasTransform;
    public Transform startBtnTransform;

    public void OnStartButton()
    {
        AudioSource.PlayClipAtPoint(startvoice, Camera.main.transform.position);
        //Instantiate(particle, canvasTransform, false);

        //DoTween 大きさを1.5倍に0.1秒かけて0.1秒維持して元に戻す
        Sequence sequence = DOTween.Sequence();
        sequence.Append(startBtnTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f));
        sequence.AppendInterval(0.1f);
        sequence.Append(startBtnTransform.DOScale(Vector3.one, 0.1f));

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
