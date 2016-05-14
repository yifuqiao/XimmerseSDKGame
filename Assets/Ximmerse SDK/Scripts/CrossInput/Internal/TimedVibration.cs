using UnityEngine;

namespace Ximmerse.CrossInput {

	public class TimedVibration:VirtualVibration.IImplementer {

		#region Fields

		protected bool m_IsVibrating;
		protected int m_WaveType;
		protected float m_Duration;
		protected float m_LastVibrationTime;
		protected DelayedCall m_DelayedCall;

		[System.NonSerialized]protected string m_Tag,m_Name;

		#endregion Fields

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public virtual void SetVibration(int waveType,float delay,float duration) {
			DelayedCall.Free(ref m_DelayedCall);
			//StopVibration();// Stop vibration firstly. ???
			// Args
			m_WaveType=waveType;
			m_Duration=duration;
			//
			if(delay<=0.0f) {
				StartVibration();
			} else {
				m_DelayedCall=Juggler.Main.DelayCall(StartVibration,delay);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void StartVibration() {
			StartVibration(m_WaveType);
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void StartVibration(int waveType) {
			// Time.
			m_LastVibrationTime=Time.realtimeSinceStartup;
			//
			DoStartVibration(waveType);
			m_IsVibrating=true;
			// When stop the vibration.
			if(m_Duration>0f) {
				DelayedCall.Free(ref m_DelayedCall);
				// <!-- TODO
				m_DelayedCall=Juggler.Main.DelayCall(StopVibration,m_Duration);
				m_Duration=0f;
				// TODO -->
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void StopVibration() {
			// Time.
			if(m_IsVibrating) {
				Log.v(
					m_Tag,
					m_Name+"'s vibration lasts "+
					((Time.realtimeSinceStartup-m_LastVibrationTime)*1000).ToString()+" ms"
				);
			}
			DelayedCall.Free(ref m_DelayedCall);
			// Args
			m_WaveType=0;
			m_Duration=0f;
			//
			DoStopVibration();
			m_IsVibrating=false;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual int GetWaveTypeId(string waveName) {
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void DoStartVibration(int waveType) {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void DoStopVibration() {
			throw new System.NotImplementedException();
		}

		#endregion Methods

	}

}