using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// R3A304　加藤
/// プレイヤーにつける想定
/// </summary>
public class PlayerHelth : MonoBehaviour
{

    // プレイヤーの体力
    int playerLife = 3;
    public int playerLifes
    {
        get { return this.playerLife; }
    }

    // 無敵時間
    [SerializeField] float mutekiTime = 3.0f;
    // ダメージ判定フラグ
    bool isDamage = false;
    // カウント
    float count = 0;
    // ラッシュフラグを拾ってくるため
    PlayerCattleMovement cattleMovement;

    


    private void Start()
    {
        // 自分のところからスクリプトを呼んでくる
        this.cattleMovement = GetComponent<PlayerCattleMovement>();
    }


    /// <summary>
    /// ゲーム毎に呼び出す
    /// </summary>
    private void Update()
    {
        if (isDamage)
        {
            count += Time.deltaTime;
            if (count > mutekiTime)
            {
                // 通常状態に戻す
                isDamage = false;
                count = 0;
            }
        }
        
    }



    /// <summary>
    /// プレイヤーのHP管理
    /// </summary>
    void PlayerHp()
    {
        isDamage = true;
        playerLife -= 1;
        // プレイヤーの体力を0にする
        //if (playerLife < 0) playerLife = 0;
        if (playerLife <= 0)
        {
            Debug.Log("ゲームオーバー");

            

        }
        
        // GameDirectorに衝突を伝える
        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<GameDirector>().DecreaseHp();
            
    }



    /// <summary>
    /// ダメージ判定
    /// くっついたままだと判定しないので
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDamage) return;
        if (cattleMovement.getRush) return;
        if (collision.gameObject.tag == "Enemy") PlayerHp();
    }




}
