using UnityEngine;
using UnityEngine.SceneManagement;

public class SongEnd : MonoBehaviour
{
    public Conductor conductor;
    public Judge judge;
    public float endDelay;

    private bool isEnd = false;

    private AudioSource music;

    private void Awake()
    {
        if (conductor != null)
        {
            music = conductor.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (isEnd || conductor == null || judge == null || music == null)
            return;

        if (music.clip == null)
            return;

        if (!music.isPlaying && conductor.songPosition < music.clip.length)
            return;

        if (conductor.songPosition > music.clip.length + endDelay)
        {
            isEnd = true;
            judge.SaveResults();
            SceneManager.LoadScene("ResultsScreen");
        }
    }
}
