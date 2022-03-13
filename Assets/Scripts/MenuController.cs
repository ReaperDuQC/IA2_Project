using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField; 
    [SerializeField] Button startButton;
    [SerializeField] Button leaderBoardButton;
    [SerializeField] Button quitButton;






    public void LoadNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void ShowInputfield()
    {
        startButton.gameObject.SetActive(!startButton.IsActive());
        leaderBoardButton.gameObject.SetActive(!leaderBoardButton.IsActive());
        quitButton.gameObject.SetActive(!quitButton.IsActive());
        inputField.gameObject.SetActive(!inputField.IsActive());
    }
    public void SaveNameAndLaunchGame()
    {
        if(inputField.text.Length > 0)
        {
            PlayerPrefs.SetString("Name", inputField.text);
            LoadNextScene(); 
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
