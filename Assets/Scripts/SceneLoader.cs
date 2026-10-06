using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("MusicPlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Play()
    {
        SceneManager.LoadScene("MusicPlay");
    }

    public void Edit()
    {
        SceneManager.LoadScene("BeatmapEdit");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
