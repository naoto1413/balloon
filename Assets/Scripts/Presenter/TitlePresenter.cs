using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePresenter : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Play");
    }
}
