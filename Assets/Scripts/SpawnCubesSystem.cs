using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

//此腳本用來Instantiate CubePrefab
//此腳本沒使用ISystem只是因為CodeMonkey要示範同效果的SystemBase寫法
public partial class SpawnCubesSystem : SystemBase
{
    //由於繼承了Abstract Class，所以至少要override OnUpdate()方法


    protected override void OnCreate()
    {
        //如果使用ISystem，需要ref SystemState state當參數，state.RequireForUpdate<SpawnCubesConfig>();
        //但SystemBase的話，直接RequireForUpdate<SpawnCubesConfig>();即可
        RequireForUpdate<SpawnCubesConfig>();
    }
        
    protected override void OnUpdate()
    {
        //在這邏輯下，codemonkey希望只生成一次Cube，所以在第一次Update過後就停用這個system
        this.Enabled = false;

        //進入SystemAPI使用GetSingleton<SpawnCubesConfig>()回傳給SpawnCubesConfig spawnCubesConfig。這是一種常用的安全檢測，如果有0個entities擁有這個component，那就會送出error
        //OnCreate()會在執行 OnUpdate()之前，先增加RequireForUpdate<SpawnCubesConfig>();。
        //而當SpawnCubesConfig超過1個的話，也會送出error。因此使用GetSingleton()是為了確保真的只有唯一一個Entity，並可以強迫自己執行適合的Singleton Pattern
        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();

        //for迴圈，直到 spawnCubesConfig.amountToSpawn的數值為止
        //要生成spawnEntity，只要透過EntityManager.Instantiate()抓spawnCubesConfig.cubePrefabEntity為輸入參數，然後回傳給Entity spawnEntity即可
        //Instantiate就只是簡單的複製在記憶體內所有的數據，比較特別的點是Prefan Entities會有Prefab Tag component，但是當生成到場景內的時候，不會把Prefab Tag component複製下來。
        //雖然Prefab裡面已經有一堆Authoring component，而且被轉換成entity的componenet的時候也照樣運行，所以不需要手動初始化這些Authoring component，只要spawn即可。
        //另外有一點是，Baker在Prefab上只會執行一次，所有的Authoring component都會被轉換成相應的Entity component一次
        //所以不用理會prefab內的authoring component，就接著繼續使用EntityManager.SetComponentData設定spawnEntity裡面的數據，像是position利用float3(x,y,z)控制位置、quaternion.identity也就是quaternion(0,0,0,0)、比例大小Scale = 1f
        for (int i = 0; i < spawnCubesConfig.amountToSpawn; i++)
        {
            Entity spawnEntity = EntityManager.Instantiate(spawnCubesConfig.cubePrefabEntity);
            EntityManager.SetComponentData(spawnEntity, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-10f, +5), 0.6f, UnityEngine.Random.Range(-4f, +7)),   //如果只有設定position，沒有設定比例的話，物件不會顯示在遊戲場景內
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }

        //額外寫法就是使用LocalTransform.FromPositionRotationScale(new float3(UnityEngine.Random.Range(-10f, +5), 0.6f, UnityEngine.Random.Range(-4f, +7)), quaternion.identity, 1f);
    }
}
