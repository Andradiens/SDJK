using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class BeatmapEditorManager : MonoBehaviour
{
    public TMP_InputField bpmInput;
    public TMP_InputField offsetInput;
    public TMP_Text statusTxt;
    public TMP_Dropdown beatmapDropdown;
    public UnityEngine.UI.Button saveBtn;
    public List<NewBeatmapData> beatmapDataList;

    private NewBeatmapData currentBeatmap;

    private void Start()
    {
        if (beatmapDataList == null || beatmapDataList.Count == 0)
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

        bpmInput.onSubmit.AddListener(ValidateBpm);
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
        currentBeatmap = beatmapData;
        ChangeDetails(beatmapData);
    }

    private void ApplyMetadataChanges()
    {
        if (!float.TryParse(bpmInput.text, out float bpm) || bpm <= 0)
        {
            return;
        }
        if (!float.TryParse(offsetInput.text, out float offsetMilliseconds))
        {
            return;
        }
        float offset = offsetMilliseconds / 1000;
        
        currentBeatmap.bpm = bpm;
        currentBeatmap.offset = offset;
    }

    private void ValidateBpm(string value)
    {
        if (!float.TryParse(value, out float bpm) || bpm <= 0)
        {
            bpmInput.text = "BPM must be above 0!";
            return;
        }
    }
}
