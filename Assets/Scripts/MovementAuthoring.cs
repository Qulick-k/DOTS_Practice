using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MovementAuthoring : MonoBehaviour
{
    //�s�@Baker
    public class Baker : Baker<MovementAuthoring>
    {
        public override void Bake(MovementAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Movement
            {
                //code monkey���n�ϥ�UnityEngine.Random
                movementVector = new float3(UnityEngine.Random.Range(-1f, +1f), 0 , UnityEngine.Random.Range(-1f, +1f))
            });
        }
    }
}

public struct Movement : IComponentData
{
    public float3 movementVector;    //float3�bdots�Mburst������vector3�A��x,y,z
}
