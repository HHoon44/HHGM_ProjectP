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
        /// Ǯ�� ����ִ� ������Ʈ
        /// </summary>
        public List<T> Pool { get; private set; } = new List<T>();

        /// <summary>
        /// ���� ���� ����
        /// </summary>
        public bool canRecycle => Pool.Find(obj => obj.CanReCycle) != null;

        /// <summary>
        /// Ǯ���� �ٽ��� ������Ʈ�� ����ϴ� �޼���
        /// </summary>
        /// <param name="obj"></param>
        public void RegistPoolableObject(T obj)
        {
            Pool.Add(obj);
        }

        /// <summary>
        /// ����� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="obj"></param>
        public void ReturnPoolableObject(T obj)
        {
            obj.transform.SetParent(holder);
            obj.gameObject.SetActive(false);
            obj.CanReCycle = false;
        }

        /// <summary>
        /// Ǯ���� ������Ʈ�� ��ȯ���ִ� �޼���
        /// </summary>
        /// <param name="pred"></param>
        /// <returns></returns>
        public T GetPoolableObject(Func<T, bool> pred = null)
        {
            if (!canRecycle)
            {
                // ���� ������ ������Ʈ�� ���ٸ�
                // Ǯ�� ��� ������ ������Ʈ�� ������ִ� �۾�
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

            // ���� ������ ������Ʈ�� �����ϴ��� Ȯ���ϴ� �۾�
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