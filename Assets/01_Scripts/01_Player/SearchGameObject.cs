using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SearchGameObject : MonoBehaviour
{
    public string tagName = "Enemy";    // インスペクターで変更可能

    private GameObject nearObj;         // 最も近いオブジェクト
    private float searchTime = 0;       // 経過時間
    void Start()
    {
        //最も近かったオブジェクトを取得
        nearObj = serchTag(gameObject, tagName);
    }
    void Update()
    {

        //経過時間を取得
        searchTime += Time.deltaTime;

        if (searchTime >= 0.1f)
        {
            //最も近かったオブジェクトを取得
            nearObj = serchTag(gameObject, tagName);

            //経過時間を初期化して、再検索
            searchTime = 0;
        }
    }
    /// <summary>
    /// 指定されたタグの中で最も近いものを取得
    /// </summary>
    /// <param name="nowObj">自分</param>
    /// <param name="tagName">検索するタグ</param>
    /// <returns></returns>
    private GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;            // 距離用一時変数
        float nearDis = 0;           // 最も近いオブジェクトの距離
        GameObject targetObj = null; // 検索されたオブジェクト
        // タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            // 自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);
            // オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            // 一時変数に距離を格納。近くない場合には更新をしない
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                targetObj = obs;
            }
        }
        //最も近かったオブジェクトを返す
        return targetObj;
    }
}