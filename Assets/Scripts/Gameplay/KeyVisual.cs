using UnityEngine;
using UnityEngine.InputSystem;

public class KeyVisual : MonoBehaviour
{
    public InputActionReference action;
    public Color normalColor;
    public Color pressedColor;
    public float popScale = 1.1f;
    public float popDuration = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 baseScale;
    private float popTimer = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
    }

    private void Update()
    {
        if (action.action.IsPressed())
        {
            spriteRenderer.color = pressedColor;
        }
        else
        {
            spriteRenderer.color = normalColor;
        }

        if (popTimer > 0)
        {
            popTimer -= Time.deltaTime;
            float t = Mathf.Clamp01(popTimer / popDuration);
            transform.localScale = baseScale * Mathf.Lerp(1f, popScale, t);
        }
    }

    public void Pop()
    {
        popTimer = popDuration;
    }
}