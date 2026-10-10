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

    public bool ReceiveChart(TextAsset chart, AudioClip music, float bpm, float offset)
    {
        if (chart == null)
        {
            Debug.LogError("Chart is null");
            return false;
        }

        BeatmapData beatmap = JsonUtility.FromJson<BeatmapData>(chart.text);
        if (beatmap == null || beatmap.notes == null)
        {
            Debug.LogError("Beatmap or beatmap.notes is null");
            return false;
        }

        if (music == null || bpm <= 0)
        {
            Debug.LogError("Music or BPM is null");
            return false;
        }

        float maxBeat = MaxBeatCalc(music, bpm, offset);
        if (maxBeat < 0)
        {
            return false;
        }
        foreach (NoteData note in beatmap.notes)
        {
            if (note.laneIndex < 0 || note.laneIndex >= 4)
            {
                Debug.LogError("Invalid Lane");
                return false;
            }
            if (note.noteBeat < 0 || note.noteBeat > maxBeat)
            {
                Debug.LogError("Invalid NoteBeat");
                return false;
            }
        }

        beatmap.notes.Sort((a, b) => a.noteBeat.CompareTo(b.noteBeat));

        beatList = beatmap.notes;
        nextBeatIndex = 0;
        return true;
    }

    public float MaxBeatCalc(AudioClip music, float bpm, float offset)
    {
        if (music == null)
        {
            Debug.LogError("Music is null");
            return -1f;
        }

        if (bpm <= 0)
        {
            Debug.LogError("BPM must be greater than zero");
            return -1f;
        }

        float availableDuration = music.length - offset;

        if (offset < 0 || availableDuration <= 0)
        {
            Debug.LogError("Invalid offset: no valid duration remains");
            return -1f;
        }

        float secPerBeat = 60f / bpm;
        float totalBeats = availableDuration / secPerBeat;

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