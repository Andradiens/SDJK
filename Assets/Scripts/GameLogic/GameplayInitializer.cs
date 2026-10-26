using Unity.AppUI.Redux;
using UnityEngine;

public class GameplayInitializer : MonoBehaviour
{
    public Conductor conductor;
    public NoteSpawner noteSpawner;
    private BeatmapMetadataLoader beatmapMetadataLoader = new BeatmapMetadataLoader();

    public void Start()
    {
        NewBeatmapData beatmap = SelectedBeatmap.newBeatmapData;

        if (beatmap == null)
        {
            Debug.LogError("Nao existe Beatmap");
            return;
        }
        
        BeatmapMetadataSave metadata = beatmapMetadataLoader.MetadataLoad(beatmap);
        float finalBpm = beatmap.bpm;
        float finalOffset = beatmap.offset;
        if (metadata != null)
        {
            finalBpm = metadata.bpm;
            finalOffset = metadata.offset;
        }

        bool chartLoaded = noteSpawner.ReceiveChart(beatmap.chart, beatmap.music, finalBpm, finalOffset);
        if (!chartLoaded)
        {
            return;
        }

        conductor.PlayBeatmap(beatmap.music, finalBpm, beatmap.scrollSpeed, finalOffset);
    }
}
