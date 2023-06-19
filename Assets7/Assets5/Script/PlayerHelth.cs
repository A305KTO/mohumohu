using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// R3A304�@����
/// �v���C���[�ɂ���z��
/// </summary>
public class PlayerHelth : MonoBehaviour
{

    // �v���C���[�̗̑�
    int playerLife = 3;
    public int playerLifes
    {
        get { return this.playerLife; }
    }

    // ���G����
    [SerializeField] float mutekiTime = 3.0f;
    // �_���[�W����t���O
    bool isDamage = false;
    // �J�E���g
    float count = 0;
    // ���b�V���t���O���E���Ă��邽��
    PlayerCattleMovement cattleMovement;

    


    private void Start()
    {
        // �����̂Ƃ��납��X�N���v�g���Ă�ł���
        this.cattleMovement = GetComponent<PlayerCattleMovement>();
    }


    /// <summary>
    /// �Q�[�����ɌĂяo��
    /// </summary>
    private void Update()
    {
        if (isDamage)
        {
            count += Time.deltaTime;
            if (count > mutekiTime)
            {
                // �ʏ��Ԃɖ߂�
                isDamage = false;
                count = 0;
            }
        }
        
    }



    /// <summary>
    /// �v���C���[��HP�Ǘ�
    /// </summary>
    void PlayerHp()
    {
        isDamage = true;
        playerLife -= 1;
        // �v���C���[�̗̑͂�0�ɂ���
        //if (playerLife < 0) playerLife = 0;
        if (playerLife <= 0)
        {
            Debug.Log("�Q�[���I�[�o�[");

            

        }
        
        // GameDirector�ɏՓ˂�`����
        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<GameDirector>().DecreaseHp();
            
    }



    /// <summary>
    /// �_���[�W����
    /// ���������܂܂��Ɣ��肵�Ȃ��̂�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDamage) return;
        if (cattleMovement.getRush) return;
        if (collision.gameObject.tag == "Enemy") PlayerHp();
    }




}
