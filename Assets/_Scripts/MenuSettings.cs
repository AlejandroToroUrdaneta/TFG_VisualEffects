using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
    
    public void AboutProject()
    {
        
    }
    

    public void ExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
