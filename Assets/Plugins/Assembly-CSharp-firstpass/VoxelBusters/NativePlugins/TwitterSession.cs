namespace VoxelBusters.NativePlugins
{
	public class TwitterSession : TwitterAuthSession
	{
		public string UserName { get; protected set; }

		protected TwitterSession()
		{
			UserName = null;
		}

		public override string ToString()
		{
			return string.Format("[TwitterSession: AuthToken={0}, AuthTokenSecret={1}, UserName={2}, UserID={3}]", base.AuthToken, base.AuthTokenSecret, UserName, base.UserID);
		}
	}
}
