using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance
    {
        get; private set;
    }
    [Header("esc事件")]
    public UnityEvent escEvent;

    public Lazerabsorber inputSystem;//从这个输入系统中获取输入的值，以及为按下某一按钮注册事件




    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // 否则，将自身设为实例，并保持在场景切换时不被销毁
        Instance = this;

        inputSystem = new Lazerabsorber();
        inputSystem.Player.Enable();
        inputSystem.UI.Enable();
        
        inputSystem.UI.ESC.performed += Esc_performed;


    }
    
    private void Esc_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("success");
        escEvent?.Invoke();
    }

    public Vector2 GetMoveDir()
    {
        Vector2 inputVector = inputSystem.Player.Move.ReadValue<Vector2>();//通过读取Actionmap来获取输入
        inputVector = inputVector.normalized;//正交化  
        return inputVector;
    }

    public void DisablePlayInput()
    {
        inputSystem.Player.Disable();
    }
    public void EnablePlayInput()
    {
        inputSystem.Player.Enable();
    }
}
