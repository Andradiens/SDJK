using UnityEngine;
using UnityEngine.SceneManagement;

public class SongEnd : MonoBehaviour
{
    public Conductor conductor;
    public AudioSource music;
    public Judge judge;
    private bool isEnd = false;

    public float endDelay;

    private void Update()
    {
        if (isEnd)
        {
            return;
        }

        if (conductor.songPosition > music.clip.length + endDelay)
        {
            isEnd = true;
            judge.SaveResults();
            SceneManager.LoadScene("ResultsScreen");
        }
    }
}