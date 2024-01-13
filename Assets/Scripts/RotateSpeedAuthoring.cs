using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

//�bRotateSpeedAuthoring�o�Ӹ}���A�N�����~��MonoBehaviour�Y�i
public class RotateSpeedAuthoring : MonoBehaviour
{
    //�i�H�b�o�}���W�[���󫬺A���ѼơAĴ�p�W�[�@�Ӹ�Component�ۦP�W�r���Ѽ�

    public float value; //����t�ת��ƭ�

    //�{�b�]�mClass��Baker�A��Baker��q�@�ӶǨ�t�~�@��
    private class Baker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            //���U�ӭn�s�WComponent�A�b�s�W���e�ݭnentity���ѦҡA�åB�ݭnTransformUsageFlags�o���u�ƪ���C�p�G�]�m��None���ܡA�N���|���ܨ��ǨS��entity transform������A�p�G���ݭn�h����entity�Τ��ݭnposition�A���]���ݭn�]�mtransform component
            //���o�m���ɻݭn��cube����A�]�����NNone���ODynamic

            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //�]�m�@��entity�A�s��GetEntity()�^�Ǫ�Entity�Ѧ�
            AddComponent(entity, new RotateSpeed{
                value = authoring.value} 
            ); //�s�WComponent�A�ǤJentity�B�ǤJ��RotateSpeedAuthoring�ݩʪ�authoring.value
            
        }
    }
}

//�p�G�Q²�ƪ��ܡA�]�i�H��RotateSpeed�}���MRotateSpeedAuthoring�}���A��b�P�@�Ӹ}����
public struct RotateSpeed : IComponentData          //�ϥ�entities���ݭn�~��MonoBehaviour�A�]���ݭn�ϥ�class�����O
{
    //�ѩ�entities����IcompponentData�u�|�x�s��ơA�B�LLogic�A�ҥH�]�mpublic�Y�i
    public float value; //�o�O���઺�t��  
}