using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private Button resumeBut, restartBut, menuBut;
    public Text titleText;
    private bool isPause = false;

    private GameObject pausePanel;
    void Start()
    {
        restartBut = GameObject.Find("RestartButton").GetComponent<Button>();
        resumeBut = GameObject.Find("ResumeButton").GetComponent<Button>();
        menuBut = GameObject.Find("MenuButton").GetComponent<Button>();
        titleText = GameObject.Find("TitleText").GetComponent<Text>();
        pausePanel = GameObject.Find("PausePanel");
        Time.timeScale = 1;

        restartBut.onClick.AddListener(Restart);
        resumeBut.onClick.AddListener(Resume);
        menuBut.onClick.AddListener(GoToMenu);
        pausePanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }

    public void SetPause()
    {
        isPause = !isPause;
        pausePanel.SetActive(isPause);
        Time.timeScale = isPause ? 0 : 1;
    }

    private void Resume()
    {
        SetPause();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
