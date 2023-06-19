using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCattleMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    // �������x
    [SerializeField] float wolakForce = 30.0f;
    // �ő�X�s�[�h
    [SerializeField] float maxWalkSpeed = 10.0f;
    // ���̒��ł̃X�s�[�h
    [SerializeField] float waterSpeed = 2.0f;
    // ���������邽�߂̐���
    [SerializeField] float downSpeed = 0.01f;
    // �ő�X�s�[�h�̏����l��������
    float farstSpeed = 0;
    // �ːi�̑��x
    [SerializeField] float rushSpeed = 20.0f;
    // ���b�V���ł��鎞��
    [SerializeField] float rushTime = 5;
    // ���b�V��->�ʏ�̈ړ����ł���悤�ɂȂ�܂ł̎��Ԃ��J�E���g
    float StopMoveTime = 1;
    // ���b�V��->�ʏ�̈ړ����ł���悤�ɂȂ�܂ł̎���
    [SerializeField] float stopTime = 1;
    // ���b�V���N�[���^�C��
    [SerializeField] float rushCoolTime = 20;
    // ���b�V���N�[���^�C���̃J�E���g
    float rushCoolCount = 0;


    // �ړ������̃L�[
    int key = 0;

    // �����̔���
    bool waterFlag = false;
    // �ːi�̔���
    bool rush = false;
    // ���b�V���ł��邩
    bool rushCoolTimeFlag = false;
    
    // rush��get
    public bool getRush
    {
        get { return this.rush; }
    }



 
    /// <summary>
    /// ��ԍŏ��ɌĂяo�����
    /// </summary>
    void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        farstSpeed = maxWalkSpeed;
    }

    /// <summary>
    /// ����Ăяo�����
    /// </summary>
    void Update()
    {
        // ���b�V�������A���b�V���N�[���^�C�����ł͂Ȃ�
        if (rush && !rushCoolTimeFlag)
        {
            StopMoveTime += Time.deltaTime;
            if (StopMoveTime > rushTime)
            {
                // ���̂܂܂�߂�����Ǝ~�܂�̂��x���Ȃ邽�ߌ���������
                rigidbody2D.velocity *= downSpeed;
                rush = false;
                rushCoolTimeFlag = true;
                StopMoveTime = 0;       
            }
        }
        // ���b�V�����ɐ����֍s�������̌�������
        else if (rush && waterFlag && StopMoveTime < stopTime)
        {
            StopMoveTime += Time.deltaTime;
            rigidbody2D.velocity *= downSpeed;
        }
        // ���b�V�����ł��邩�̔���
        else if (Input.GetKey(KeyCode.Space)�@&& !waterFlag && !rushCoolTimeFlag)
        {
            rush = true;
            PlayerMoveKey();
        }
        else
        // �ӂ��̈ړ�
        {
            StopMoveTime = 0;
            PlayerMoveKey();
        }


        // �v���C���[�̑��x�֌W�̃N���X
        PlayerMoveSpeed();

        // ���b�V���̃N�[���^�C���֌W�̃N���X
        rushCoolSystem();


    }

    /// <summary>
    /// �ړ�
    /// </summary>
    void PlayerMoveKey()
    {

        // �E�Ɉړ�
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }

        //���Ɉړ�
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }


        // ���������ɉ����ĉ摜�𔽓]
        if (key != 0 && !getRush)
        {
            transform.localScale = new Vector2(key, 1);
        }


    }

    /// <summary>
    /// ���b�V���̃N�[���^�C���������ǂ�
    /// </summary>
    void rushCoolSystem()
    {
        // ���b�V���̃N�[���^�C��
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
    /// �v���C���[�̃X�s�[�h
    /// </summary>
    void PlayerMoveSpeed()
    {
        // �ړ����x�̌v�Z
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
    /// ���ƕǂɂԂ���������
    /// </summary>
    /// <param name="collision"></param>
   private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���b�V�����ł���
        if (getRush)
        {
            rush = false;
            // �ڐG�����̂��ǂ̏ꍇ
            if (collision.gameObject.tag == "Wall")
            {
                rush = false;
                rushCoolTimeFlag = true;
            }
            // �ڐG�����̂����ӂ̏ꍇ
            if (collision.gameObject.tag == "water")
            {
                rush = false;
                rushCoolTimeFlag = true;
                waterFlag = true;
            }
        }

        // ���ʂɐ��֓������ꍇ
        MoveWater(collision);

    }


    /// <summary>
    /// ������o�����̔���
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        waterFlag = false;
        maxWalkSpeed = farstSpeed;
        StopMoveTime = 0;
    }

    /// <summary>
    /// �G���A�̋��E�ʂł�����Ӑ}���Ȃ������΍�
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay2D(Collision2D collision)
    {
        // ���ʂɐ��֓������ꍇ
        MoveWater(collision);
    }

    /// <summary>
    /// �����ł̓�������
    /// </summary>
    /// <param name="collision"></param>
    void MoveWater(Collision2D collision)
    {
        // ���ɓ���������
        if (collision.gameObject.tag == "water")
        {
            waterFlag = true;
        }
    }


}
