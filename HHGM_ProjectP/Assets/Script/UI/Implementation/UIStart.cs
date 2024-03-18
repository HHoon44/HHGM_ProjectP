using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectP.UI
{
    /// <summary>
    /// 게임 시작 화면에 대한 UI들을 관리하는 클래스
    /// </summary>
    public class UIStart : MonoBehaviour
    {
        public Text loadStateDesc;      // 로딩바 텍스트
        public Image loadFillGauge;     // 로딩바 이미지
        public Image backImage;         // 설정할 백 이미지

        private void Start()
        {

        }

        /// <summary>
        /// 현재 진행 상태를 알려주는 메서드
        /// </summary>
        /// <param name="loadState"></param>
        public void SetLoadStateDescription(string loadState)
        {
            loadStateDesc.text = $"Load{loadState}...";
        }

        /// <summary>
        /// 현재 진행 상태를 값으로 표현할 메서드
        /// </summary>
        /// <param name="loadPer"></param>
        /// <returns></returns>
        public IEnumerator LoadGaugeUpdate(float loadPer)
        {
            /// Approximately : 두개의 값의 근사값을 측정

            // 두개의 값이 같아질 때 까지 반복하는 작업
            while (!Mathf.Approximately(loadFillGauge.fillAmount, loadPer))
            {
                loadFillGauge.fillAmount = Mathf.Lerp(loadFillGauge.fillAmount, loadPer, Time.deltaTime * 2f);
                yield return null;
            }
        }
    }
}