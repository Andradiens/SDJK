using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float bpm = 130;
    public float secPerBeat;
    public float offset = 0.05f;
    public float songDelay = 1f;
    public double songPosition;
    public double songPositionInBeats;
    public double dspSongTime;
    public float scrollSpeed;

    private AudioSource musicSource;

    private int lastBeat = -1;

    private bool isPaused;
    private double pauseStartDspTime;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayBeatmap(AudioClip music, float bpm, float scrollSpeed)
    {
        musicSource.clip = music;
        this.scrollSpeed = scrollSpeed;
        this.bpm = bpm;
        secPerBeat = 60f / bpm;
        
        lastBeat = -1;
        songPosition = 0d;
        songPositionInBeats = 0d;

        dspSongTime = AudioSettings.dspTime + songDelay;

        musicSource.PlayScheduled(dspSongTime);
    }

    private void Update()
    {
        if (isPaused)
            return;

        songPosition = (double)(AudioSettings.dspTime - dspSongTime - offset);

        if (secPerBeat > 0)
            songPositionInBeats = songPosition / secPerBeat;
        else
            return;

        int currentBeat = Mathf.FloorToInt((float)songPositionInBeats);

        if (currentBeat > lastBeat)
        {
            lastBeat = currentBeat;
        }
    }

    public void PauseSong()
    {
        if (isPaused)
            return;

        isPaused = true;
        pauseStartDspTime = AudioSettings.dspTime;

        musicSource.Pause();
    }

    public void Resume()
    {
        if (!isPaused)
            return;

        double pauseDuration = AudioSettings.dspTime - pauseStartDspTime;

        dspSongTime += pauseDuration;

        musicSource.UnPause();

        isPaused = false;
    }
}