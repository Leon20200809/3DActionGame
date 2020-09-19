using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialPouUp : MonoBehaviour
{
    public GameObject startCanvas;
    public void OnStartButton()
    {
        // ゲーム内時間の流れを再開する
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        startCanvas = GameObject.Find("StartCanvas");
        startCanvas.GetComponent<Canvas>(); 


    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム内時間の流れを停止
        Time.timeScale = 0;
      
    }
}
