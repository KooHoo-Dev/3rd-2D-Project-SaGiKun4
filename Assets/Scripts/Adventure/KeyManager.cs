using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    private PlayerInput inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            RebindUp();
        }
    }

    public void RebindUp()
    {
        var action = inputActions.PlayerMove.Move;

        action.Disable();

        action.PerformInteractiveRebinding(1)
            .WithControlsExcluding("Mouse")
            .OnComplete(op =>
            {
                action.Enable();
                op.Dispose();
            })
            .Start();
    }
}
