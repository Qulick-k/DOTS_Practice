using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MovementAuthoring : MonoBehaviour
{
    //製作Baker
    public class Baker : Baker<MovementAuthoring>
    {
        public override void Bake(MovementAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Movement
            {
                //code monkey偏好使用UnityEngine.Random
                movementVector = new float3(UnityEngine.Random.Range(-1f, +1f), 0 , UnityEngine.Random.Range(-1f, +1f))
            });
        }
    }
}

public struct Movement : IComponentData
{
    public float3 movementVector;    //float3在dots和burst約等於vector3，走x,y,z
}
