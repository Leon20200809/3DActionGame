using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPoint : MonoBehaviour
{
    //出現時エフェクト用
    public GameObject appearteftPrefab;
    public AudioClip appearSE;

    //　出現させる敵
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();

    //　次に敵が出現するまでの時間
    public float appearNextTime;

    //　この場所から出現する敵の数
    public int maxNumOfEnemys;

    //　今何人の敵を出現させたか（総数）
    public int numberOfEnemys;

    //　待ち時間計測フィールド
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemys = 0;
        //elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemyというタグが付いているオブジェクトのデータを箱の中に入れる。
        //enemys = GameObject.FindGameObjectsWithTag("Enemy");


        // データの入った箱の数をコンソール画面に表示する。
        //print(enemys.Length);

        // データの入った箱のデータが０に等しくなった時（Enemyというタグが付いているオブジェクトが全滅したとき）
        //if (enemyList.Length == 0)
        {
            //AppearEnemy();
            Debug.Log("ボス出現");
            
        }

        //　この場所から出現する最大数を超えてたら何もしない
        if (numberOfEnemys >= maxNumOfEnemys)
        {
            return;
        }
        //　経過時間を足す
        //elapsedTime += Time.deltaTime;

        //　経過時間が経ったら
        if (elapsedTime > appearNextTime)
        {
           //elapsedTime = 0f;

            //AppearEnemy();
        }
    }

    //　敵出現メソッド
    void AppearEnemy()
    {
        //設定した敵のプレファブを生成
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        
        //エフェクト再生
        GenerateEffect(gameObject);

        //リストに追加
        enemyList.Add(enemyPrefab);
    }

    void GenerateEffect(GameObject other)
    {
        //出現時エフェクトを生成する
        AudioSource.PlayClipAtPoint(appearSE, gameObject.transform.position);
        GameObject effect = Instantiate(appearteftPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

}
