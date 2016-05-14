using UnityEngine;
using System;
using Ximmerse.CrossInput;


public class InputDispatcher {

    private const float INPUT_DEADZONE = 0.15F;

    public event Action<float> OnHorizontalInput;
    public event Action<float> OnVerticalInput;
    
    public event Action<float> OnLeftTriggerButton;
    public event Action<float> OnRightTriggerButton;

    public event Action<float> OnYawInput;
    public event Action<float> OnPitchInput;

    public event Action<VirtualPose> OnLeftHandPose;
    public event Action<VirtualPose> OnRightHandPose;

    public event Action OnLeftReloadButton;
    public event Action OnRightReloadButton;

    public readonly VirtualPose LeftPose;
    public readonly VirtualPose RightPose;

    public InputDispatcher()
    {
        LeftPose = CrossInputManager.VirtualPoseReference("Left_Hand");
        RightPose = CrossInputManager.VirtualPoseReference("Right_Hand");
    }

    /// <summary>
    /// required to be called in FixedUpdate
    /// </summary>
    public void DispatchMotionInputEvents()
    {
        if (OnLeftHandPose != null && LeftPose != null)
            OnLeftHandPose(LeftPose);

        if (OnRightHandPose != null && RightPose != null)
            OnRightHandPose(RightPose);
    }
    /// <summary>
    /// required to be called in Update()
    /// </summary>
    public void DispatchInputEvents()
    {
        if(CrossInputManager.GetButtonDown("Left_Three"))
        {
            if(OnLeftReloadButton!=null)
            {
                OnLeftReloadButton();
            }
        }

        if (CrossInputManager.GetButtonDown("Right_Three"))
        {
            if (OnRightReloadButton!= null)
            {
                OnRightReloadButton();
            }
        }

        float axisInput = CrossInputManager.GetAxis("Left_Horizontal");
        if(OnHorizontalInput!=null && Mathf.Abs(axisInput) > INPUT_DEADZONE)
            OnHorizontalInput(axisInput);

        axisInput = CrossInputManager.GetAxis("Left_Vertical");
        if (OnVerticalInput != null && Mathf.Abs(axisInput) > INPUT_DEADZONE)
            OnVerticalInput(axisInput);

        axisInput = CrossInputManager.GetAxis("Left_Trigger");
        if (OnLeftTriggerButton != null )
            OnLeftTriggerButton(axisInput);

        axisInput = CrossInputManager.GetAxis("Right_Trigger");
        if (OnRightTriggerButton != null )
            OnRightTriggerButton(axisInput);

        axisInput = CrossInputManager.GetAxis("Right_Horizontal");
        if (OnYawInput != null && Mathf.Abs(axisInput) > INPUT_DEADZONE)
            OnYawInput(axisInput);

        axisInput = CrossInputManager.GetAxis("Right_Vertical");
        if (OnPitchInput != null && Mathf.Abs(axisInput) > INPUT_DEADZONE)
            OnPitchInput(axisInput);
    }

}
