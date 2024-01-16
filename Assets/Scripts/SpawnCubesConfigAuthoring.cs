using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

//�Q��Prefab�ͦ�Entity��C�������̭��C�b�����e�ݭn�s��reference��ڭ̪�Prefab�̡C
//�]���Ыؤ@��Component�s���A�A�z�Lsystem�ϥΨ���Component
//�o�}�����C����������SpawnCubesConfig����
public class SpawnCubesConfigAuthoring : MonoBehaviour
{
    //�ӱ`�W�ŧiGameObject�Mint
    public GameObject cubePrefab;
    public int amountToSpawn;

    //���F��GameObject Prefab�ഫ��Entity Prefab�A���۳Ы�Baker
    //�]�m�@��Entity entity�s��GetEntity()�^�Ǫ���
    //�ѩ�{�b�ϥΪ�config component�N�u���u�O�@��data component�Acode monkey���Ʊ楦�s�b�A�]���L��ܨϥ�TransformUsageFlags.None
    public class Baker : Baker<SpawnCubesConfigAuthoring>
    {
        public override void Bake(SpawnCubesConfigAuthoring authoring)  //authoring��@�@��SpawnCubesConfigAuthoring�}������
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new SpawnCubesConfig
            {
                //SpawnCubesConfig�̭���entity����A�@�˨ϥ�GetEntity�A��SpawnCubesConfigAuthoring��GameObject��cubePrefab�A�]�w��TransformUsageFlags.Dynamic�A����^�ǵ�SpawnCubesConfig��Entity��cubePrefabEntity
                //SpawnCubesConfig�̭���int����A�N��²��A�����SpawnCubesConfigAuthoring��int��amountToSpawn�A�����^�ǵ�SpawnCubesConfig��int��amountToSpawn�Y�i
                cubePrefabEntity = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn,
            });
        }
    }
}

//�@�@��Entity Component
public struct SpawnCubesConfig : IComponentData
{
    //�ŧiEntity�Ө��N�`�W�ŧi��GameObject    
    public Entity cubePrefabEntity;
    public int amountToSpawn;
}
