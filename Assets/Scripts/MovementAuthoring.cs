using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MovementAuthoring : MonoBehaviour
{
    
}

public struct Movement : IComponentData
{
    public float3 movementVector;    //float3在dots和burst約等於vector3，走x,y,z
}
