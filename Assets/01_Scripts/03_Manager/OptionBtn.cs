using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class OptionBtn : MonoBehaviour
{
    public AudioClip startvoice;
    public GameObject setsumei;
    public Transform canvasTransform;
    public Transform optionBtnTransform;

    public void OnStartButton()
    {
        AudioSource.PlayClipAtPoint(startvoice, Camera.main.transform.position);
        //Instantiate(particle, canvasTransform, false);

        //DoTween 大きさを1.5倍に0.1秒かけて0.1秒維持して元に戻す
        Sequence sequence = DOTween.Sequence();
        sequence.Append(optionBtnTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f));
        sequence.AppendInterval(0.1f);
        sequence.Append(optionBtnTransform.DOScale(Vector3.one, 0.1f));

        //コルーチンメソッド呼び出し
        StartCoroutine(Kankaku());

    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Wait");

        //操作説明イメージ再生
        setsumei.SetActive(true);

    }

    public void OnCloseButton()
    {
        setsumei.SetActive(false);

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
