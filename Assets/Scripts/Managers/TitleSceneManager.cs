using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class TitleSceneManager : MonoBehaviour
    {
        public void OnStartButton()
        {
            SceneManager.LoadScene("Play");
        }
    }
}

