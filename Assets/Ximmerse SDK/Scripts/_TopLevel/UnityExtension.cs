using UnityEngine;

namespace Ximmerse {

	/// <summary>
	/// 
	/// </summary>
	public static partial class UnityExtension {

		/// <summary>
		/// 
		/// </summary>
		public static string ToHexString(this byte[] bytes,int offset=0,int count=0){
			if(count==0) {
				count=bytes.Length;
			}
			if(count>0){
				System.Text.StringBuilder sb=new System.Text.StringBuilder();
				sb.Append(bytes[offset++].ToString("X2"));
				while(offset<count){
					sb.Append(" "+bytes[offset++].ToString("X2"));
				}
				return sb.ToString();
			}
			return "";
		}

		/// <summary>
		/// 
		/// </summary>
		public static void ResetLocal(this Transform t) {
			t.localPosition=Vector3.zero;
			t.localRotation=Quaternion.identity;
			t.localScale=Vector3.one;
		}

	}
}
