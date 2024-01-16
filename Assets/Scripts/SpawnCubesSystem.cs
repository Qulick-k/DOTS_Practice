using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

//���}���Ψ�Instantiate CubePrefab
//���}���S�ϥ�ISystem�u�O�]��CodeMonkey�n�ܽd�P�ĪG��SystemBase�g�k
public partial class SpawnCubesSystem : SystemBase
{
    //�ѩ��~�ӤFAbstract Class�A�ҥH�ܤ֭noverride OnUpdate()��k


    protected override void OnCreate()
    {
        //�p�G�ϥ�ISystem�A�ݭnref SystemState state��ѼơAstate.RequireForUpdate<SpawnCubesConfig>();
        //��SystemBase���ܡA����RequireForUpdate<SpawnCubesConfig>();�Y�i
        RequireForUpdate<SpawnCubesConfig>();
    }
        
    protected override void OnUpdate()
    {
        //�b�o�޿�U�Acodemonkey�Ʊ�u�ͦ��@��Cube�A�ҥH�b�Ĥ@��Update�L��N���γo��system
        this.Enabled = false;

        //�i�JSystemAPI�ϥ�GetSingleton<SpawnCubesConfig>()�^�ǵ�SpawnCubesConfig spawnCubesConfig�C�o�O�@�ر`�Ϊ��w���˴��A�p�G��0��entities�֦��o��component�A���N�|�e�Xerror
        //OnCreate()�|�b���� OnUpdate()���e�A���W�[RequireForUpdate<SpawnCubesConfig>();�C
        //�ӷ�SpawnCubesConfig�W�L1�Ӫ��ܡA�]�|�e�Xerror�C�]���ϥ�GetSingleton()�O���F�T�O�u���u���ߤ@�@��Entity�A�åi�H�j���ۤv����A�X��Singleton Pattern
        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();

        //for�j��A���� spawnCubesConfig.amountToSpawn���ƭȬ���
        //�n�ͦ�spawnEntity�A�u�n�z�LEntityManager.Instantiate()��spawnCubesConfig.cubePrefabEntity����J�ѼơA�M��^�ǵ�Entity spawnEntity�Y�i
        //Instantiate�N�u�O²�檺�ƻs�b�O���餺�Ҧ����ƾڡA����S�O���I�OPrefan Entities�|��Prefab Tag component�A���O��ͦ�����������ɭԡA���|��Prefab Tag component�ƻs�U�ӡC
        //���MPrefab�̭��w�g���@��Authoring component�A�ӥB�Q�ഫ��entity��componenet���ɭԤ]�Ӽ˹B��A�ҥH���ݭn��ʪ�l�Ƴo��Authoring component�A�u�nspawn�Y�i�C
        //�t�~���@�I�O�ABaker�bPrefab�W�u�|����@���A�Ҧ���Authoring component���|�Q�ഫ��������Entity component�@��
        //�ҥH���βz�|prefab����authoring component�A�N�����~��ϥ�EntityManager.SetComponentData�]�wspawnEntity�̭����ƾڡA���Oposition�Q��float3(x,y,z)�����m�Bquaternion.identity�]�N�Oquaternion(0,0,0,0)�B��Ҥj�pScale = 1f
        for (int i = 0; i < spawnCubesConfig.amountToSpawn; i++)
        {
            Entity spawnEntity = EntityManager.Instantiate(spawnCubesConfig.cubePrefabEntity);
            EntityManager.SetComponentData(spawnEntity, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-10f, +5), 0.6f, UnityEngine.Random.Range(-4f, +7)),   //�p�G�u���]�wposition�A�S���]�w��Ҫ��ܡA���󤣷|��ܦb�C��������
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }

        //�B�~�g�k�N�O�ϥ�LocalTransform.FromPositionRotationScale(new float3(UnityEngine.Random.Range(-10f, +5), 0.6f, UnityEngine.Random.Range(-4f, +7)), quaternion.identity, 1f);
    }
}
