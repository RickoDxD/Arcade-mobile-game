using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public void LoadMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadInstruction()
    {
        SceneManager.LoadScene(2);
    }
}
