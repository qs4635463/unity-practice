using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance{
        get{
            return _instance;
        }
    }
    private PlayerControl playerControl;

    private void Awake(){
        if(_instance != null && _instance != this)
            Destroy(this);
        else{
            _instance = this;
        }
        playerControl = new PlayerControl();
    }
    void Start()
    {

    }

    private void OnEnable(){
        playerControl.Enable();
    }

    private void OnDisable(){
        playerControl.Disable();
    }

    public Vector2 GetPlayerMovement(){
        return playerControl.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta(){
        return playerControl.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpThisFrame(){
        return playerControl.Player.Jump.triggered;
    }

    public bool PlayerIsRunning(){
        return playerControl.Player.Run.IsPressed();
    }

    public bool PlayerPause(){
        return playerControl.Player.Pause.triggered;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
