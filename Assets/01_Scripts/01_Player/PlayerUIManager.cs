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
    public Text txtElixir;

    //HP更新
    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }

    /// <summary>
    /// 現SPを更新しＵＩへ反映
    /// </summary>
    /// <param name="sp"></param>
    public void UpdateSP(int sp)
    {
        spSlider.DOValue(sp, 0.5f);
    }

    /// <summary>
    /// プレイヤーステータスの更新
    /// </summary>
    /// <param name="playerManager">プレイヤーマネージャー</param>
    public void Init(PlayerController playerManager)
    {
        //HP,SP
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;
        spSlider.maxValue = playerManager.maxSp;
        spSlider.value = playerManager.maxSp;

    }

    ///<summary>
    ///お薬所持数の更新
    ///</summary>
    ///<param name="elixirCount">薬の所持数</param>
    ///
    public void UpdateDisplayElixirCount(int elixirCount)
    {
        txtElixir.text = elixirCount.ToString();
    }

}
