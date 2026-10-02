using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Conductor conductor;
    public List<float> beatList;
    public int nextBeatIndex = 0;
    public float spawnDistanceInBeats;

    public void Update()
    {
        if (nextBeatIndex < beatList.Count)
        {
            float nextBeat = beatList[nextBeatIndex];

            if (conductor.songPositionInBeats >= nextBeat - spawnDistanceInBeats)
            {
                GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);
                Note noteScript = note.GetComponent<Note>();
                noteScript.noteBeat = nextBeat;
                noteScript.conductor = conductor;
                nextBeatIndex++;
            }
        }
    }
}