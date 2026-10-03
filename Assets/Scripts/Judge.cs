using UnityEngine;
using TMPro;

public class Judge : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text precisionText;
    public TMP_Text comboText;

    public JudgementData[] judgementData;
    public Conductor conductor;
    public NoteSpawner noteSpawner;
    public float perfectThreshold = 0.05f;
    public float greatThreshold = 0.09f;
    public float goodThreshold = 0.13f;
    public float missThreshold = 0.18f;
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

    public void JudgeNoteHit(Note note)
    {
        double noteTime = note.noteBeat * conductor.secPerBeat;
        double signedDifference = conductor.songPosition - noteTime;
        float timeDifference = Mathf.Abs((float)signedDifference);
        bool isNoteHit = false;

        if (timeDifference <= perfectThreshold)
        {
            RegisterJudgement(Judgement.Perfect);
            isNoteHit = true;
        }
        else if (timeDifference <= greatThreshold)
        {
            RegisterJudgement(Judgement.Great);
            isNoteHit = true;
        }
        else if (timeDifference <= goodThreshold)
        {
            RegisterJudgement(Judgement.Good);
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
    }

    private void RegisterJudgement(Judgement judgement)
    {
        JudgementData data = judgementData[(int)judgement];
        Debug.Log(data.name);
        score += data.score;
        data.count++;
        totalPercentage += data.percentage;

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
}