using UnityEngine;
using UnityEngine.InputSystem;

public class LaneInput : MonoBehaviour
{

    public InputActionReference lane1;
    public InputActionReference lane2;
    public InputActionReference lane3;
    public InputActionReference lane4;
    public Judge judge;

    private void Update()
    {
        if (lane1.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 1 pressed");
            judge.JudgeLane(0);
        }
        if (lane2.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 2 pressed");
            judge.JudgeLane(1);
        }
        if (lane3.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 3 pressed");
            judge.JudgeLane(2);
        }
        if (lane4.action.WasPressedThisFrame())
        {
            Debug.Log("Lane 4 pressed");
            judge.JudgeLane(3);
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