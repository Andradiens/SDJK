using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteBeat;
    public float endPosition = 0f;
    public Conductor conductor;

    private void Update()
    {
        float distance = (float)(conductor.songPositionInBeats - noteBeat) * conductor.scrollSpeed;

        Vector3 notePosition = transform.position;
        notePosition.y = endPosition - distance;
        transform.position = notePosition;

        if (notePosition.y < -12f)
        {
            Destroy(gameObject);
        }
    }
}