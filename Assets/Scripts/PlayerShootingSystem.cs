using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//���}���������a�g�X�l�u�����p�A�]���s�WPlayerShootingSystem�}���A�ϥ�partial class�A�åB�~��SystemBase
public partial class PlayerShootingSystem : SystemBase
{
    //�T�O�u�]�bPlayer tag component�W
    protected override void OnCreate()
    {        
        RequireForUpdate<Player>();
    }

    
    //���]��Cube��@�l�u�A�M���²�檺�����˴����a���S�����ť���A�p�G���a�S�����ť���Nreturn
    protected override void OnUpdate()
    {

        //����UT�BY��A���oPlayer��Singleton�A�M��ҥ�playerEntity��stunned component���ҥΩM�T��
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


        //�s�W�@��new EntityCommandBuffer();�AEntityCommandBuffer�ݭn�@�ӯS�w���O������t���A���\�h�ﶵ���ܡA�ݬ�EntityCommandBuffer�n�d�s�h�[
        //EntityCommandBuffer�b�o�Ӹ}���u�Ψӥͦ�entity�A�M��N��EntityCommandBuffer���A�ҥH�ϥ�temp�Atemp�u�|��EntityCommandBuffer�d�s1 frame�άO a job�A����N�������
        //�p�G�n�^����ӷL���į઺�ܡA�i�H�ϥ�new EntityCommandBuffer(WorldUpdateAllocator);
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        #region ���� 
        //�M�䪱�a����m�A���l�u���D�Ӧb���̥ͦ��C�ĥΰ�ŪRefRO�Y�i�A�M�ϥ�WithAll<Player>()���wPlayer��tag component
        //�p�G�n�ͦ��s��Entity���ܡA�N�n�վ�U����Entity spawnedEntity���{���X�F
        //���p�G���Q�n�վ㪺�ܡA���F�bJob�άOforeacj�ͦ��άO�R���A�i�H�ϥ�EntityCommandBuffer
        //�p�Gnew prefab�S��match�� SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>()���ܡA�N�ॿ�`�B��U�����{���X�C
        //���O�o�}���nspawn�@��cube entity�A���N�|�S��player component�C�Ӧp�G�w�g�Q�ͦ���entity��type�ŦX SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>()���ܡA�N�L�k�B�@
        //�ҥH���ӥ�EntityCommandBuffer�ӥͦ�Entity�C        
        //*�ɥR*�n���}���P�_�A���a�b���B��Stunned���A�A�N�����A�h�gWithDisabled<Stunned>()
        #endregion
        foreach (RefRO<LocalTransform> localTramsform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<Stunned>())
        {
            //entityCommandBuffer�èS���u���ͦ�entity�A�ӬO�s�W�@��command��command list��̭��A����~�|����command list
            //�I�sentityCommandBuffer.SetComponent()�A�M���spawnedEntity�MLocalTransform��@��J�ѼƩ�i�h            
            Entity spawnedEntity = entityCommandBuffer.Instantiate(spawnCubesConfig.cubePrefabEntity); 
            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform  //�o�u�O�n���@��command�A���|�u���]�ߥ���component data
            {
                Position = localTramsform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 1F
            });
        }

        //���C���]�w�n�F�A�N�i�H����command list�C
        //�I�sPlayback()�A�N�ளEntityManager��ѼơA��EntityManager�ͦ�entity�F
        entityCommandBuffer.Playback(EntityManager);
    }
}
