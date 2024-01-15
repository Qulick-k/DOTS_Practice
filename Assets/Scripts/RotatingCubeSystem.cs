using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//此腳本不擴展MonoBehaviour，使用partial讓DOTS能write the rest of the code，並且由於這是用來管理數據，因此使用結構struct，並且使用ISystem代表unmanaged types。burst在應用時效能會更快。
public partial struct RotatingCubeSystem : ISystem
{

    //繼承ISystem可以使用下面方法
    //OnCreate、OnDestroy、OnUpdate
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //如果要停止這個腳本的運作的話，在這個方法裡面，把state.Enabled修改為false，並且return即可
        state.Enabled = false;
        return;

        //使用foreach去SystemAPI.Query裡面尋找有掛載ratate speed component的entities，以及為了能夠旋轉物件也需要尋找有掛載LocalTransform的entities
        //在Query裡面找特定的types，像是RotateSpeed。在執行ECS時需要明確表明我們想要"讀寫"或是只想要"唯讀"，因此需要在表明的types前面多加RefRW或是RefRO代表"讀寫"或是"唯讀"
        //由於LocalTransform內的參數需要被改寫，所以使用RefRW；而RotateSpeed純粹只需要讀取裡面的參數，所以使用RefRO
        //**在tagComponent上面也可以在Query<>()後面放上.WitheAll<RotatingCube>()或是.WitheNone<Player>()，前者代表只有有RotatingCube這個tag的物件能夠執行；後者代表只要是有Player這個tag的物件就不能夠執行
        foreach ((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotatingCube>())
        {

            //寫一個爛code，測試有無Burst的差異。編譯器可能夠聰明把這段爛code自動移除掉。
            float power = 1f;
            for (int i = 0; i < 100000; i++)
            {
                power *= 2f;
                power /= 2f;
            }

            //現在foreach都找到我們指定的component了，所以我們現在來set up物件的旋轉
            //進入locakTransform找到ValueRW這個能讀寫的變數，在進入locakTransform找到ValueRO這個"唯讀"的變數，在裡面選擇RotateY()方法。
            //RotateY()方法內的參數，透過進入rotateSpeed找到ValueRO的value來抓取數值，並且透過SystemAPI.Time.DeltaTime，讓數值隨著frame rate變化
            //最後當作參數傳進RotateY()，RotateY()運行完後再回傳給locakTransform.ValueRW。
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }

        //為了讓deltaTime存取數值，new一個job，調用 SystemAPI.Time.DeltaTime把數值賦予給deltaTime。最後在傳回rotatingCubeJob
        //接著讓rotatingCubeJob執行ScheduleParallel()，也可以使用Run()來debug但不建議，因為會馬上在主執行緒運作。
        RotatingCubeJob rotatingCubeJob = new RotatingCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        rotatingCubeJob.ScheduleParallel();
    }


    //由於使用Burst仍然只在Main Thread上面運作，而無法多執行緒運作，因此需要Job協助
    //設置RotatingCubeJob腳本繼承IJobEntity來讓Entity運作，並一樣使用partial和struct，盡量不繼承IJobParallelFor
    //讓Job和BurstCompile結合，會跑出最好的效果。
    [BurstCompile]
    //[WithNone(typeof(Player))] //為了不包含Player，設置WithNone屬性並設定其中的type為Player
    [WithAll(typeof(RotatingCube))]
    public partial struct RotatingCubeJob : IJobEntity
    {
        public float deltaTime;

        //新增一個新的Execute()方法，由於參數來源就兩個，LocalTransform和RotateSpeed，前綴用ref能把參考的數值改寫，in則是不能把參考的數值改寫
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            float power = 1f;
            for (int i = 0; i < 100000; i++)
            {
                power *= 2f;
                power /= 2f;
            }

            //在job裡面變數前面就不需要RefRW和RefRO了
            //由於IJobEntity不允許使用SystemAPI，因此設置一個公有變數deltaTime，在OnUpdate()裡面把數值傳給deltaTime
            localTransform = localTransform.RotateY(rotateSpeed.value * deltaTime * power);
        }
    }
}
