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
    public float3 movementVector;    //float3�bdots�Mburst������vector3�A��x,y,z
}
