using UnityEngine;

public class GameplayInitializer : MonoBehaviour
{
    public Conductor conductor;
    public NoteSpawner noteSpawner;

    public void Start()
    {
        NewBeatmapData beatmap = SelectedBeatmap.newBeatmapData;

        if (beatmap == null)
        {
            Debug.LogError("Nao existe Beatmap");
            return;
        }
        
        bool chartLoaded = noteSpawner.ReceiveChart(beatmap.chart, beatmap.music, beatmap.bpm, beatmap.offset);
        if (!chartLoaded)
        {
            return;
        }

        conductor.PlayBeatmap(beatmap.music, beatmap.bpm, beatmap.scrollSpeed, beatmap.offset);
    }
}
