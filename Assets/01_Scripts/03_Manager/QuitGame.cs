using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuitGame : MonoBehaviour
{
    public AudioClip Exitvoice;

    public void OnStartButton()
    {
        AudioSource.PlayClipAtPoint(Exitvoice, gameObject.transform.position);

        //コルーチンメソッド呼び出し
        StartCoroutine(Kankaku());
    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Wait");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();                                
#endif

    }
}
