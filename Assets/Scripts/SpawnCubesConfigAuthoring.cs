using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

//利用Prefab生成Entity到遊戲場景裡面。在那之前需要存取reference到我們的Prefab裡。
//因此創建一個Component存取，再透過system使用那個Component
//這腳本放到遊戲場景中的SpawnCubesConfig物件
public class SpawnCubesConfigAuthoring : MonoBehaviour
{
    //照常規宣告GameObject和int
    public GameObject cubePrefab;
    public int amountToSpawn;

    //為了把GameObject Prefab轉換成Entity Prefab，接著創建Baker
    //設置一個Entity entity存放GetEntity()回傳的值
    //由於現在使用的config component就真的只是一個data component，code monkey不希望它存在，因此他選擇使用TransformUsageFlags.None
    public class Baker : Baker<SpawnCubesConfigAuthoring>
    {
        public override void Bake(SpawnCubesConfigAuthoring authoring)  //authoring當作一個SpawnCubesConfigAuthoring腳本物件
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new SpawnCubesConfig
            {
                //SpawnCubesConfig裡面的entity抓取，一樣使用GetEntity，把SpawnCubesConfigAuthoring中GameObject的cubePrefab，設定為TransformUsageFlags.Dynamic，之後回傳給SpawnCubesConfig中Entity的cubePrefabEntity
                //SpawnCubesConfig裡面的int抓取，就很簡單，抓取把SpawnCubesConfigAuthoring中int的amountToSpawn，直接回傳給SpawnCubesConfig中int的amountToSpawn即可
                cubePrefabEntity = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn,
            });
        }
    }
}

//作一個Entity Component
public struct SpawnCubesConfig : IComponentData
{
    //宣告Entity來取代常規宣告的GameObject    
    public Entity cubePrefabEntity;
    public int amountToSpawn;
}
