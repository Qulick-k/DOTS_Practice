using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    //製作Baker，Baker的type放入PlayerAuthoring
    public class Baker : Baker<PlayerAuthoring>
    {
        //在Bake()方法中新增新的Component
        //用GetEntity()抓取entity回傳給Entity entity，由於玩家會動，所以選擇Dynamic
        //最後使用AddComponent()增加Component
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Player());
        }
    }
}

//下面的struct就只是當作一個component的tag，空空的即可
public struct Player : IComponentData  
{

}
