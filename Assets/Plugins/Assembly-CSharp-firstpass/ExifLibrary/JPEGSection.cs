namespace ExifLibrary
{
	public class JPEGSection
	{
		public JPEGMarker Marker { get; private set; }

		public byte[] Header { get; set; }

		public byte[] EntropyData { get; set; }

		private JPEGSection()
		{
			Header = new byte[0];
			EntropyData = new byte[0];
		}

		public JPEGSection(JPEGMarker marker, byte[] data, byte[] entropydata)
		{
			Marker = marker;
			Header = data;
			EntropyData = entropydata;
		}

		public JPEGSection(JPEGMarker marker)
		{
			Marker = marker;
		}

		public override string ToString()
		{
			return string.Format("{0} => Header: {1} bytes, Entropy Data: {2} bytes", Marker, Header.Length, EntropyData.Length);
		}
	}
}
