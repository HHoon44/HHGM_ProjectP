using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectP.Util
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
    {
        public Transform holder;

        /// <summary>
        /// 풀에 담겨있는 오브젝트
        /// </summary>
        public List<T> Pool { get; private set; } = new List<T>();

        /// <summary>
        /// 재사용 가능 여부
        /// </summary>
        public bool canRecycle => Pool.Find(obj => obj.CanReCycle) != null;

        /// <summary>
        /// 풀에서 다스릴 오브젝트를 등록하는 메서드
        /// </summary>
        /// <param name="obj"></param>
        public void RegistPoolableObject(T obj)
        {
            Pool.Add(obj);
        }

        /// <summary>
        /// 사용한 오브젝트를 풀에 반환하는 메서드
        /// </summary>
        /// <param name="obj"></param>
        public void ReturnPoolableObject(T obj)
        {
            obj.transform.SetParent(holder);
            obj.gameObject.SetActive(false);
            obj.CanReCycle = false;
        }

        /// <summary>
        /// 풀에서 오브젝트를 반환해주는 메서드
        /// </summary>
        /// <param name="pred"></param>
        /// <returns></returns>
        public T GetPoolableObject(Func<T, bool> pred = null)
        {
            if (!canRecycle)
            {
                // 재사용 가능한 오브젝트가 없다면
                // 풀에 사용 가능한 오브젝트를 만들어주는 작업
                var protoObj = Pool.Count > 0 ? Pool[0] : null;

                if (protoObj != null)
                {
                    var newObj = GameObject.Instantiate(protoObj.gameObject, holder);
                    newObj.name = protoObj.name;
                    newObj.SetActive(true);

                    RegistPoolableObject(newObj.GetComponent<T>());
                }
                else
                {
                    return null;
                }
            }

            // 재사용 가능한 오브젝트가 존재하는지 확인하는 작업
            T recycleObj = (pred == null) ? (Pool.Count > 0 ? Pool[0] : null) : (Pool.Find(obj => pred(obj) && obj.CanReCycle));

            if (recycleObj == null)
            {
                return null;
            }

            recycleObj.CanReCycle = false;

            return recycleObj;
        }
    }
}