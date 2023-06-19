using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// R3A304 ����
/// </summary>
public class GameDirector : MonoBehaviour
{

    // �n�[�g�̕\���n
    GameObject heart1;
    GameObject heart2;
    GameObject heart3;
    // player�̗̑͂Ɠ���
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
    /// HP�̕\���ύX
    /// (PlayerHelth���Q�Ƃ��Ă��邽�ߑ̗͑��������ύX����K�v������)
    /// </summary>
    public void DecreaseHp()
    {
        //count ++ ;
        // �����n�[�g
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
