namespace VoxelBusters.NativePlugins
{
	public class TwitterUser
	{
		public string UserID { get; protected set; }

		public string Name { get; protected set; }

		public bool IsVerified { get; protected set; }

		public bool IsProtected { get; protected set; }

		public string ProfileImageURL { get; protected set; }

		protected TwitterUser()
		{
			UserID = string.Empty;
			Name = string.Empty;
			IsVerified = false;
			IsProtected = false;
			ProfileImageURL = string.Empty;
		}

		public override string ToString()
		{
			return string.Format("[TwitterUser: UserID={0}, Name={1}, IsVerified={2}, IsProtected={3}, ProfileImageURL={4}]", UserID, Name, IsVerified, IsProtected, ProfileImageURL);
		}
	}
}
