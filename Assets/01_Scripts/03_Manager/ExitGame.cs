using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class ExitGame : MonoBehaviour
{
    public void OnStartButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   
        #else
        Application.Quit();                                
        #endif
    }

    public QuitCheckPouUp quitCheckPouUpPrefab;    // ポップアップのプレファブ用変数。名前にプレファブと付けることをおすすめします。
    private QuitCheckPouUp quitCheckPouUp = null;    //生成したポップアップを代入する変数を宣言フィールドに追加
    public Transform canvasTran;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //ポップアップが生成されていない(変数が空)なら
            if (quitCheckPouUp == null)
            {
                //（ポップアップが生成されていない場合だけ）終了確認用のポップアップを生成して変数にいれる
                quitCheckPouUp = Instantiate(quitCheckPouUpPrefab, canvasTran, false);
                // 変数に代入する処理を追加。生成＝値が入る。空ではなくなる


                //Debug.Log(quitCheckPouUp);
            }



        }
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

