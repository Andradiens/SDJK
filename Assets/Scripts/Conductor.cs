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
    public float scrollSpeed = 3f;

    private AudioSource musicSource;
    public AudioSource audioSource;

    private int lastBeat = -1;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();

        secPerBeat = 60f / bpm;

        dspSongTime = AudioSettings.dspTime + songDelay;

        musicSource.PlayScheduled(dspSongTime);
    }

    private void Update()
    {
        songPosition = (double)(AudioSettings.dspTime - dspSongTime - offset);

        songPositionInBeats = songPosition / secPerBeat;

        int currentBeat = Mathf.FloorToInt((float)songPositionInBeats);

        if (currentBeat > lastBeat)
        {
            audioSource.PlayOneShot(audioSource.clip);
            lastBeat = currentBeat;
        }
    }
}