using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct HandleCubesSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        //aspect用來管理過多的component成一個邏輯單元，此專案另外創建一個Aspect腳本 RotatingMovingCubeAspect
        foreach (RotatingMovingCubeAspect rotatingMovingCubeAspect in SystemAPI.Query<RotatingMovingCubeAspect>())
        {
            //用aspect把需要的component打包之後，也就可以直接呼叫aspect裡面的方法，並且把SystemAPI當作輸入參數
            rotatingMovingCubeAspect.MoveAndRotate(SystemAPI.Time.DeltaTime);
            //把ValueRO裡面回傳的旋轉和移動數值，賦予給ValueRW
            //localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
            //localTransform.ValueRW = localTransform.ValueRO.Translate(movement.ValueRO.movementVector * SystemAPI.Time.DeltaTime);
        }
    }
}
