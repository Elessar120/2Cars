using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TapsellSDK;

using Debug = UnityEngine.Debug;

public class UIManager : MonoBehaviour
{
  
    [SerializeField] private GameObject mainPanel;
  
    [SerializeField] private GameObject mainSettingPanel;

    [SerializeField] private GameObject gameoverUi;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseSetting;
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private GameObject[] lines;
    [SerializeField] private GameObject gameoverUiLoadingAds;
    [SerializeField] private GameObject gameoverUiNoInternetAds;
    [SerializeField]  private Text showAdError;
    private string adState;
    private bool _pauseUiCheck;
    //  public bool unFreeze;

    //  [SerializeField] private GameObject mainUI;

    // Start is called before the first frame update
    private static UIManager m_instance;

    public static UIManager M_Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new UIManager();
            }

            return m_instance;
        }
    }


    private UIManager()
    {
        m_instance = this;
    }

    public int score;
    public Text highScoreText;
    public int highScore;
    public Text gameoverScore;
    public bool mainMenuOn;
    public Text scoreText;
    public Text scoreTextPause;
    public Text highscoreTextPause;

    public void SetAdError(string Error)
    {
        adState = Error;
        showAdError.text = adState;

    }
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1;

    }

    void Awake()
    {
        highScore =
            PlayerPrefs.GetInt("HighScore", highScore);
    }

    private void Start()
    {
//        HighScoreText.text = "High Score : " + UIManager.M_Instance.HighScore;

    }

    void Update()
    {
       

        if (mainMenuOn)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void AddScore()
    {
        score++;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScore",highScore);
    }
    public void MainSetting()
    {
        mainPanel.SetActive(false);
        mainSettingPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainPanel.SetActive(true);
        mainSettingPanel.SetActive(false);
      
    }

    public void Pausegame()
    {
        Debug.Log("pause");
        Time.timeScale = 0;
        lines[0].SetActive(false);
        lines[1].SetActive(false);
        lines[2].SetActive(false);
        pauseUi.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        gameoverUi.SetActive(false);
        gameoverUiLoadingAds.SetActive(false);
        gameoverUiNoInternetAds.SetActive(false);
        if (score > highScore)
        {
            highScore = score;
        }

        scoreTextPause.text = "YOUR SCORE : " + UIManager.M_Instance.score;
        highscoreTextPause.text = "HIGH SCORE : " + UIManager.M_Instance.highScore;
        Debug.Log(highScore);
        Debug.Log(scoreTextPause);
        Debug.Log(highscoreTextPause);

    }
    public void Gameover()
    {
        Time.timeScale = 0;
        lines[0].SetActive(false);
        lines[1].SetActive(false);
        lines[2].SetActive(false);

        gameoverUi.SetActive(true);
       pauseButton.gameObject.SetActive(false);
       
    }

    public void GameoverLoadingAds()
    {
        gameoverUi.SetActive(false);
        gameoverUiNoInternetAds.SetActive(false);
        gameoverUiLoadingAds.SetActive(true);
        AdManager.M_Instance.Request();
    }

    public void GameoverNoInternetAds()
    {
        gameoverUiLoadingAds.SetActive(false);
        gameoverUiNoInternetAds.SetActive(true);

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        lines[0].SetActive(true);
        lines[1].SetActive(true);
        lines[2].SetActive(true);

        pauseUi.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        gameoverUi.SetActive(false);
        gameoverUiLoadingAds.SetActive(false);
        gameoverUiNoInternetAds.SetActive(false);

    }

    public void CloseAllMenues()
    {
        gameoverUi.SetActive(false);
        gameoverUiLoadingAds.SetActive(false);
        gameoverUiNoInternetAds.SetActive(false);
    }

    public void LoadMainMenu()
    {
        if (score > highScore)
        {
            highScore = score;
        }

        gameoverScore.text = "YOUR SCORE : " + UIManager.M_Instance.score;
        highScoreText.text = "HIGH SCORE : " + UIManager.M_Instance.highScore;
        SaveScore();
        SceneManager.LoadScene("MainMenu");
        
    }

    public void PauseSetting()
    {
        pauseUi.SetActive(false);
        pauseSetting.SetActive(true);
        
    }

    public void CloseInGameSoundSetting()
    {
        pauseUi.SetActive(true);
        pauseSetting.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
    /* public void FreezeCheck()
     {
         unFreeze = true;
     }*/

    // Update is called once per frame


    /* private void startMenuCheck()
     {
         Time.timeScale = 0;
         mainUI.SetActive(true);
         if (unFreeze)
         {
             mainUI.SetActive(false);
             Time.timeScale = 1;
         }
     }*/
}