using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeatmapEditorManager : MonoBehaviour
{
    public TMP_InputField bpmInput;
    public TMP_InputField offsetInput;
    public TMP_Text statusTxt;
    public TMP_Dropdown beatmapDropdown;
    public List<NewBeatmapData> beatmapDataList;

    private void Start()
    {
        if (beatmapDataList == null)
        {
            Debug.LogError("beatmapDataList is null");
            return;
        }

        beatmapDropdown.ClearOptions();
        foreach (NewBeatmapData beatmap in beatmapDataList)
        {
            beatmapDropdown.AddOptions(new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData(beatmap.songName + " - " + beatmap.difficultyName)
            });
        }

        beatmapDropdown.onValueChanged.AddListener(OnBeatmapChange);
        OnBeatmapChange(0);
    }

    private void ChangeDetails(NewBeatmapData beatmap)
    {
        bpmInput.text = beatmap.bpm.ToString();
        offsetInput.text = (beatmap.offset * 1000).ToString();
        statusTxt.text = beatmap.songName;
    }

    private void OnBeatmapChange(int index)
    {
        if (beatmapDataList == null || index < 0 || index >= beatmapDataList.Count)
        {
            Debug.LogError("OnBeatmapChange error");
            return;
        }
        NewBeatmapData beatmapData = beatmapDataList[index];
        ChangeDetails(beatmapData);
    }
}
