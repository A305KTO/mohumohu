using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
/// 作成者
/// R3A304 加藤千寛
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    
    Rigidbody2D rigidbody2D;
    // playerの情報を拾ってくる
    [SerializeField] GameObject player;

    // 初期スピード
    [SerializeField]float speed = 50f;
    // スピードを落とす時間
    [SerializeField]int speedDownTime = 10;
    // プレイヤーとの接触フラグ
    bool Triggerflag = false;
    // スタンフラグ
    bool stanFlag = false;

    // カウント
    float count = 0;
    float count2 = 0;

    // 初期のスピードを格納し代入する
    float farstSpeed = 0;
    // 減速するときに代入するスピード
    [SerializeField] float downSpeed = 2;
    [SerializeField] float stanTime = 20;
    // ラッシュフラグを拾ってくるため
    PlayerCattleMovement cattleMovement;

    void Start()
    {
        // オブジェクトのRigidbody2Dを取得
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Playerタグのオブジェクトを取得
        player = GameObject.FindWithTag("Player");
        // 他人についているスクリプトからとってくる
        cattleMovement = player.GetComponent<PlayerCattleMovement>();
        farstSpeed = speed;  
    }

    void Update()
    {
        // 移動関数の呼び出し
        EnemyMove();
        // 減速させるかどうか
        if (Triggerflag)
        {
            count += Time.deltaTime;
            if (count > speedDownTime)
            {
                speed = farstSpeed;
                count = 0;
                Triggerflag = false;
            }
        }

        if (stanFlag)
        {
            count2 += Time.deltaTime;

            if (count2 > stanTime)
            {
                speed = farstSpeed;
                count2 = 0;
            }
        }

    }

    /// <summary>
    /// Enemyの移動
    /// とりあえず追尾するだけ
    /// コードは丸パクリ
    /// </summary>
    void EnemyMove()
    {
        // PLAYERの位置を取得
        Vector2 targetPos = player.transform.position;
        // PLAYERのx座標
        float x = targetPos.x;
        // PLAYERのy座標
        float y = targetPos.y;
        // 移動を計算させるための２次元のベクトルを作る
        Vector2 direction = new Vector2(
            x - transform.position.x, y-transform.position.y).normalized;
        // ENEMYのRigidbody2Dに移動速度を指定する
        rigidbody2D.velocity = direction * speed;


    }


    /// <summary>
    /// プレイヤーと接触したときの減速判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speed = downSpeed;
            Triggerflag = true;
            if (cattleMovement.getRush)
            {
                stanFlag = true;
                speed = 0;
            }
        }
    }
}
