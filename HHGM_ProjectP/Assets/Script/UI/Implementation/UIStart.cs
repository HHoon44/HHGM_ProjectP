using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectP_UI
{
    /// <summary>
    /// ���� ���� ȭ�鿡 ���� UI���� �����ϴ� Ŭ����
    /// </summary>
    public class UIStart : MonoBehaviour
    {
        public Text loadStateDesc;      // �ε��� �ؽ�Ʈ
        public Image loadFillGauge;     // �ε��� �̹���
        public Image backImage;         // ������ �� �̹���

        private void Start()
        {

        }

        /// <summary>
        /// ���� ���� ���¸� �˷��ִ� �޼���
        /// </summary>
        /// <param name="loadState"></param>
        public void SetLoadStateDescription(string loadState)
        {
            loadStateDesc.text = $"Load{loadState}...";
        }

        /// <summary>
        /// ���� ���� ���¸� ������ ǥ���� �޼���
        /// </summary>
        /// <param name="loadPer"></param>
        /// <returns></returns>
        public IEnumerator LoadGaugeUpdate(float loadPer)
        {
            /// Approximately : �ΰ��� ���� �ٻ簪�� ����

            // �ΰ��� ���� ������ �� ���� �ݺ��ϴ� �۾�
            while (!Mathf.Approximately(loadFillGauge.fillAmount, loadPer))
            {
                loadFillGauge.fillAmount = Mathf.Lerp(loadFillGauge.fillAmount, loadPer, Time.deltaTime * 2f);
                yield return null;
            }
        }
    }
}