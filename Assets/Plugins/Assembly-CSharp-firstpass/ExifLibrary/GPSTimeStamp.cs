namespace ExifLibrary
{
	public class GPSTimeStamp : ExifURationalArray
	{
		protected new MathEx.UFraction32[] Value
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

		public MathEx.UFraction32 Hour
		{
			get
			{
				return mValue[0];
			}
			set
			{
				mValue[0] = value;
			}
		}

		public MathEx.UFraction32 Minute
		{
			get
			{
				return mValue[1];
			}
			set
			{
				mValue[1] = value;
			}
		}

		public MathEx.UFraction32 Second
		{
			get
			{
				return mValue[2];
			}
			set
			{
				mValue[2] = value;
			}
		}

		public GPSTimeStamp(ExifTag tag, MathEx.UFraction32[] value)
			: base(tag, value)
		{
		}

		public override string ToString()
		{
			return string.Format("{0:F2}:{1:F2}:{2:F2}\"", (float)Hour, (float)Minute, (float)Second);
		}
	}
}
