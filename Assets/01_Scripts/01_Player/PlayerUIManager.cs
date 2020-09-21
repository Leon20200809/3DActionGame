using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    //情報取得用
    public Slider hpSlider;
    public Slider spSlider;
    public Text elxirNum;
    public PlayerController playerController;

    //HP更新
    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }

    //SP更新
    public void UpdateSP(int sp)
    {
        spSlider.DOValue(sp, 0.5f);
    }

    //お薬更新
    public void UpdateElxir()
    {
        elxirNum.ToString();
    }

    //初期化
    public void Init(PlayerController playerManager)
    {
        //HP,SP
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;
        spSlider.maxValue = playerManager.maxSp;
        spSlider.value = playerManager.maxSp;

        //お薬所持数
        //elxirNum.text = playerController.maxElixir.ToString();
    }

}
