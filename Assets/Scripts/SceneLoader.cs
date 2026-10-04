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

    public void Exit()
    {
        Application.Quit();
    }
}
