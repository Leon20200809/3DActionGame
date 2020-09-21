using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EscPouUp : MonoBehaviour
{
    public GameObject tutorialCanvas;

    // Start is called before the first frame update
    void Start()
    {
        tutorialCanvas = GameObject.Find("TutorialCanvas");
        tutorialCanvas.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            tutorialCanvas.SetActive(true);
            // ゲーム内時間の流れを停止
            Time.timeScale = 0;

        }
    }
}
