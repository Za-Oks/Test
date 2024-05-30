namespace VoxelBusters.NativePlugins
{
	public class TwitterAuthSession
	{
		public string AuthToken { get; protected set; }

		public string AuthTokenSecret { get; protected set; }

		public string UserID { get; protected set; }

		protected TwitterAuthSession()
		{
			AuthToken = null;
			AuthTokenSecret = null;
			UserID = null;
		}

		public override string ToString()
		{
			return string.Format("[TwitterSession: AuthToken={0}, AuthTokenSecret={1}, UserID={2}]", AuthToken, AuthTokenSecret, UserID);
		}
	}
}
