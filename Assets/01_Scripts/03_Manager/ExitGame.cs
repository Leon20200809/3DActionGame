using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class ExitGame : MonoBehaviour
{
    public AudioClip Exitvoice;
    public Transform canvasTransform;
    public Transform exitBtnTransform;
    public GameObject particle;


    public void OnStartButton()
    {
        AudioSource.PlayClipAtPoint(Exitvoice, Camera.main.transform.position);

        Instantiate(particle, canvasTransform, false);

        //DoTween 大きさを1.5倍に0.1秒かけて0.1秒維持して元に戻す
        Sequence sequence = DOTween.Sequence();
        sequence.Append(exitBtnTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f));
        sequence.AppendInterval(0.1f);
        sequence.Append(exitBtnTransform.DOScale(Vector3.one, 0.1f));


        //コルーチンメソッド呼び出し
        StartCoroutine(Kankaku());

    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(2.6f);
        Debug.Log("Wait");

        QuitGame();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// ゲームの終了処理(staticメソッドにしておくことで、終了確認用ポップアップからも呼び出せる)
    /// </summary>
    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}

