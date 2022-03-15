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
    [SerializeField] Slider depthSlider;
    [SerializeField] Slider widthSlider;
    [SerializeField] TextMeshProUGUI depthText;
    [SerializeField] TextMeshProUGUI widthText;
    string depthBaseText = "Depth : ";
    string widthBaseText = "Width : ";
    int depthValue = 0;
    int widthValue = 0;
    private void Awake()
    {
        UpdateTexts();
    }

    public void LoadNextScene()
    {
        PlayerPrefs.SetInt("Width", widthValue);
        PlayerPrefs.SetInt("Depth", depthValue);
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

    public void UpdateWidthValue()
    {
        UpdateWidthText();
    }
    public void UpdateDepthValue()
    {
        UpdateDepthText();
    }
    private void UpdateWidthText()
    {
        if (widthText != null && widthSlider != null)
        {
            widthValue = (int)widthSlider.value;
            widthText.text = widthBaseText + widthValue.ToString();
        }
    }
    private void UpdateDepthText()
    {
        if (depthText != null && depthSlider != null)
        {
            depthValue = (int)depthSlider.value;
            depthText.text = depthBaseText + depthValue.ToString();
        }
    }
    void UpdateTexts()
    {
        UpdateWidthText();
        UpdateDepthText();
    }
}
