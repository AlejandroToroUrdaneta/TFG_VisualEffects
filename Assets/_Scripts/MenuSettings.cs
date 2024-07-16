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

    public void HelpButton()
    {
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void AboutMeButton()
    {
        
    }

    public void CreditsButton()
    {
        
    }

    public void AboutProject()
    {
        
    }

    public void ResumeButton()
    {
        
    }

    public void ExitGameButton()
    {
        SceneManager.LoadScene(0);
    }
}
