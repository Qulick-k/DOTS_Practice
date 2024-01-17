using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���}���O�`�W�}���A�Ψӷ�@�ܽdECS�M�`�W���󤬰ʪ��d�Ҹ}��
public class ShootPopup : MonoBehaviour
{
    private float destroyTimer = 1f;

    // �D�n�N�O���r��C���W��
    // �M��L�F1���R���r��
    private void Update()
    {
        float moveSpeed = 2f;
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
