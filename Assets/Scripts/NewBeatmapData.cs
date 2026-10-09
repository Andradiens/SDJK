using UnityEngine;

[CreateAssetMenu(fileName = "New Beatmap", menuName =  "Rhythm Game/Beatmap")]
public class NewBeatmapData : ScriptableObject
{
    public string songName;
    public string artist;
    public string mapper;

    public string difficultyName;
    public float difficulty;
    public float bpm;
    public float scrollSpeed;

    public AudioClip music;
    public Sprite background;
    public TextAsset chart;
}