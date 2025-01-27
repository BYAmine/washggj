using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class mainmenu : MonoBehaviour
{

    public void PlayGame()
    {

        SceneManager.LoadSceneAsync(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
        public void Tutuo()
    {

        SceneManager.LoadSceneAsync(1);
    }
}