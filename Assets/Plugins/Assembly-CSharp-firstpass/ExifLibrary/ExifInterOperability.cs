namespace ExifLibrary
{
	public struct ExifInterOperability
	{
		private ushort mTagID;

		private ushort mTypeID;

		private uint mCount;

		private byte[] mData;

		public ushort TagID
		{
			get
			{
				return mTagID;
			}
		}

		public ushort TypeID
		{
			get
			{
				return mTypeID;
			}
		}

		public uint Count
		{
			get
			{
				return mCount;
			}
		}

		public byte[] Data
		{
			get
			{
				return mData;
			}
		}

		public ExifInterOperability(ushort tagid, ushort typeid, uint count, byte[] data)
		{
			mTagID = tagid;
			mTypeID = typeid;
			mCount = count;
			mData = data;
		}

		public override string ToString()
		{
			return string.Format("Tag: {0}, Type: {1}, Count: {2}, Data Length: {3}", mTagID, mTypeID, mCount, mData.Length);
		}
	}
}
