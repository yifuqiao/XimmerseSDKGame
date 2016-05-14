namespace Ximmerse.CrossInput {

	/// <summary>
	/// DirectInputElement is the InputElement which has only one main data supplier.
	/// </summary>
	public class DirectInputElement:SmartPointer {

		protected object m_Supplier;

		public virtual bool SetMainSupplier(object supplier){
			if(m_Supplier==null) {
				m_Supplier=supplier;
				return true;
			}else {
				if(m_Supplier==supplier) {
					return true;
				}else {
					Log.e("DirectInputElement","Must have only one main data supplier!!!");
					return false;
				}
			}
		}

	}

}