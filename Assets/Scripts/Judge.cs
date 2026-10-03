using UnityEngine;
using TMPro;

public class Judge : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text precisionText;
    public TMP_Text comboText;
    public TMP_Text judgementText;

    public JudgementData[] judgementData;
    public KeyVisual[] keys;
    public Conductor conductor;
    public NoteSpawner noteSpawner;
    public float perfectThreshold = 0.05f;
    public float greatThreshold = 0.09f;
    public float goodThreshold = 0.13f;
    public float missThreshold = 0.18f;
    public float displayTime = 0.6f;
    public float popScale = 1.3f;
    private float timer = 0f;
    public int combo = 0;
    

    public enum Judgement
    {
        Perfect,
        Great,
        Good,
        Miss
    }

    public int score = 0;
    public float precision = 100f;
    private float totalPercentage = 0f;
    private int notesJudged = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        for (int lane = 0; lane < noteSpawner.lanes.Length; lane++)
        {
            if (noteSpawner.lanes[lane].notes.Count > 0)
            {
                Note firstNote = noteSpawner.lanes[lane].notes[0];
                double noteTime = firstNote.noteBeat * conductor.secPerBeat;
                double timeDifference = conductor.songPosition - noteTime;

                if (timeDifference > missThreshold)
                {
                    RegisterJudgement(Judgement.Miss);
                    noteSpawner.lanes[lane].notes.Remove(firstNote);
                    Destroy(firstNote.gameObject);
                }
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Color c = judgementText.color;
            c.a = Mathf.Clamp01(timer / displayTime);
            judgementText.color = c;

            float textScale = Mathf.Lerp(popScale, 1f, 1f - (timer / displayTime));
            judgementText.transform.localScale = Vector3.one * textScale;
        }
    }

    public void JudgeNoteHit(Note note)
    {
        double noteTime = note.noteBeat * conductor.secPerBeat;
        double signedDifference = conductor.songPosition - noteTime;
        float timeDifference = Mathf.Abs((float)signedDifference);
        bool isNoteHit = false;

        if (timeDifference <= perfectThreshold)
        {
            RegisterJudgement(Judgement.Perfect);
            keys[note.laneIndex].Pop();
            isNoteHit = true;
        }
        else if (timeDifference <= greatThreshold)
        {
            RegisterJudgement(Judgement.Great);
            keys[note.laneIndex].Pop();
            isNoteHit = true;
        }
        else if (timeDifference <= goodThreshold)
        {
            RegisterJudgement(Judgement.Good);
            keys[note.laneIndex].Pop();
            isNoteHit = true;
        }
        else if (timeDifference <= missThreshold)
        {
            RegisterJudgement(Judgement.Miss);
            if (signedDifference > 0)
            {
                isNoteHit = true;
            }
        }

        if (isNoteHit == true)
        {
            noteSpawner.lanes[note.laneIndex].notes.Remove(note);
            Destroy(note.gameObject); 
        }
    }

    private void RegisterJudgement(Judgement judgement)
    {
        JudgementData data = judgementData[(int)judgement];
        score += data.score * combo;
        data.count++;
        totalPercentage += data.percentage;
        judgementText.text = data.name;
        judgementText.color = data.color;
        timer = displayTime;

        if (judgement == Judgement.Miss)
        {
            combo = 0;
        }
        else
        {
            combo++;
        }

        notesJudged++;
        precision = totalPercentage / notesJudged;
    
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();
        precisionText.text = precision.ToString("F1") + "%";
        comboText.text = combo.ToString() + "x";
    }

    public void JudgeLane(int lane)
    {
        if (noteSpawner.lanes[lane].notes.Count <= 0)
        {
            return;
        }
        else
        {
            Note firstNote = noteSpawner.lanes[lane].notes[0];
            JudgeNoteHit(firstNote);
        }
    }
}

[System.Serializable]
public class JudgementData
{
    public string name;
    public int score;
    public int count = 0;
    public float percentage;
    public Color color = Color.white;
}