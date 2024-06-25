using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProejctP.UI
{
    public class UIMain : MonoBehaviour
    {
        public void StatBtnFun()
        {
            SceneManager.LoadScene("Ingame");
        }
    }
}