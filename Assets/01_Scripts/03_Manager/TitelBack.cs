using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

// ボタンを押したらGameSceneへ遷移
public class TitelBack : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Title");
    }
}
