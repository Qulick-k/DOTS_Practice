using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//aspect������readonly�A�ө��U�Ҧ���componenet�]��������readonly�A���Ocomponent���e���٬O�������RefRO�άORefRW
public readonly partial struct RotatingMovingCubeAspect : IAspect
{    
    public readonly RefRO<RotatingCube> rotatingCube;     //�o��s�W��rotatingCube�A�O�@��tag componemt
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<RotateSpeed> rotateSpeed;
    public readonly RefRO<Movement> movement;

    //�����N�B�@�޿襴�]�iaspect����k�A�ѩ�aspect�}���L�k�ϥ�SystemAPI�A�ҥH�]�m��J�Ѽ�deltatime
    public void MoveAndRotate(float deltatime)
    {
        localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * deltatime);
        localTransform.ValueRW = localTransform.ValueRO.Translate(movement.ValueRO.movementVector * deltatime);
    }
    
}