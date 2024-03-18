using ProjectP.Define;
using ProjectP.UI;
using System.Collections;
using UnityEngine;

namespace ProjectP
{
    /// <summary>
    /// ���� ������ ���� ���� ���� �ʿ��� �۾��� �����ϴ� Ŭ����
    /// </summary>
    public class StartController : MonoBehaviour
    {
        public UIStart uiStart;

        private bool allLoaded;                                 // �ε尡 ������ �Ǿ������� ���� ������
        private IntroPhase introPhase = IntroPhase.Start;       // ���� ������ ��
        private Coroutine loadGaugeUpdateCoroutine;             // �ε� ���¸� ��Ÿ�� �������ٸ� �ٽ����� �ڷ�ƾ
        private bool loadComplete;                              // ���� ����� �ε� �Ǿ������� ���� ������

        /// <summary>
        /// ���� ������ ���� �� ���� ����� ���� ��û�� ������ ������Ƽ
        /// </summary>
        public bool LoadComplete
        {
            get => loadComplete;
            set
            {
                loadComplete = value;

                if (loadComplete && !allLoaded)
                {
                    // ���� ����� �ҷ����� �۾�
                    NextPhase();
                }
            }
        }

        /// <summary>
        /// ù ����� �ҷ����� �޼���
        /// </summary>
        public void Initalize()
        {
            OnPhase(introPhase);
        }

        /// <summary>
        /// ���� ����� ���� �۾��� ��û�ϴ� �޼���
        /// </summary>
        /// <param name="phase"></param>
        private void OnPhase(IntroPhase phase)
        {
            // ���� ���� ��Ȳ�� UI�� ���� �۾�
            uiStart.SetLoadStateDescription(phase.ToString());

            if (loadGaugeUpdateCoroutine != null)
            {
                // ���� ��Ȳ�� ���� �ڷ�ƾ�� �̹� �������̶�� �ڷ�ƾ�� ���ߴ� �۾�
                StopCoroutine(loadGaugeUpdateCoroutine);
                loadGaugeUpdateCoroutine = null;
            }

            if (phase != IntroPhase.Complete)
            {
                float loadPer = (float)phase / (float)IntroPhase.Complete;

                // ���� �ε� ������ ���� �����ϴ� �۾�
                loadGaugeUpdateCoroutine = StartCoroutine(uiStart.LoadGaugeUpdate(loadPer));
            }
            else
            {
                // �Ϸ� �Ǿ��ٸ� �������� �� ä��� �۾�
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
        /// ���� ������� �����ϴ� �޼���
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