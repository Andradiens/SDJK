using UnityEngine;
using UnityEngine.InputSystem;

public class LaneInput : MonoBehaviour
{

    public InputActionReference lane1;
    public InputActionReference lane2;
    public InputActionReference lane3;
    public InputActionReference lane4;

    private void Update()
    {
        if (lane1.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 1 pressed");
        }
        if (lane2.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 2 pressed");
        }
        if (lane3.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 3 pressed");
        }
        if (lane4.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 4 pressed");
        }
    }

    private void OnEnable()
    {
        lane1.action.Enable();
        lane2.action.Enable();
        lane3.action.Enable();
        lane4.action.Enable();
    }

    private void OnDisable()
    {
        lane1.action.Disable();
        lane2.action.Disable();
        lane3.action.Disable();
        lane4.action.Disable();
    }
}
