using UnityEngine;
using UnityEditor;

namespace Ximmerse.Core {

	[CustomEditor(typeof(XHawkMapSetting))]
	public class XHawkMapSettingEditor:Editor {
		protected bool m_IsRawView = true;
		protected bool
			m_rawPose;
		protected string
			m_pose,
			m_vibration,
			m_axis_JoystickX,
			m_axis_JoystickY,
			m_axis_Trigger,
			m_btn_Start,
			m_btn_One,
			m_btn_Two,
			m_btn_Three,
			m_btn_Trigger;

		/// <summary>
		/// 
		/// </summary>
		public override void OnInspectorGUI() {
			m_IsRawView=EditorGUILayout.Toggle("Is RawView",m_IsRawView);
			XHawkMapSetting target_ = target as XHawkMapSetting;
			if(m_IsRawView) {
				EditorGUI.BeginChangeCheck();

				//m_rawPose=EditorGUILayout.Toggle("Use Raw Pose",target_.rawPose);
				m_pose=EditorGUILayout.TextField("Pose",target_.pose);
				m_vibration=EditorGUILayout.TextField("Vibration",target_.vibration);
				m_axis_JoystickX=EditorGUILayout.TextField("Axis JoystickX",target_.axis_JoystickX);
				m_axis_JoystickY=EditorGUILayout.TextField("Axis JoystickY",target_.axis_JoystickY);
				m_axis_Trigger=EditorGUILayout.TextField("Axis Trigger",target_.axis_Trigger);
				m_btn_One=EditorGUILayout.TextField("Button [1]",target_.btn_One);
				m_btn_Start=EditorGUILayout.TextField("Button [2]",target_.btn_Start);
				m_btn_Two=EditorGUILayout.TextField("Button [3]",target_.btn_Two);
				m_btn_Three=EditorGUILayout.TextField("Button [4]",target_.btn_Three);
				m_btn_Trigger=EditorGUILayout.TextField("Button Trigger",target_.btn_Trigger);

				if(EditorGUI.EndChangeCheck()) {
					Undo.RecordObject(target_,"Modifies Map");
					EditorUtility.SetDirty(target_);
					//target_.rawPose=m_rawPose;
					target_.pose=m_pose;
					target_.vibration=m_vibration;
					target_.axis_JoystickX=m_axis_JoystickX;
					target_.axis_JoystickY=m_axis_JoystickY;
					target_.axis_Trigger=m_axis_Trigger;
					target_.btn_Start=m_btn_Start;
					target_.btn_One=m_btn_One;
					target_.btn_Two=m_btn_Two;
					target_.btn_Three=m_btn_Three;
					target_.btn_Trigger=m_btn_Trigger;
				}
			} else {
				DrawDefaultInspector();
			}
		}
	}

}