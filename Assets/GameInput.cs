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
    [Header("esc�¼�")]
    public UnityEvent escEvent;

    public Lazerabsorber inputSystem;//���������ϵͳ�л�ȡ�����ֵ���Լ�Ϊ����ĳһ��ťע���¼�




    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // ���򣬽�������Ϊʵ�����������ڳ����л�ʱ��������
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
        Vector2 inputVector = inputSystem.Player.Move.ReadValue<Vector2>();//ͨ����ȡActionmap����ȡ����
        inputVector = inputVector.normalized;//������  
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
