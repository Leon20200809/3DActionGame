using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EscPouUp : MonoBehaviour
{
    public EscCheckPouUp escCheckPouUp = null;
    public Transform canvasTran;

    void Update()
    {
        //ゲーム中断用ポップアップ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc押した！");
            if (escCheckPouUp == null)
            {
                Debug.Log("Esc押した22！");

                escCheckPouUp = Instantiate(escCheckPouUp, canvasTran, false);
            }
        }
    }

}
