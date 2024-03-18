using ProjectP.Util;
using ProjectP.Define;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectP.Util
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        /// <summary>
        /// ��� Ǯ�� ��Ƴ��� ��ųʸ�
        /// </summary>
        private Dictionary<PoolType, object> poolDic = new Dictionary<PoolType, object>();

        /// <summary>
        /// Ǯ�� �����ϴ� �޼���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="poolCount"></param>
        public void RegistPool<T>(PoolType type, T obj, int poolCount = 1) where T : MonoBehaviour, IPoolableObject
        {
            ObjectPool<T> pool = null;

            // ����ϰ��� �ϴ� ������Ʈ�� Ǯ�� �̹� �ִ��� Ȯ���ϴ� �۾�
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
        /// ��û���� Ǯ�� ��ȯ�ϴ� �޼���
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
        /// Ǯ�� �����ϴ� �޼���
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