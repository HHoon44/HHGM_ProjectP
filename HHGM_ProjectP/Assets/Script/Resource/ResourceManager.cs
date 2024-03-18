using ProjectP.Util;
using ProjectP.Define;
using System;
using UnityEngine;

namespace ProjectP.Resource
{
    /// <summary>
    /// 리소스 폴더에 존재하는 리소스들을 불러오는 클래스
    /// </summary>
    public class ResourceManager : Singleton<ResourceManager>
    {
        public void Initialize()
        {
            LoadAllAtlas();
            LoadAllPrefabs();
        }

        /// <summary>
        /// 리소스  폴더에 존재하는 오브젝트를 가져오는 메서드
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public GameObject LoadObject(string path)
        {
            return Resources.Load<GameObject>(path);
        }

        /// <summary>
        /// 인게임에서 사용할 모든 아틀라스를 불러오는 메서드
        /// </summary>
        private void LoadAllAtlas()
        {
            // 나중에 아틀라스 불러오기 작성
            // SpriteAtlas[] portraitAtlase = Resources.LoadAll<SpriteAtlas>("Atlase/PortraitAtlase");
            // SpriteLoader.SetAtlas(portraitAtlase);
        }

        /// <summary>
        /// 인게임에서 사용할 모든 프리팹을 불러오는 메서드
        /// </summary>
        public void LoadAllPrefabs()
        {
            // 나중에 프리팹 불러오기 작성
            // LoadPoolableObject<MonHpBar>(PoolType.None, "Prefabs/UI/MonHpBar", 3);
        }

        /// <summary>
        /// 불러온 오브젝트를 풀에 등록하는 메서드
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