using ProjectP.Util;
using UnityEngine;

namespace ProjectP
{
    /// <summary>
    /// ������ ��ü �帧�� ��Ʈ���ϴ� �Ŵ���
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public float loadProgress;      // ���� ���� ���� �غ� �ۼ�Ʈ ��

        protected override void Awake()
        {
            // �̱��濡 �����ϴ� Awake ����
            base.Awake();

            if (gameObject == null)
            {
                return;
            }

            // ���� ���� �Ǿ �ı� ���� �ʵ���
            DontDestroyOnLoad(gameObject);

            StartController startController = FindObjectOfType<StartController>();
            startController?.Initalize();
        }

        /// <summary>
        /// ���� �⺻ ������ ����ϴ� �޼���
        /// </summary>
        public void OnApplicationSetting()
        {
            // ���� ����ȭ Off
            QualitySettings.vSyncCount = 0;

            // ���� ������ 60 Set
            Application.targetFrameRate = 60;

            // ȭ�� ��ȣ�� Off
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void LoadScene()
        { 
            
        }

        public void OnAddtiveLoadingScene()
        { 
        
        }
    }
}