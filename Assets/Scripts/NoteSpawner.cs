using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Conductor conductor;
    public List<NoteData> beatList;
    public int nextBeatIndex = 0;
    public float spawnDistanceInBeats;
    public float[] lanePositions;
    public LaneNotes[] lanes;
    public TextAsset beatmapJson;

    private void Start()
    {
        BeatmapData beatmap = JsonUtility.FromJson<BeatmapData>(beatmapJson.text);
        beatList = beatmap.notes;
        Debug.Log("Loaded beatmap with " + beatList.Count + " notes.");
    }

    public void Update()
    {
        if (nextBeatIndex < beatList.Count)
        {
            NoteData nextBeat = beatList[nextBeatIndex];
            Vector3 spawnPosition = new Vector3(lanePositions[nextBeat.laneIndex], 0f, 0f);

            if (conductor.songPositionInBeats >= nextBeat.noteBeat - spawnDistanceInBeats)
            {
                GameObject note = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
                Note noteScript = note.GetComponent<Note>();
                noteScript.noteBeat = nextBeat.noteBeat;
                noteScript.laneIndex = nextBeat.laneIndex;
                noteScript.conductor = conductor;

                lanes[nextBeat.laneIndex].notes.Add(noteScript);
                nextBeatIndex++;
            }
        }
    }
}

[System.Serializable]
public class LaneNotes
{
    public List<Note> notes = new List<Note>();
}

[System.Serializable]
public class BeatmapData{
    public List<NoteData> notes;
}