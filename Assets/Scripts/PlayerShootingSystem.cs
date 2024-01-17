using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//此腳本模擬玩家射出子彈的情況，因此新增PlayerShootingSystem腳本，使用partial class，並且繼承SystemBase
public partial class PlayerShootingSystem : SystemBase
{
    //確保只跑在Player tag component上
    protected override void OnCreate()
    {        
        RequireForUpdate<Player>();
    }

    
    //假設讓Cube當作子彈，然後用簡單的按鍵檢測玩家有沒有按空白鍵，如果玩家沒有按空白鍵就return
    protected override void OnUpdate()
    {

        //當按下T、Y鍵，取得Player的Singleton，然後啟用playerEntity中stunned component的啟用和禁用
        if (Input.GetKeyDown(KeyCode.T))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, true);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, false);
        }


        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();


        //新增一個new EntityCommandBuffer();，EntityCommandBuffer需要一個特定的記憶體分配器，有許多選項能選擇，端看EntityCommandBuffer要留存多久
        //EntityCommandBuffer在這個腳本只用來生成entity，然後就把EntityCommandBuffer丟棄，所以使用temp，temp只會讓EntityCommandBuffer留存1 frame或是 a job，之後就直接丟棄
        //如果要榨取更細微的效能的話，可以使用new EntityCommandBuffer(WorldUpdateAllocator);
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        #region 註解 
        //尋找玩家的位置，讓子彈知道該在哪裡生成。採用唯讀RefRO即可，和使用WithAll<Player>()指定Player的tag component
        //如果要生成新的Entity的話，就要調整下面的Entity spawnedEntity的程式碼了
        //但如果不想要調整的話，為了在Job或是foreacj生成或是摧毀，可以使用EntityCommandBuffer
        //如果new prefab沒有match到 SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>()的話，就能正常運行下面的程式碼。
        //像是這腳本要spawn一個cube entity，那就會沒有player component。而如果已經被生成的entity的type符合 SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>()的話，就無法運作
        //所以應該用EntityCommandBuffer來生成Entity。        
        //*補充*要讓腳本判斷，玩家在不處於Stunned狀態，就必須再多寫WithDisabled<Stunned>()
        #endregion
        foreach (RefRO<LocalTransform> localTramsform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<Stunned>())
        {
            //entityCommandBuffer並沒有真的生成entity，而是新增一個command到command list表裡面，之後才會執行command list
            //呼叫entityCommandBuffer.SetComponent()，然後把spawnedEntity和LocalTransform當作輸入參數放進去            
            Entity spawnedEntity = entityCommandBuffer.Instantiate(spawnCubesConfig.cubePrefabEntity); 
            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform  //這只是登錄一個command，不會真的設立任何的component data
            {
                Position = localTramsform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 1F
            });
        }

        //當佇列都設定好了，就可以執行command list。
        //呼叫Playback()，就能拿EntityManager當參數，讓EntityManager生成entity了
        entityCommandBuffer.Playback(EntityManager);
    }
}
