using UnityEngine;
using System.IO;

public class BeatmapMetadataLoader
{
    public BeatmapMetadataSave MetadataLoad(NewBeatmapData beatmap)
    {
        string filePath = GetMetadataFilePath(beatmap);
        if (!File.Exists(filePath))
        {
            return null;
        }
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<BeatmapMetadataSave>(json);
    }

    private string GetMetadataFilePath(NewBeatmapData beatmap)
    {
        string gameDir = Application.persistentDataPath;
        string jsonDir = Path.Combine(gameDir, "BeatmapMetadataSave");
        string fileName = $"{beatmap.songName}_{beatmap.difficultyName}.json";
        return Path.Combine(jsonDir, fileName);
    }
}
