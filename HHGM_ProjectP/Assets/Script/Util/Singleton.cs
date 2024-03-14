using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace ProjectP.Util
{
    /// <summary>
    /// �̱��� ���̽� Ŭ����
    /// </summary>
    /// <typeparam name="T"> �̱����� ������� �ϴ� �Ļ� Ŭ���� </typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        // �̱��� �ν��Ͻ��� ã�ų� ������ �ٸ� �����忡�� ��������� �Ǵ��� ��ü
        private static object syncObject = new object();

        private static T instance;

        // �ܺο��� �ν��Ͻ� ��ü�� �����ϱ� ���� ������Ƽ
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // �ν��Ͻ��� ���ٸ� �ٸ� �������� ��������� üũ
                    lock (syncObject)
                    {
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            // �׷��� �������� �ʴ´ٸ� ���ο� �ν��Ͻ��� ����

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