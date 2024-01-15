using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
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

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //�p�G�n����o�Ӹ}�����B�@���ܡA�b�o�Ӥ�k�̭��A��state.Enabled�קאּfalse�A�åBreturn�Y�i
        state.Enabled = false;
        return;

        //�ϥ�foreach�hSystemAPI.Query�̭��M�䦳����ratate speed component��entities�A�H�ά��F������ફ��]�ݭn�M�䦳����LocalTransform��entities
        //�bQuery�̭���S�w��types�A���ORotateSpeed�C�b����ECS�ɻݭn���T����ڭ̷Q�n"Ū�g"�άO�u�Q�n"��Ū"�A�]���ݭn�b�����types�e���h�[RefRW�άORefRO�N��"Ū�g"�άO"��Ū"
        //�ѩ�LocalTransform�����Ѽƻݭn�Q��g�A�ҥH�ϥ�RefRW�F��RotateSpeed�º�u�ݭnŪ���̭����ѼơA�ҥH�ϥ�RefRO
        //**�btagComponent�W���]�i�H�bQuery<>()�᭱��W.WitheAll<RotatingCube>()�άO.WitheNone<Player>()�A�e�̥N��u����RotatingCube�o��tag������������F��̥N��u�n�O��Player�o��tag������N���������
        foreach ((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotatingCube>())
        {

            //�g�@����code�A���զ��LBurst���t���C�sĶ���i����o����o�q��code�۰ʲ������C
            float power = 1f;
            for (int i = 0; i < 100000; i++)
            {
                power *= 2f;
                power /= 2f;
            }

            //�{�bforeach�����ڭ̫��w��component�F�A�ҥH�ڭ̲{�b��set up���󪺱���
            //�i�JlocakTransform���ValueRW�o�ӯ�Ū�g���ܼơA�b�i�JlocakTransform���ValueRO�o��"��Ū"���ܼơA�b�̭����RotateY()��k�C
            //RotateY()��k�����ѼơA�z�L�i�JrotateSpeed���ValueRO��value�ӧ���ƭȡA�åB�z�LSystemAPI.Time.DeltaTime�A���ƭ��H��frame rate�ܤ�
            //�̫��@�ѼƶǶiRotateY()�ARotateY()�B�槹��A�^�ǵ�locakTransform.ValueRW�C
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }

        //���F��deltaTime�s���ƭȡAnew�@��job�A�ե� SystemAPI.Time.DeltaTime��ƭȽᤩ��deltaTime�C�̫�b�Ǧ^rotatingCubeJob
        //������rotatingCubeJob����ScheduleParallel()�A�]�i�H�ϥ�Run()��debug������ĳ�A�]���|���W�b�D������B�@�C
        RotatingCubeJob rotatingCubeJob = new RotatingCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        rotatingCubeJob.ScheduleParallel();
    }


    //�ѩ�ϥ�Burst���M�u�bMain Thread�W���B�@�A�ӵL�k�h������B�@�A�]���ݭnJob��U
    //�]�mRotatingCubeJob�}���~��IJobEntity����Entity�B�@�A�ä@�˨ϥ�partial�Mstruct�A�ɶq���~��IJobParallelFor
    //��Job�MBurstCompile���X�A�|�]�X�̦n���ĪG�C
    [BurstCompile]
    //[WithNone(typeof(Player))] //���F���]�tPlayer�A�]�mWithNone�ݩʨó]�w�䤤��type��Player
    [WithAll(typeof(RotatingCube))]
    public partial struct RotatingCubeJob : IJobEntity
    {
        public float deltaTime;

        //�s�W�@�ӷs��Execute()��k�A�ѩ�Ѽƨӷ��N��ӡALocalTransform�MRotateSpeed�A�e���ref���ѦҪ��ƭȧ�g�Ain�h�O�����ѦҪ��ƭȧ�g
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            float power = 1f;
            for (int i = 0; i < 100000; i++)
            {
                power *= 2f;
                power /= 2f;
            }

            //�bjob�̭��ܼƫe���N���ݭnRefRW�MRefRO�F
            //�ѩ�IJobEntity�����\�ϥ�SystemAPI�A�]���]�m�@�Ӥ����ܼ�deltaTime�A�bOnUpdate()�̭���ƭȶǵ�deltaTime
            localTransform = localTransform.RotateY(rotateSpeed.value * deltaTime * power);
        }
    }
}
