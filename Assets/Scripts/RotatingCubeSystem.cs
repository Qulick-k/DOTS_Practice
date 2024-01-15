using System.Collections;
using System.Collections.Generic;
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

    public void OnUpdate(ref SystemState state)
    {
        //使用foreach去SystemAPI.Query裡面尋找有掛載ratate speed component的entities，以及為了能夠旋轉物件也需要尋找有掛載LocalTransform的entities
        //在Query裡面找特定的types，像是RotateSpeed。在執行ECS時需要明確表明我們想要"讀寫"或是只想要"唯讀"，因此需要在表明的types前面多加RefRW或是RefRO代表"讀寫"或是"唯讀"
        //由於LocalTransform內的參數需要被改寫，所以使用RefRW；而RotateSpeed純粹只需要讀取裡面的參數，所以使用RefRO
        foreach ((RefRW<LocalTransform> locakTransform, RefRO<RotateSpeed> rotateSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
        {
            //現在foreach都找到我們指定的component了，所以我們現在來set up物件的旋轉
            //進入locakTransform找到ValueRW這個能讀寫的變數，在進入locakTransform找到ValueRO這個"唯讀"的變數，在裡面選擇RotateY()方法。
            //RotateY()方法內的參數，透過進入rotateSpeed找到ValueRO的value來抓取數值，並且透過SystemAPI.Time.DeltaTime，讓數值隨著frame rate變化
            //最後當作參數傳進RotateY()，RotateY()運行完後再回傳給locakTransform.ValueRW。
            locakTransform.ValueRW = locakTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }
    }
}
