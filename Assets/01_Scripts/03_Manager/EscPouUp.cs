using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EscPouUp : MonoBehaviour
{
    public QuitCheckPouUp quitCheckPouUpPrefab;  
    private QuitCheckPouUp quitCheckPouUp = null; 
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
}
