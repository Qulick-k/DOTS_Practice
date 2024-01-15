using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    //�s�@Baker�ABaker��type��JPlayerAuthoring
    public class Baker : Baker<PlayerAuthoring>
    {
        //�bBake()��k���s�W�s��Component
        //��GetEntity()���entity�^�ǵ�Entity entity�A�ѩ󪱮a�|�ʡA�ҥH���Dynamic
        //�̫�ϥ�AddComponent()�W�[Component
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Player());
        }
    }
}

//�U����struct�N�u�O��@�@��component��tag�A�ŪŪ��Y�i
public struct Player : IComponentData  
{

}
