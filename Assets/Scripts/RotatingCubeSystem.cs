using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//���}�����X�iMonoBehaviour�A�ϥ�partial��DOTS��write the rest of the code�A�åB�ѩ�o�O�ΨӺ޲z�ƾڡA�]���ϥε��cstruct�A�åB�ϥ�ISystem�N��unmanaged types�Cburst�b���ήɮį�|��֡C
public partial struct RotatingCubeSystem : ISystem
{

    //�~��ISystem�i�H�ϥΤU����k
    //OnCreate�BOnDestroy�BOnUpdate
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }

    public void OnUpdate(ref SystemState state)
    {
        //�ϥ�foreach�hSystemAPI.Query�̭��M�䦳����ratate speed component��entities�A�H�ά��F������ફ��]�ݭn�M�䦳����LocalTransform��entities
        //�bQuery�̭���S�w��types�A���ORotateSpeed�C�b����ECS�ɻݭn���T����ڭ̷Q�n"Ū�g"�άO�u�Q�n"��Ū"�A�]���ݭn�b�����types�e���h�[RefRW�άORefRO�N��"Ū�g"�άO"��Ū"
        //�ѩ�LocalTransform�����Ѽƻݭn�Q��g�A�ҥH�ϥ�RefRW�F��RotateSpeed�º�u�ݭnŪ���̭����ѼơA�ҥH�ϥ�RefRO
        foreach ((RefRW<LocalTransform> locakTransform, RefRO<RotateSpeed> rotateSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>())
        {
            //�{�bforeach�����ڭ̫��w��component�F�A�ҥH�ڭ̲{�b��set up���󪺱���
            //�i�JlocakTransform���ValueRW�o�ӯ�Ū�g���ܼơA�b�i�JlocakTransform���ValueRO�o��"��Ū"���ܼơA�b�̭����RotateY()��k�C
            //RotateY()��k�����ѼơA�z�L�i�JrotateSpeed���ValueRO��value�ӧ���ƭȡA�åB�z�LSystemAPI.Time.DeltaTime�A���ƭ��H��frame rate�ܤ�
            //�̫��@�ѼƶǶiRotateY()�ARotateY()�B�槹��A�^�ǵ�locakTransform.ValueRW�C
            locakTransform.ValueRW = locakTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }
    }
}
