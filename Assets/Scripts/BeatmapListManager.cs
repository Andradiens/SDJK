using System.Collections.Generic;
using UnityEngine;

public class BeatmapListManager : MonoBehaviour
{
    public List<NewBeatmapData> beatmapDataList;
    public BeatmapItemUI beatmapBtnPrefab;
    public Transform beatmapButtonPos;
    public NewBeatmapData selectedBeatmap;

    public void Start()
    {
        for (int x = 0; x < beatmapDataList.Count; x++)
        {
            BeatmapItemUI currentBeatmap = Instantiate(beatmapBtnPrefab, beatmapButtonPos);
            currentBeatmap.Setup(beatmapDataList[x], this);
        }
    }

    public void BeatmapDetailsUpdate(NewBeatmapData beatmap)
    {
        selectedBeatmap = beatmap;
    }
}