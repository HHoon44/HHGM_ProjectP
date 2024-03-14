using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace ProjectP.Util
{
    /// <summary>
    /// 싱글톤 베이스 클래스
    /// </summary>
    /// <typeparam name="T"> 싱글톤을 만들고자 하는 파생 클래스 </typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        // 싱글톤 인스턴스를 찾거나 과정중 다른 스레드에서 사용중인지 판단할 객체
        private static object syncObject = new object();

        private static T instance;

        // 외부에서 인스턴스 객체에 접근하기 위한 프로퍼티
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // 인스턴스가 없다면 다른 과정에서 사용중인지 체크
                    lock (syncObject)
                    {
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            // 그래도 존재하지 않는다면 새로운 인스턴스를 생성

                            GameObject obj = new GameObject();
                            obj.name = typeof(T).Name;
                            instance = obj.AddComponent<T>();
                        }
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (instance != this)
            {
                return;
            }

            instance = null;
        }

        public static bool HasInstance()
        {
            return instance ? true : false;
        }
    }
}