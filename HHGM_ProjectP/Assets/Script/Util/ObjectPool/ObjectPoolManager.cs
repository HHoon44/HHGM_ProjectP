using ProjectP.Util;
using ProjectP.Define;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectP.Util
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        /// <summary>
        /// 모든 풀을 담아놓을 딕셔너리
        /// </summary>
        private Dictionary<PoolType, object> poolDic = new Dictionary<PoolType, object>();

        /// <summary>
        /// 풀을 저장하는 메서드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="poolCount"></param>
        public void RegistPool<T>(PoolType type, T obj, int poolCount = 1) where T : MonoBehaviour, IPoolableObject
        {
            ObjectPool<T> pool = null;

            // 등록하고자 하는 오브젝트의 풀이 이미 있는지 확인하는 작업
            if (poolDic.ContainsKey(type))
            {
                pool = poolDic[type] as ObjectPool<T>;
            }
            else
            {
                pool = new ObjectPool<T>();
                poolDic.Add(type, pool);
            }

            if (pool.holder == null)
            {
                pool.holder = new GameObject($"{type.ToString()}Holder").transform;
                pool.holder.parent = transform;
                pool.holder.position = Vector3.zero;
            }

            for (int i = 0; i < poolCount; i++)
            {
                var poolableObj = Instantiate(obj);
                poolableObj.name = obj.name;
                poolableObj.transform.SetParent(pool.holder);
                poolableObj.gameObject.SetActive(true);

                pool.RegistPoolableObject(poolableObj);
            }
        }

        /// <summary>
        /// 요청받은 풀을 반환하는 메서드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public ObjectPool<T> GetPool<T>(PoolType type)
            where T : MonoBehaviour, IPoolableObject
        {
            if (!poolDic.ContainsKey(type))
            {
                return null;
            }

            return poolDic[type] as ObjectPool<T>;
        }

        /// <summary>
        /// 풀을 삭제하는 메서드
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        public void ClearPool<T>(PoolType type)
            where T : MonoBehaviour, IPoolableObject
        {
            var pool = GetPool<T>(type)?.Pool;

            if (pool == null)
            {
                return;
            }

            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i] != null)
                {
                    Destroy(pool[i].gameObject);
                }
            }

            pool.Clear();
        }
    }
}