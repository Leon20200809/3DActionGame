using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIManager : MonoBehaviour
{
    //HP情報取得用
    public Slider hpSlider;
    public Slider spSlider;

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

    //初期化
    public void Init(PlayerController playerManager)
    {
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;
        spSlider.maxValue = playerManager.maxSp;
        spSlider.value = playerManager.maxSp;
    }
}
