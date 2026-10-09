using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatmapItemUI : MonoBehaviour
{
    public NewBeatmapData music;
    public TMP_Text songTitle;
    public TMP_Text artistName;
    public Button beatmapButton;
    public BeatmapListManager beatmapListManager;

    public void Setup(NewBeatmapData music, BeatmapListManager beatmapListManager)
    {
        this.beatmapListManager = beatmapListManager;
        this.music = music;
        songTitle.text = music.songName;
        artistName.text = music.artist;

        beatmapButton.onClick.AddListener(BeatmapClicked);
    }

    public void BeatmapClicked()
    {
        beatmapListManager.BeatmapDetailsUpdate(music);
    }
}