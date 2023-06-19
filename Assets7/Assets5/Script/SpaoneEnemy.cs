using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// R3A304�@����
/// </summary>
public class SpaoneEnemy : MonoBehaviour
{
    
    // �G�l�~�[�̍ő�l
    [SerializeField] int maxEnemies = 5;
    int enemyCount = 0;

    // �J�E���g
    float time = 0;
    // �������Ԃ̊Ԋu
    float interval = 10;
   

    // �G�𐶐�����ꏊ
    // ��̃I�u�W�F�N�g���g���Ďw��
    [SerializeField] Transform[] spawnPoints;

    // �X�|�[��������G�l�~�[�̎��
    [SerializeField] GameObject enemy;


    void Update()
    {
        // �������Ԃ̃J�E���g
        time += Time.deltaTime;
        if (time > interval)
        {
            SpawnEnemy();
            time = 0;
        }
    }


   
    /// <summary>
    /// ����
    /// </summary>
    void SpawnEnemy()
    {
        
        if (enemyCount < maxEnemies)
        {
            
            // �G�𐶐�����t���O�������_���Ɍ��߂�
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            // �G�𐶐�����
            Instantiate(enemy, spawnPoints[spawnPointIndex].position,
                spawnPoints[spawnPointIndex].rotation);
            enemyCount += 1;
        }
    }


}
