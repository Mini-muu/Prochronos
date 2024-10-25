using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAround : MonoBehaviour
{
    [SerializeField, Range(-0.5f, 1.5f)] float defaultYPos = 0.8f;
    [SerializeField, Range(0.9f, 1.06f)] float upperYDest = 1.05f;
    [SerializeField, Range(0.05f, 0.5f)] float lowerYDest = 0.07f;

    private float yPos;
    private float yDest;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer virtualCameraTransposer;
    private InputAction moveAction;

    private void Start()
    {
        moveAction = PlayerInputManager.instance.move;
        yPos = defaultYPos;
        virtualCameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLookingAround())
            yDest = GetYLookingDir() > 0 ? upperYDest : lowerYDest;
        else
            yDest = defaultYPos;

        if (yDest != yPos)
            MoveCamera();

    }

    private float GetYLookingDir() => moveAction.ReadValue<Vector2>().y;

    private void MoveCamera()
    {
        yPos = GetNewPos();

        virtualCameraTransposer.m_ScreenY = yPos;
    }

    private float GetNewPos()
    {
        float calc = Mathf.Abs(yPos - yDest);
        return calc < 0.01 ? yDest : Mathf.Lerp(yPos, yDest, calc);
    }
    private bool IsLookingAround() => GetYLookingDir() != 0;
}