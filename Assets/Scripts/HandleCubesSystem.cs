using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct HandleCubesSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        //aspect�ΨӺ޲z�L�h��component���@���޿�椸�A���M�ץt�~�Ыؤ@��Aspect�}�� RotatingMovingCubeAspect
        foreach (RotatingMovingCubeAspect rotatingMovingCubeAspect in SystemAPI.Query<RotatingMovingCubeAspect>())
        {
            //��aspect��ݭn��component���]����A�]�N�i�H�����I�saspect�̭�����k�A�åB��SystemAPI��@��J�Ѽ�
            rotatingMovingCubeAspect.MoveAndRotate(SystemAPI.Time.DeltaTime);
            //��ValueRO�̭��^�Ǫ�����M���ʼƭȡA�ᤩ��ValueRW
            //localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
            //localTransform.ValueRW = localTransform.ValueRO.Translate(movement.ValueRO.movementVector * SystemAPI.Time.DeltaTime);
        }
    }
}
