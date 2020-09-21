using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class TutorialPouUpEsc : MonoBehaviour
{
    public GameObject tutorialCanvas;

    void Start()
    {
        tutorialCanvas = GameObject.Find("TutorialCanvas");
        //tutorialCanvas.GetComponent<Canvas>();
    }

    public void OnStartButton()
    {
        // ゲーム内時間の流れを再開
        Time.timeScale = 1;
        tutorialCanvas.SetActive(false);
    }


}
