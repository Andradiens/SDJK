using UnityEngine;
using TMPro;

public class ResultsDisplay : MonoBehaviour
{
    public TMP_Text finalScore;
    public TMP_Text maxCombo;
    public TMP_Text perfects;
    public TMP_Text greats;
    public TMP_Text goods;
    public TMP_Text misses;
    public TMP_Text precision;

    private void Start()
    {
        finalScore.text = GameResults.finalScore.ToString();
        maxCombo.text = GameResults.maxCombo.ToString() + "x";
        precision.text = GameResults.precision.ToString("F1") + "%";
        perfects.text = GameResults.perfects.ToString() + "x";
        greats.text = GameResults.greats.ToString() + "x";
        goods.text = GameResults.goods.ToString() + "x";
        misses.text = GameResults.misses.ToString() + "x";
    }
}
