using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

//PlayerShootManager�}�������b�C�������W��PlayerShootManager����
public class PlayerShootManager : MonoBehaviour
{

    //�]�i�H�ϹL�ӷQ�A��PlayerShootManager�g���@��Singleton�A
    public static PlayerShootManager Instance { get; private set; }

    [SerializeField] private GameObject shootPopupPrefab;


    //�@����N��}���ᤩ��Instance
    private void Awake()
    {
        Instance = this;
    }

    //�ѩ�SystemAPI�L�k�b�o�ϥΡA�ҥH�ϥ�World.DefaultGameObjectInjectionWorld<>()��PlayerShootingSystem��Singleton
    //����ᤩ��PlayerShootingSystem playerShootingSystem�A�����playerShootingSystem�q�\Event�C��k�W��PlayerShootingSystem_OnShoot
    //private void Start()
    //{
    //    PlayerShootingSystem playerShootingSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();

    //    playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
    //}


    //���sender�A�åB�ഫ���O��Entity�A�ᤩ��Entity playerEntity
    //�{�b���D�Ӧb���̥ͦ�prefab�A���ٻݭn���D���a����m�A�ҥH�ݭnaccess��player entity��localTransform
    //�o���z�LWorld.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<>()���w�n���playerEntity��component data�C
    //����A�ᤩ��localTransform�A����Instantiate(�w�s��,��m,��V)�ͦ�prefab�C��:float3�A�ͦ��ɡA�|�۰��ഫ��vector3�C
    //private void PlayerShootingSystem_OnShoot(object sender, System.EventArgs e)
    //{
    //    Entity playerEntity = (Entity)sender;
    //    LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
    //    Instantiate(shootPopupPrefab, localTransform.Position, Quaternion.identity);
    //}


    //�o�}����singleton���ɭԡA�N�i�H�Х�PlayerShoot()��k�A������J���a��m�ѼơA�M��I�s��k�ͦ�prefab
    //�o�˴N��bPlayerShootingSystem�}���W�A�����I�sPlayerShoot()�o�Ӥ�k�F
    public void PlayerShoot(Vector3 playerPosition)
    {
        Instantiate(shootPopupPrefab, playerPosition, Quaternion.identity);
    }
}
