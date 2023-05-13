using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject LevelSelectionCanvas;
    [SerializeField] private string _gameplayPath;
    
    public void OpenLevelSelection()
    {
        MainMenuCanvas.SetActive(false);
        LevelSelectionCanvas.SetActive(true);
    }

    public void OpenGameplay()
    {
        SceneManager.LoadScene(_gameplayPath);
    }
}
