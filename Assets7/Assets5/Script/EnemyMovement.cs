using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
/// �쐬��
/// R3A304 �����犰
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    
    Rigidbody2D rigidbody2D;
    // player�̏����E���Ă���
    [SerializeField] GameObject player;

    // �����X�s�[�h
    [SerializeField]float speed = 50f;
    // �X�s�[�h�𗎂Ƃ�����
    [SerializeField]int speedDownTime = 10;
    // �v���C���[�Ƃ̐ڐG�t���O
    bool Triggerflag = false;
    // �X�^���t���O
    bool stanFlag = false;

    // �J�E���g
    float count = 0;
    float count2 = 0;

    // �����̃X�s�[�h���i�[���������
    float farstSpeed = 0;
    // ��������Ƃ��ɑ������X�s�[�h
    [SerializeField] float downSpeed = 2;
    [SerializeField] float stanTime = 20;
    // ���b�V���t���O���E���Ă��邽��
    PlayerCattleMovement cattleMovement;

    void Start()
    {
        // �I�u�W�F�N�g��Rigidbody2D���擾
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Player�^�O�̃I�u�W�F�N�g���擾
        player = GameObject.FindWithTag("Player");
        // ���l�ɂ��Ă���X�N���v�g����Ƃ��Ă���
        cattleMovement = player.GetComponent<PlayerCattleMovement>();
        farstSpeed = speed;  
    }

    void Update()
    {
        // �ړ��֐��̌Ăяo��
        EnemyMove();
        // ���������邩�ǂ���
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
    /// Enemy�̈ړ�
    /// �Ƃ肠�����ǔ����邾��
    /// �R�[�h�͊ۃp�N��
    /// </summary>
    void EnemyMove()
    {
        // PLAYER�̈ʒu���擾
        Vector2 targetPos = player.transform.position;
        // PLAYER��x���W
        float x = targetPos.x;
        // PLAYER��y���W
        float y = targetPos.y;
        // �ړ����v�Z�����邽�߂̂Q�����̃x�N�g�������
        Vector2 direction = new Vector2(
            x - transform.position.x, y-transform.position.y).normalized;
        // ENEMY��Rigidbody2D�Ɉړ����x���w�肷��
        rigidbody2D.velocity = direction * speed;


    }


    /// <summary>
    /// �v���C���[�ƐڐG�����Ƃ��̌�������
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
