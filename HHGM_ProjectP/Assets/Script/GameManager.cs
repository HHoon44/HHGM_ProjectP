using ProjectP.Util;
using UnityEngine;

namespace ProjectP
{
    /// <summary>
    /// 게임의 전체 흐름을 컨트롤하는 매니저
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public float loadProgress;      // 다음 씬에 대한 준비 퍼센트 값

        protected override void Awake()
        {
            // 싱글톤에 존재하는 Awake 실행
            base.Awake();

            if (gameObject == null)
            {
                return;
            }

            // 씬이 변경 되어도 파괴 되지 않도록
            DontDestroyOnLoad(gameObject);

            StartController startController = FindObjectOfType<StartController>();
            startController?.Initalize();
        }

        /// <summary>
        /// 앱의 기본 설정을 담당하는 메서드
        /// </summary>
        public void OnApplicationSetting()
        {
            // 수직 동기화 Off
            QualitySettings.vSyncCount = 0;

            // 랜덤 프레임 60 Set
            Application.targetFrameRate = 60;

            // 화면 보호기 Off
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