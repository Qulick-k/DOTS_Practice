using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//PlayerShootManager腳本掛載在遊戲場景上的PlayerShootManager物件
public class PlayerShootManager : MonoBehaviour
{

    //也可以反過來想，把PlayerShootManager寫成一個Singleton，
    public static PlayerShootManager Instance { get; private set; }

    [SerializeField] private GameObject shootPopupPrefab;


    //一喚醒就把腳本賦予給Instance
    private void Awake()
    {
        Instance = this;
    }

    //由於SystemAPI無法在這使用，所以使用World.DefaultGameObjectInjectionWorld<>()找PlayerShootingSystem的Singleton
    //找到後賦予給PlayerShootingSystem playerShootingSystem，之後用playerShootingSystem訂閱Event。方法名為PlayerShootingSystem_OnShoot
    //private void Start()
    //{
    //    PlayerShootingSystem playerShootingSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();

    //    playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
    //}


    //抓取sender，並且轉換類別成Entity，賦予給Entity playerEntity
    //現在知道該在哪裡生成prefab，但還需要知道玩家的位置，所以需要access到player entity的localTransform
    //這次透過World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<>()指定要抓取playerEntity的component data。
    //找到後，賦予給localTransform，之後Instantiate(預製體,位置,轉向)生成prefab。註:float3再生成時，會自動轉換成vector3。
    //private void PlayerShootingSystem_OnShoot(object sender, System.EventArgs e)
    //{
    //    Entity playerEntity = (Entity)sender;
    //    LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
    //    Instantiate(shootPopupPrefab, localTransform.Position, Quaternion.identity);
    //}


    //這腳本用singleton的時候，就可以創立PlayerShoot()方法，直接輸入玩家位置參數，然後呼叫方法生成prefab
    //這樣就能在PlayerShootingSystem腳本上，直接呼叫PlayerShoot()這個方法了
    public void PlayerShoot(Vector3 playerPosition)
    {
        Instantiate(shootPopupPrefab, playerPosition, Quaternion.identity);
    }
}
