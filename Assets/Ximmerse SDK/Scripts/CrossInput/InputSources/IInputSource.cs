using UnityEngine;
using System.Collections;

namespace Ximmerse.CrossInput {

	/// <summary>
	/// ��Ϊһ������Դ�Ľӿ�
	/// </summary>
	public interface IInputSource {

		/// <summary>
		/// 
		/// </summary>
		bool enabled{
			get;
			set;
		}

		/// <summary>
		/// ��ʼ������.
		/// </summary>
		int InitInput();

		/// <summary>
		/// �˳�����.
		/// </summary>
		int ExitInput();

		/// <summary>
		/// ��������.
		/// </summary>
		//void ResetInput();

		/// <summary>
		/// ��������֡(д�����뻺��).
		/// </summary>
		int EnterInputFrame();

		/// <summary>
		/// �˳�����֡(Cleanup).
		/// </summary>
		int ExitInputFrame();

	}
}