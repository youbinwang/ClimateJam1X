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
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(false); 
        Time.timeScale = 1f;
        isPaused = false;
        isShowingControls = false;
    }

    public void PauseGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadTitleScreen()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Title"); 
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ToggleControls()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        isShowingControls = !isShowingControls;
        controlsUI.SetActive(isShowingControls);
    }
}