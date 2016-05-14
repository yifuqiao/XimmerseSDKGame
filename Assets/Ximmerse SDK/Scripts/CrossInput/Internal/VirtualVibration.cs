
namespace Ximmerse.CrossInput {

	/// <summary>
	/// 
	/// </summary>
	public class VirtualVibration:DirectInputElement {

		#region Nested Types

		public interface IImplementer {
			void SetVibration(int waveType,float delay,float duration) ;
			void StartVibration(int waveType);
			void StopVibration();
			int GetWaveTypeId(string waveName);
		}

		#endregion Nested Types

		#region Fields

		public string name;/*{
			get;
			private set;
		}*/
		public bool matchWithInputManager {
			get;
			private set;
		}

		protected IImplementer m_Impl;

		#endregion Fields

		#region Constructors

		public VirtualVibration(string name)
			: this(name,true) {
		}


		public VirtualVibration(string name,bool matchToInputSettings) {
			this.name=name;
			matchWithInputManager=matchToInputSettings;
		}

		public bool InitVibration(object supplier,IImplementer implementer) {
			bool ret=this.SetMainSupplier(supplier);
			if(ret) {
				m_Impl=implementer;
			}
			return ret;
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public void SetVibration(int waveType,float delay,float duration) {
			if(m_Impl!=null) {
				m_Impl.SetVibration(waveType,delay,duration);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void StartVibration(int waveType) {
			if(m_Impl!=null) {
				m_Impl.StartVibration(waveType);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void StopVibration() {
			if(m_Impl!=null) {
				m_Impl.StopVibration();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int GetWaveTypeId(string waveName) {
			if(m_Impl!=null) {
				return m_Impl.GetWaveTypeId(waveName);
			}
			return 0;
		}
		
		#endregion Methods

	}
}
