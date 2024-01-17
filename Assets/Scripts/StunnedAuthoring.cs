using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


#region
//此腳本用來示範如何啟用、禁用component
#endregion
public class StunnedAuthoring : MonoBehaviour
{
    //啟用Entity，然後設定SetComponentEnabled<Stunned>()，為false，起始時就component就會是false
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

//如果要讓Stunned component能夠啟用、禁用的話，要多繼承IEnableableComponent
public struct Stunned : IComponentData, IEnableableComponent
{

}