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

        conductor.PlayBeatmap(beatmap.music, beatmap.bpm, beatmap.scrollSpeed, beatmap.offset);
        noteSpawner.ReceiveChart(beatmap.chart, beatmap.music, conductor.secPerBeat, beatmap.offset);
    }
}
