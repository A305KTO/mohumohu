using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// R3A304 加藤
/// </summary>
public class GameDirector : MonoBehaviour
{

    // ハートの表示系
    GameObject heart1;
    GameObject heart2;
    GameObject heart3;
    // playerの体力と同期
    [SerializeField]GameObject player;
    PlayerHelth playerHelth;
    
    //int count = 0;

    void Start()
    {
        this.heart1 = GameObject.Find("Hart1");
        this.heart2 = GameObject.Find("Hart2");
        this.heart3 = GameObject.Find("Hart3");
        player = GameObject.FindWithTag("Player");
        playerHelth = player.GetComponent<PlayerHelth>();
    }


    /// <summary>
    /// HPの表示変更
    /// (PlayerHelthを参照しているため体力増えた時変更する必要がある)
    /// </summary>
    public void DecreaseHp()
    {
        //count ++ ;
        // 消すハート
        switch (playerHelth.playerLifes)
        {
            case 2:
                this.heart1.SetActive(false);
                break;
            case 1:
                this.heart2.SetActive(false);
                break;
            case 0:
                this.heart3.SetActive(false);
                break;
            default:
                break;
        }

    }
}
