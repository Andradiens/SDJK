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

    private void Start()
    {
        
    }

    public void Update()
    {
        if (beatList == null)
            return;

        if (nextBeatIndex < beatList.Count)
        {
            NoteData nextBeat = beatList[nextBeatIndex];
            Vector3 spawnPosition = new Vector3(lanePositions[nextBeat.laneIndex], 15f, 0f);

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

    public void ReceiveChart(TextAsset chart, AudioClip music, float secPerBeat, float offset)
    {
        if (chart == null)
        {
            Debug.LogError("Chart is null");
            return;
        }

        BeatmapData beatmap = JsonUtility.FromJson<BeatmapData>(chart.text);
        if (beatmap == null || beatmap.notes == null)
        {
            Debug.LogError("Beatmap or beatmap.notes is null");
            return;
        }

        if (music == null || secPerBeat <= 0)
        {
            Debug.LogError("Music ou secPerBeat is null");
            return;
        }
        
        float maxBeat = MaxBeatCalc(music, secPerBeat, offset);
        foreach (NoteData note in beatmap.notes)
        {
            if (note.laneIndex < 0 || note.laneIndex >= 4)
            {
                Debug.LogError("Invalid Lane");
                return;
            }
            if (note.noteBeat < 0 || note.noteBeat > maxBeat)
            {
                Debug.LogError("Invalid NoteBeat");
                return;
            }
        }

        beatmap.notes.Sort((a, b) => a.noteBeat.CompareTo(b.noteBeat));

        beatList = beatmap.notes;
        nextBeatIndex = 0;
    }

    public float MaxBeatCalc(AudioClip music, float secPerBeat, float offset)
    {
        float totalBeats = (music.length - offset) / secPerBeat;
        return totalBeats;
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