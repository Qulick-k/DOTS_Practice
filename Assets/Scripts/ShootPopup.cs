using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//此腳本是常規腳本，用來當作示範ECS和常規物件互動的範例腳本
public class ShootPopup : MonoBehaviour
{
    private float destroyTimer = 1f;

    // 主要就是讓字體每秒往上升
    // 然後過了1秒後刪掉字體
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
