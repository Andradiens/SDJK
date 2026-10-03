using UnityEngine;
using TMPro;

public class KeyLabels : MonoBehaviour
{
    public TMP_Text[] keyLabels;
    public float showTime = 0.8f;
    public float fadeTime = 0.4f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > showTime)
        {
            float alpha = 1f - (timer - showTime) / fadeTime;
            alpha = Mathf.Clamp01(alpha);

            foreach (TMP_Text label in keyLabels)
            {
                Color c = label.color;
                c.a = alpha;
                label.color = c;
            }
        }
    }
}