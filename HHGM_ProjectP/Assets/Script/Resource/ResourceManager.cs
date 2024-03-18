using ProjectP.Util;
using ProjectP.Define;
using System;
using UnityEngine;

namespace ProjectP.Resource
{
    /// <summary>
    /// ���ҽ� ������ �����ϴ� ���ҽ����� �ҷ����� Ŭ����
    /// </summary>
    public class ResourceManager : Singleton<ResourceManager>
    {
        public void Initialize()
        {
            LoadAllAtlas();
            LoadAllPrefabs();
        }

        /// <summary>
        /// ���ҽ�  ������ �����ϴ� ������Ʈ�� �������� �޼���
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public GameObject LoadObject(string path)
        {
            return Resources.Load<GameObject>(path);
        }

        /// <summary>
        /// �ΰ��ӿ��� ����� ��� ��Ʋ�󽺸� �ҷ����� �޼���
        /// </summary>
        private void LoadAllAtlas()
        {
            // ���߿� ��Ʋ�� �ҷ����� �ۼ�
            // SpriteAtlas[] portraitAtlase = Resources.LoadAll<SpriteAtlas>("Atlase/PortraitAtlase");
            // SpriteLoader.SetAtlas(portraitAtlase);
        }

        /// <summary>
        /// �ΰ��ӿ��� ����� ��� �������� �ҷ����� �޼���
        /// </summary>
        public void LoadAllPrefabs()
        {
            // ���߿� ������ �ҷ����� �ۼ�
            // LoadPoolableObject<MonHpBar>(PoolType.None, "Prefabs/UI/MonHpBar", 3);
        }

        /// <summary>
        /// �ҷ��� ������Ʈ�� Ǯ�� ����ϴ� �޼���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="poolType"></param>
        /// <param name="path"></param>
        /// <param name="poolCount"></param>
        /// <param name="loadComplete"></param>
        public void LoadPoolableObject<T>(PoolType poolType, string path, int poolCount = 1, Action loadComplete = null)
            where T : MonoBehaviour, IPoolableObject
        {
            GameObject obj = LoadObject(path);

            var tComponent = obj.GetComponent<T>();

            ObjectPoolManager.Instance.RegistPool<T>(poolType, tComponent, poolCount);

            loadComplete?.Invoke();
        }
    }
}