using System.Text;

namespace ExifLibrary
{
	public class ExifUndefined : ExifProperty
	{
		protected byte[] mValue;

		protected override object _Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = (byte[])value;
			}
		}

		public new byte[] Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}

		public override ExifInterOperability Interoperability
		{
			get
			{
				return new ExifInterOperability(ExifTagFactory.GetTagID(mTag), 7, (uint)mValue.Length, mValue);
			}
		}

		public ExifUndefined(ExifTag tag, byte[] value)
			: base(tag)
		{
			mValue = value;
		}

		public static implicit operator byte[](ExifUndefined obj)
		{
			return obj.mValue;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			byte[] array = mValue;
			foreach (byte value in array)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(' ');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
