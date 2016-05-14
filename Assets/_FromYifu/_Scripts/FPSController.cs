using UnityEngine;
using System.Collections;
using Ximmerse.Animation;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour {

    [SerializeField]
    private float m_movementSpeed=10f;
    [SerializeField]
    private float m_yawSpeed=0.5f;
    [SerializeField]
    private float m_pitchSpeed=0.2f;
    [SerializeField]
    private float m_armLength = 0.7f;
    [SerializeField]
    private Camera m_playerCamera;
    [SerializeField]
    private Transform m_leftHandTransform;
    [SerializeField]
    private Transform m_rightHandTransform;
    [SerializeField]
    private Transform m_leftHandWeaponSocket;
    [SerializeField]
    private Transform m_rightHandWeaponSocket;
    [SerializeField]
    private Transform m_chestTransform;

    [SerializeField]
    private WeaponBase m_leftWeapon;
    [SerializeField]
    private WeaponBase m_rightWeapon;

    private HandAnimator m_leftHandAnimator;
    private HandAnimator m_rightHandAnimator;
    private CharacterController m_characterController;

    void Awake()
    {
        GameManager.Instance.InputDispatcher.OnHorizontalInput += InputDispatcher_OnHorizontalInput;
        GameManager.Instance.InputDispatcher.OnVerticalInput += InputDispatcher_OnVerticalInput;
        GameManager.Instance.InputDispatcher.OnLeftTriggerButton += InputDispatcher_OnLeftTriggerButton;
        GameManager.Instance.InputDispatcher.OnRightTriggerButton += InputDispatcher_OnRightTriggerButton;
        GameManager.Instance.InputDispatcher.OnYawInput += InputDispatcher_OnYawInput;
        GameManager.Instance.InputDispatcher.OnPitchInput += InputDispatcher_OnPitchInput;
        GameManager.Instance.InputDispatcher.OnLeftHandPose += InputDispatcher_OnLeftHandPose;
        GameManager.Instance.InputDispatcher.OnRightHandPose += InputDispatcher_OnRightHandPose;

        GameManager.Instance.InputDispatcher.OnLeftReloadButton += InputDispatcher_OnLeftReloadButton; ;
        GameManager.Instance.InputDispatcher.OnRightReloadButton += InputDispatcher_OnRightReloadButton; ;

        m_leftHandAnimator = m_leftHandTransform.GetComponent<HandAnimator>();
        m_rightHandAnimator = m_rightHandTransform.GetComponent<HandAnimator>();
        m_characterController = GetComponent<CharacterController>();

    }

    void Destroy()
    {
        GameManager.Instance.InputDispatcher.OnHorizontalInput -= InputDispatcher_OnHorizontalInput;
        GameManager.Instance.InputDispatcher.OnVerticalInput -= InputDispatcher_OnVerticalInput;
        GameManager.Instance.InputDispatcher.OnLeftTriggerButton -= InputDispatcher_OnLeftTriggerButton;
        GameManager.Instance.InputDispatcher.OnRightTriggerButton -= InputDispatcher_OnRightTriggerButton;
        GameManager.Instance.InputDispatcher.OnYawInput -= InputDispatcher_OnYawInput;
        GameManager.Instance.InputDispatcher.OnPitchInput -= InputDispatcher_OnPitchInput;
        GameManager.Instance.InputDispatcher.OnLeftHandPose -= InputDispatcher_OnLeftHandPose;
        GameManager.Instance.InputDispatcher.OnRightHandPose -= InputDispatcher_OnRightHandPose;
    }

    private void InputDispatcher_OnRightReloadButton()
    {
        m_rightWeapon.Reload();
    }

    private void InputDispatcher_OnLeftReloadButton()
    {
        m_leftWeapon.Reload();
    }

    private void InputDispatcher_OnRightHandPose(Ximmerse.CrossInput.VirtualPose obj)
    {
        m_rightHandTransform.localRotation = obj.rotation;
        //todo: for some reason, the positional tracking of the hardware is very inconsitent, so I am using a different method to calculate hand positon;
        m_rightHandTransform.position = m_rightHandTransform.forward * m_armLength + m_chestTransform.position;
    }
    private void InputDispatcher_OnLeftHandPose(Ximmerse.CrossInput.VirtualPose obj)
    {
        m_leftHandTransform.localRotation = obj.rotation;
        //todo: for some reason, the positional tracking of the hardware is very inconsitent, so I am using a different method to calculate hand positon;
        m_leftHandTransform.position = m_leftHandTransform.forward * m_armLength + m_chestTransform.position;
    }
    private void InputDispatcher_OnPitchInput(float obj)
    {
        m_playerCamera.transform.RotateAround(m_playerCamera.transform.position,transform.right, obj * m_pitchSpeed * Time.deltaTime);
    }
    private void InputDispatcher_OnYawInput(float obj)
    {
        transform.Rotate(transform.up, obj * m_yawSpeed * Time.deltaTime);
    }
    private void InputDispatcher_OnRightTriggerButton(float obj)
    {
        // convert 0-1 to -1 to 1
        m_rightHandAnimator.rightGesture.entries[1].value = ((1f-obj)-0.5f)*2f;
        if(obj>0.9f)
            m_rightWeapon.Fire();
    }
    private void InputDispatcher_OnLeftTriggerButton(float obj)
    {
        // convert 0-1 to -1 to 1
        m_leftHandAnimator.leftGesture.entries[1].value = ((1f - obj) - 0.5f) * 2f;
        if (obj > 0.9f)
            m_leftWeapon.Fire();
    }
    private void InputDispatcher_OnVerticalInput(float obj)
    {
        m_characterController.Move(transform.forward * obj * m_movementSpeed * Time.deltaTime);
    }
    private void InputDispatcher_OnHorizontalInput(float obj)
    {
        m_characterController.Move(transform.right * obj * m_movementSpeed*Time.deltaTime);
    }
}
