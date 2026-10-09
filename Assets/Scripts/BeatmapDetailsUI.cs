using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatmapDetailsUI : MonoBehaviour
{
    public Image beatmapCover;
    public TMP_Text songTitle;
    public TMP_Text artistName;
    public TMP_Text information;
    public Button playBtn;

    public void DetailsUpdate(NewBeatmapData details)
    {
        beatmapCover.sprite = details.background;
        songTitle.text = details.songName;
        artistName.text = details.artist;
        information.text = $"\nBPM: {details.bpm} \n\n Difficulty: {details.difficultyName} ({details.difficulty}) \n\n Mapper: {details.mapper}";
    
        beatmapCover.gameObject.SetActive(true);
        songTitle.gameObject.SetActive(true);
        artistName.gameObject.SetActive(true);
        information.gameObject.SetActive(true);
        playBtn.gameObject.SetActive(true);
    }
}
