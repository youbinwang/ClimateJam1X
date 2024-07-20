using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject controlsUI; 
    private bool isPaused = false;
    private bool isShowingControls = false; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (isShowingControls)
                {
                    ToggleControls(); 
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(false); 
        Time.timeScale = 1f;
        isPaused = false;
        isShowingControls = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadTitleScreen()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Title"); 
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ToggleControls()
    {
        isShowingControls = !isShowingControls;
        controlsUI.SetActive(isShowingControls);
    }
}