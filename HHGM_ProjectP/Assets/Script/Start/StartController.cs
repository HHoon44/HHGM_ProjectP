using ProjectP_Define;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace ProjectP
{
    /// <summary>
    /// ���� ������ ���� ���� ���� �ʿ��� �۾��� �����ϴ� Ŭ����
    /// </summary>
    public class StartController : MonoBehaviour
    {

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