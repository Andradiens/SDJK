using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteBeat;
    public int laneIndex;
    public float endPosition = 0f;
    public Conductor conductor;

    private void Update()
    {
        float distance = (float)(conductor.songPositionInBeats - noteBeat) * conductor.scrollSpeed;

        Vector3 notePosition = transform.position;
        notePosition.y = endPosition - distance;
        transform.position = notePosition;
    }
}

[System.Serializable]
public class NoteData
{
    public float noteBeat;
    public int laneIndex;
}