using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// R3A304　加藤
/// </summary>
public class SpaoneEnemy : MonoBehaviour
{
    
    // エネミーの最大値
    [SerializeField] int maxEnemies = 5;
    int enemyCount = 0;

    // カウント
    float time = 0;
    // 生成時間の間隔
    float interval = 10;
   

    // 敵を生成する場所
    // 空のオブジェクトを使って指定
    [SerializeField] Transform[] spawnPoints;

    // スポーンさせるエネミーの種類
    [SerializeField] GameObject enemy;


    void Update()
    {
        // 生成時間のカウント
        time += Time.deltaTime;
        if (time > interval)
        {
            SpawnEnemy();
            time = 0;
        }
    }


   
    /// <summary>
    /// 生成
    /// </summary>
    void SpawnEnemy()
    {
        
        if (enemyCount < maxEnemies)
        {
            
            // 敵を生成するフラグをランダムに決める
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            // 敵を生成する
            Instantiate(enemy, spawnPoints[spawnPointIndex].position,
                spawnPoints[spawnPointIndex].rotation);
            enemyCount += 1;
        }
    }


}
