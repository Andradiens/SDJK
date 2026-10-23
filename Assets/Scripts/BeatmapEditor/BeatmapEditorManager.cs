using System.Collections.Generic;
using System.IO;
using TMPro;
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

        saveBtn.onClick.AddListener(SaveBeatmap);
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
        LoadBeatmapMetadata();
        ChangeDetails(beatmapData);
    }

    private bool ApplyMetadataChanges()
    {
        if (!float.TryParse(bpmInput.text, out float bpm) || bpm <= 0)
        {
            bpmInput.text = "BPM must be above 0!";
            return false;
        }
        if (!float.TryParse(offsetInput.text, out float offsetMilliseconds))
        {
            return false;
        }
        float offset = offsetMilliseconds/1000;

        currentBeatmap.bpm = bpm;
        currentBeatmap.offset = offset;
        return true;
    }

    private void ValidateBpm(string value)
    {
        if (!float.TryParse(value, out float bpm) || bpm <= 0)
        {
            bpmInput.text = "BPM must be above 0!";
            return;
        }
    }

    private string GetMetadataFilePath()
    {
        string gameDir = Application.persistentDataPath;
        string jsonDir = Path.Combine(gameDir, "BeatmapMetadataSave");
        if (!Directory.Exists(jsonDir))
        {
            Directory.CreateDirectory(jsonDir);
        }
        string fileName = $"{currentBeatmap.songName}_{currentBeatmap.difficultyName}.json";
        return Path.Combine(jsonDir, fileName);
    }

    private void SaveBeatmap()
    {
        if (ApplyMetadataChanges() == false)
        {   
            Debug.LogError("ApplyMetadataChanges returned False");
            return;
        }

        BeatmapMetadataSave metadataSave = new BeatmapMetadataSave
        {
            beatmapName = currentBeatmap.songName,
            beatmapDifficulty = currentBeatmap.difficultyName,
            bpm = currentBeatmap.bpm,
            offset = currentBeatmap.offset
        };

        string json = JsonUtility.ToJson(metadataSave);
        string filePath = GetMetadataFilePath();

        File.WriteAllText(filePath, json);
        statusTxt.text = "Beatmap Saved!";
        Debug.Log(filePath);
    }

    private void LoadBeatmapMetadata()
    {
        if (currentBeatmap == null)
        {
            Debug.LogError("currentBeatmap is null");
            return;
        }
        string filePath = GetMetadataFilePath();
        
        if (!File.Exists(filePath))
        {
            return;
        }

        string beatmapMetadataText = File.ReadAllText(filePath);
        BeatmapMetadataSave beatmapMetadata = JsonUtility.FromJson<BeatmapMetadataSave>(beatmapMetadataText);

        if (beatmapMetadata == null)
        {
            Debug.LogError("beatmapMetadata is null");
            return;
        }

        currentBeatmap.bpm = beatmapMetadata.bpm;
        currentBeatmap.offset = beatmapMetadata.offset;
    }
}
