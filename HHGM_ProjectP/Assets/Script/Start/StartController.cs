using ProjectP_Define;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace ProjectP
{
    /// <summary>
    /// 시작 씬에서 게임 시작 전에 필요한 작업을 수행하는 클래스
    /// </summary>
    public class StartController : MonoBehaviour
    {

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

        private void OnPhase(IntroPhase phase)
        {

        }


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