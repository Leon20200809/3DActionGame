using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //敵を全滅させたかどうかを判定し、指定された位置に次の敵の生成を支持する
    //その敵も全滅したかどうかを判定し、指定された位置にボスを生成する
    //各敵とボスの生成のタイミングではSEとエフェクト（ボス出現時はBGMも変更)を生成する
    //ボスを倒すとクリアの判定をする

    //出現時エフェクト用
    public GameObject appearteftPrefab;
    public AudioClip appearSE;

    //　出現させる敵
    public GameObject enemyPrefab;
    public GameObject enemyPrefabBoss;
    public List<GameObject> enemyList = new List<GameObject>();

    //生成した数
    public int appearEnemyNum;

    //敵を倒した数
    public int destroyEnemyNum;

    //BOSS用BGM再生
    AudioSource bgm1;
    AudioSource bgm2;

    //ゲームクリアフラグ
    public GameObject gameClear;

    //勝利ボイス
    public AudioClip syouriVoice;


    // Start is called before the first frame update
    void Start()
    {
        appearEnemyNum = enemyList.Count;
        bgm1 = GameObject.Find("BGM1").GetComponent<AudioSource>();
        bgm2 = GameObject.Find("BGM2").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (appearEnemyNum == 0)
        {
            AppearEnemy(new Vector3(0f, 0f, 10f));
            AppearEnemy(new Vector3(8f, 0f, 10f));
            AppearEnemy(new Vector3(-8f, 0f, 10f));
            Debug.Log("WAVE1");
        }

        if (destroyEnemyNum == 3 && appearEnemyNum == 3)
        {
            AppearEnemy(new Vector3(0f, 0f, -5f));
            AppearEnemy(new Vector3(10f, 0f, -5f));
            AppearEnemy(new Vector3(-10f, 0f, -5f));
            Debug.Log("WAVE2");
        }

        if (destroyEnemyNum == 5 && appearEnemyNum == 6)
        {
            AppearEnemy(new Vector3(7f, 0f, 0f));
            AppearEnemy(new Vector3(7f, 0f, 10f));
            AppearEnemy(new Vector3(-15f, 0f, -0f));
            AppearEnemy(new Vector3(-15f, 0f, -10f));
            Debug.Log("WAVE3");
        }

        if (destroyEnemyNum == 10 && appearEnemyNum == 10)
        {
            AppearEnemy(new Vector3(5f, 0f, 10f));
            AppearEnemy(new Vector3(5f, 0f, 0f));
            AppearEnemy(new Vector3(-9f, 0f, -5f));
            AppearEnemy(new Vector3(-9f, 0f, -10f));
            Debug.Log("WAVE4");
        }

        if (destroyEnemyNum == 14 && appearEnemyNum == 14)
        {
            bgm1.enabled = false;
            bgm2.enabled = true;
            AppearEnemyBoss(new Vector3(0f, 0f, 0f));
            Debug.Log("ボス登場");
        }

        if (destroyEnemyNum == 15)
        {
            Debug.Log("ゲームクリア");
            StartCoroutine(Kankaku());

        }

    }

    /// <summary>
    /// 敵出現メソッド
    /// </summary>
    /// <param name="apperPos">出現位置ワールド座標</param>
    void AppearEnemy(Vector3 apperPos)
    {
        //設定した敵のプレファブを生成
        GameObject enemyClone = Instantiate(enemyPrefab, apperPos, Quaternion.identity);

        //エフェクト再生
        GenerateEffect(enemyClone);

        //リストに追加
        enemyList.Add(enemyClone);

        //カウント追加
        appearEnemyNum++;
    }
    /// <summary>
    /// ボス出現メソッド
    /// </summary>
    /// <param name="apperPos"></param>
    void AppearEnemyBoss(Vector3 apperPos)
    {
        //設定した敵のプレファブを生成
        GameObject enemyClone = Instantiate(enemyPrefabBoss, apperPos, Quaternion.identity);

        //エフェクト再生
        GenerateEffect(enemyClone);

        //リストに追加
        enemyList.Add(enemyClone);

        //カウント追加
        appearEnemyNum++;
    }

    public void DestroyEnemyNum()
    {
        destroyEnemyNum++;
    }

    /// <summary>
    /// 敵出現時のエフェクト生成⇒削除
    /// </summary>
    /// <param name="other"></param>
    void GenerateEffect(GameObject other)
    {
        //出現時エフェクトを生成する
        AudioSource.PlayClipAtPoint(appearSE, Camera.main.transform.position, 0.2f);
        GameObject effect = Instantiate(appearteftPrefab, other.transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(指定秒の間を設ける)
        yield return new WaitForSeconds(2f);
        Debug.Log("Wait");
        AudioSource.PlayClipAtPoint(syouriVoice, Camera.main.transform.position);
        gameClear.SetActive(true);
    }
}
