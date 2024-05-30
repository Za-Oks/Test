using System.Runtime.InteropServices;

namespace VoxelBusters.Utility.Internal
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct JSONString
	{
		public string Value { get; private set; }

		public bool IsNullOrEmpty { get; private set; }

		public int Length { get; private set; }

		public char this[int _index]
		{
			get
			{
				return Value[_index];
			}
		}

		public JSONString(string _JSONString)
		{
			this = default(JSONString);
			Value = _JSONString;
			IsNullOrEmpty = string.IsNullOrEmpty(_JSONString);
			Length = ((!IsNullOrEmpty) ? _JSONString.Length : 0);
		}
	}
}
