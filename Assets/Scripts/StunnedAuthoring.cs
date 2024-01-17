using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


#region
//���}���Ψӥܽd�p��ҥΡB�T��component
#endregion
public class StunnedAuthoring : MonoBehaviour
{
    //�ҥ�Entity�A�M��]�wSetComponentEnabled<Stunned>()�A��false�A�_�l�ɴNcomponent�N�|�Ofalse
    public class Baker : Baker<StunnedAuthoring>
    {
        public override void Bake(StunnedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Stunned());
            SetComponentEnabled<Stunned>(entity, false);
        }
    }
}

//�p�G�n��Stunned component����ҥΡB�T�Ϊ��ܡA�n�h�~��IEnableableComponent
public struct Stunned : IComponentData, IEnableableComponent
{

}