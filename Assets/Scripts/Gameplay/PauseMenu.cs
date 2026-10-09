using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    public InputActionReference escape;
    public Conductor conductor;

    private void Start()
    {
        container.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        escape.action.Enable();
    }

    private void OnDisable()
    {
        escape.action.Disable();
    }

    private void Update()
    {
        if (escape.action.WasPressedThisFrame())
        {
            if (container.activeSelf)
            {
                ResumeBtn();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        container.SetActive(true);
        Time.timeScale = 0f;

        conductor.PauseSong();
    }

    public void ResumeBtn()
    {
        container.SetActive(false);
        Time.timeScale = 1f;

        conductor.Resume();
    }

    public void ExitSongBtn()
    {
        SceneManager.LoadScene("BeatmapList");
        Time.timeScale = 1f;
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}