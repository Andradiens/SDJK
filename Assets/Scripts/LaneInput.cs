using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{

    public InputActionReference lane1;
    public InputActionReference lane2;
    public InputActionReference lane3;
    public InputActionReference lane4;

    void Update()
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
}
