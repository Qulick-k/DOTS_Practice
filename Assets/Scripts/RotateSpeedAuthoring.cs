using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

//在RotateSpeedAuthoring這個腳本，就直接繼承MonoBehaviour即可
public class RotateSpeedAuthoring : MonoBehaviour
{
    //可以在這腳本增加任何型態的參數，譬如增加一個跟Component相同名字的參數

    public float value; //旋轉速度的數值

    //現在設置Class的Baker，讓Baker能從一個傳到另外一個
    private class Baker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            //接下來要新增Component，在新增之前需要entity的參考，並且需要TransformUsageFlags這個優化物件。如果設置為None的話，將不會轉變那些沒有entity transform的物件，如果不需要去移動entity或不需要position，其實也不需要設置transform component
            //但這練習檔需要讓cube旋轉，因此取代None的是Dynamic

            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //設置一個entity，存取GetEntity()回傳的Entity參考
            AddComponent(entity, new RotateSpeed{
                value = authoring.value} 
            ); //新增Component，傳入entity、傳入有RotateSpeedAuthoring屬性的authoring.value
            
        }
    }
}

//如果想簡化的話，也可以把RotateSpeed腳本和RotateSpeedAuthoring腳本，放在同一個腳本內
public struct RotateSpeed : IComponentData          //使用entities不需要繼承MonoBehaviour，也不需要使用class的類別
{
    //由於entities中的IcompponentData只會儲存資料，且無Logic，所以設置public即可
    public float value; //這是旋轉的速度  
}