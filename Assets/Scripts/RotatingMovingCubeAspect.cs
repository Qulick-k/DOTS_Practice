using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//aspect必須為readonly，而底下所有的componenet也都必須為readonly，但是component的前綴還是必須選擇RefRO或是RefRW
public readonly partial struct RotatingMovingCubeAspect : IAspect
{    
    public readonly RefRO<RotatingCube> rotatingCube;     //這行新增的rotatingCube，是一個tag componemt
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<RotateSpeed> rotateSpeed;
    public readonly RefRO<Movement> movement;

    //直接將運作邏輯打包進aspect成方法，由於aspect腳本無法使用SystemAPI，所以設置輸入參數deltatime
    public void MoveAndRotate(float deltatime)
    {
        localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * deltatime);
        localTransform.ValueRW = localTransform.ValueRO.Translate(movement.ValueRO.movementVector * deltatime);
    }
    
}