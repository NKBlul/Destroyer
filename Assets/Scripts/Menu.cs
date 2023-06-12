using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Invoke_Play()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void Exit_Game()
    {
        Application.Quit();
    }
}
