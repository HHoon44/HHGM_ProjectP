using ProjectP.Define;
using ProjectP.UI;
using System.Collections;
using UnityEngine;

namespace ProjectP
{
    /// <summary>
    /// 시작 씬에서 게임 시작 전에 필요한 작업을 수행하는 클래스
    /// </summary>
    public class StartController : MonoBehaviour
    {
        public UIStart uiStart;

        private bool allLoaded;                                 // 로드가 마무리 되었는지에 대한 진리값
        private IntroPhase introPhase = IntroPhase.Start;       // 현재 페이즈 값
        private Coroutine loadGaugeUpdateCoroutine;             // 로딩 상태를 나타낼 게이지바를 다스리는 코루틴
        private bool loadComplete;                              // 현재 페이즈가 로드 되었는지에 대한 진리값

        /// <summary>
        /// 현재 페이즈 종료 시 다음 페이즈에 대한 요청을 보내는 프로퍼티
        /// </summary>
        public bool LoadComplete
        {
            get => loadComplete;
            set
            {
                loadComplete = value;

                if (loadComplete && !allLoaded)
                {
                    // 다음 페이즈를 불러오는 작업
                    NextPhase();
                }
            }
        }

        /// <summary>
        /// 첫 페이즈를 불러오는 메서드
        /// </summary>
        public void Initalize()
        {
            OnPhase(introPhase);
        }

        /// <summary>
        /// 현재 페이즈에 대한 작업을 요청하는 메서드
        /// </summary>
        /// <param name="phase"></param>
        private void OnPhase(IntroPhase phase)
        {
            // 현재 진행 상황을 UI에 띄우는 작업
            uiStart.SetLoadStateDescription(phase.ToString());

            if (loadGaugeUpdateCoroutine != null)
            {
                // 진행 상황을 띄우는 코루틴이 이미 진행중이라면 코루틴을 멈추는 작업
                StopCoroutine(loadGaugeUpdateCoroutine);
                loadGaugeUpdateCoroutine = null;
            }

            if (phase != IntroPhase.Complete)
            {
                float loadPer = (float)phase / (float)IntroPhase.Complete;

                // 현재 로딩 게이지 값을 전달하는 작업
                loadGaugeUpdateCoroutine = StartCoroutine(uiStart.LoadGaugeUpdate(loadPer));
            }
            else
            {
                // 완료 되었다면 게이지를 다 채우는 작업
                uiStart.loadFillGauge.fillAmount = 1f;
            }

            switch (phase)
            {
                case IntroPhase.Start:
                    LoadComplete = true;
                    break;

                case IntroPhase.ApplicationSetting:
                    GameManager.Instance.OnApplicationSetting();
                    break;

                case IntroPhase.Resource:
                    break;

                case IntroPhase.UI:
                    break;

                case IntroPhase.Complete:
                    allLoaded = true;
                    LoadComplete = true;
                    break;
            }
        }

        /// <summary>
        /// 다음 페이즈로 변경하는 메서드
        /// </summary>
        private void NextPhase()
        {
            StartCoroutine(WaitForSeconds());

            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(.5f);
                LoadComplete = false;
                OnPhase(++introPhase);
            }
        }
    }
}