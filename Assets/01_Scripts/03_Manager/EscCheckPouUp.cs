using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;

public class EscCheckPouUp : MonoBehaviour
{
    public Button btnQuitGame;        // タイトルに戻るボタン
    public Button btnClosePopup;      // ポップアップを閉じてゲームに戻るボタン

    void Start()
    {
        // 各ボタンに処理を登録
        btnQuitGame.onClick.AddListener(GoTitleButton);
        btnClosePopup.onClick.AddListener(OnClickClosePopUp);

        // ゲーム内時間の流れを停止
        Time.timeScale = 0;
    }

    /// <summary>
    /// ポップアップを閉じてゲームに戻る
    /// </summary>
    private void OnClickClosePopUp()
    {
        // ゲーム内時間の流れを再開する
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }

    void GoTitleButton()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(Kankaku());
        
    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(1f);
        Debug.Log("Wait");

        //タイトルシーンへ移行
        SceneManager.LoadScene("Title");

    }

}
