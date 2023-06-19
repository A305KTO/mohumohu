using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCattleMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    // 歩く速度
    [SerializeField] float wolakForce = 30.0f;
    // 最大スピード
    [SerializeField] float maxWalkSpeed = 10.0f;
    // 水の中でのスピード
    [SerializeField] float waterSpeed = 2.0f;
    // 減速させるための数字
    [SerializeField] float downSpeed = 0.01f;
    // 最大スピードの初期値を代入する
    float farstSpeed = 0;
    // 突進の速度
    [SerializeField] float rushSpeed = 20.0f;
    // ラッシュできる時間
    [SerializeField] float rushTime = 5;
    // ラッシュ->通常の移動ができるようになるまでの時間をカウント
    float StopMoveTime = 1;
    // ラッシュ->通常の移動ができるようになるまでの時間
    [SerializeField] float stopTime = 1;
    // ラッシュクールタイム
    [SerializeField] float rushCoolTime = 20;
    // ラッシュクールタイムのカウント
    float rushCoolCount = 0;


    // 移動方向のキー
    int key = 0;

    // 水中の判定
    bool waterFlag = false;
    // 突進の判定
    bool rush = false;
    // ラッシュできるか
    bool rushCoolTimeFlag = false;
    
    // rushのget
    public bool getRush
    {
        get { return this.rush; }
    }



 
    /// <summary>
    /// 一番最初に呼び出される
    /// </summary>
    void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        farstSpeed = maxWalkSpeed;
    }

    /// <summary>
    /// 毎回呼び出される
    /// </summary>
    void Update()
    {
        // ラッシュ中かつ、ラッシュクールタイム中ではない
        if (rush && !rushCoolTimeFlag)
        {
            StopMoveTime += Time.deltaTime;
            if (StopMoveTime > rushTime)
            {
                // そのままやめさせると止まるのが遅くなるため減速させる
                rigidbody2D.velocity *= downSpeed;
                rush = false;
                rushCoolTimeFlag = true;
                StopMoveTime = 0;       
            }
        }
        // ラッシュ中に水中へ行った時の減速判定
        else if (rush && waterFlag && StopMoveTime < stopTime)
        {
            StopMoveTime += Time.deltaTime;
            rigidbody2D.velocity *= downSpeed;
        }
        // ラッシュができるかの判定
        else if (Input.GetKey(KeyCode.Space)　&& !waterFlag && !rushCoolTimeFlag)
        {
            rush = true;
            PlayerMoveKey();
        }
        else
        // ふつうの移動
        {
            StopMoveTime = 0;
            PlayerMoveKey();
        }


        // プレイヤーの速度関係のクラス
        PlayerMoveSpeed();

        // ラッシュのクールタイム関係のクラス
        rushCoolSystem();


    }

    /// <summary>
    /// 移動
    /// </summary>
    void PlayerMoveKey()
    {

        // 右に移動
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }

        //左に移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }


        // 動く向きに応じて画像を反転
        if (key != 0 && !getRush)
        {
            transform.localScale = new Vector2(key, 1);
        }


    }

    /// <summary>
    /// ラッシュのクールタイムをつかさどる
    /// </summary>
    void rushCoolSystem()
    {
        // ラッシュのクールタイム
        if (rushCoolTimeFlag)
        {
            rushCoolCount += Time.deltaTime;
            if (rushCoolCount > rushCoolTime)
            {
                rushCoolTimeFlag = false;
                rushCoolCount = 0;
            }
        }
    }

    /// <summary>
    /// プレイヤーのスピード
    /// </summary>
    void PlayerMoveSpeed()
    {
        // 移動速度の計算
        float speedX = Mathf.Abs(this.rigidbody2D.velocity.x);

        if (speedX < this.maxWalkSpeed)
        {
            if (rush)
            {
                maxWalkSpeed = rushSpeed;
                this.rigidbody2D.AddForce(transform.right * key * this.rushSpeed);
            }
            else if (waterFlag)
            {
                maxWalkSpeed = waterSpeed;
                this.rigidbody2D.AddForce(transform.right * key * waterSpeed);
            }
            else
            {
                maxWalkSpeed = farstSpeed;
                this.rigidbody2D.AddForce(transform.right * key * this.wolakForce);
            }

        }
    }


    /// <summary>
    /// 水と壁にぶつかった判定
    /// </summary>
    /// <param name="collision"></param>
   private void OnCollisionEnter2D(Collision2D collision)
    {
        // ラッシュ中である
        if (getRush)
        {
            rush = false;
            // 接触したのが壁の場合
            if (collision.gameObject.tag == "Wall")
            {
                rush = false;
                rushCoolTimeFlag = true;
            }
            // 接触したのが水辺の場合
            if (collision.gameObject.tag == "water")
            {
                rush = false;
                rushCoolTimeFlag = true;
                waterFlag = true;
            }
        }

        // 普通に水へ入った場合
        MoveWater(collision);

    }


    /// <summary>
    /// 水から出た時の判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        waterFlag = false;
        maxWalkSpeed = farstSpeed;
        StopMoveTime = 0;
    }

    /// <summary>
    /// エリアの境界面でおこる意図しない挙動対策
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay2D(Collision2D collision)
    {
        // 普通に水へ入った場合
        MoveWater(collision);
    }

    /// <summary>
    /// 水中での動き判定
    /// </summary>
    /// <param name="collision"></param>
    void MoveWater(Collision2D collision)
    {
        // 水に入った判定
        if (collision.gameObject.tag == "water")
        {
            waterFlag = true;
        }
    }


}
