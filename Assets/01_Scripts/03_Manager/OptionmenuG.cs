using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionmenuG : MonoBehaviour
{
    //スライダー取得用
    public Slider mouseSensitivitySlider;

    //スライダー数値
    float maxMs = 1200f;
    float minMs = 200f;
    float nowMs = DataSender.mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        //スライダー最大値
        mouseSensitivitySlider.maxValue = maxMs;
        //スライダー最小値
        mouseSensitivitySlider.minValue = minMs;

        mouseSensitivitySlider.value = nowMs;
    }

    public void Hanei()
    {
        //デフォルト設定値
        DataSender.mouseSensitivity = mouseSensitivitySlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DataSender.mouseSensitivity);
    }
}
