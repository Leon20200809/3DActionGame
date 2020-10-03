using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optionmenu : MonoBehaviour
{
    //スライダー取得用
    public Slider mouseSensitivitySlider;

    //スライダー数値
    float maxMs = 1200f;
    float minMs = 200f;
    float nowMs = 700f;

    //PlayerPrefsで保存するためのキー
    string mouseSensitivityKey = "mouseSensitivity";

    private void Awake()
    {
        
    }

    public void Save()
    {
        //プレイヤーの変更したマウスセンシを保存
        PlayerPrefs.SetFloat(mouseSensitivityKey, DataSender.mouseSensitivity);
        PlayerPrefs.Save();
    }


    // Start is called before the first frame update
    void Start()
    {

        //スライダー最大値
        mouseSensitivitySlider.maxValue = maxMs;
        //スライダー最小値
        mouseSensitivitySlider.minValue = minMs;
        //デフォルト設定値
        mouseSensitivitySlider.value = nowMs;
        
    }

    public void Hanei()
    {
        DataSender.mouseSensitivity = nowMs;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DataSender.mouseSensitivity);
    }
}
