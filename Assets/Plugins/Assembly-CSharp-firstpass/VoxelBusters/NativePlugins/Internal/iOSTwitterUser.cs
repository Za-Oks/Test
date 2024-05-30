using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class iOSTwitterUser : TwitterUser
	{
		private const string kIsVerified = "is-verified";

		private const string kUserID = "user-ID";

		private const string kName = "name";

		private const string kProfileImageURL = "profile-image-URL";

		private const string kIsProtected = "is-protected";

		public iOSTwitterUser(IDictionary _userJsonDict)
		{
			base.UserID = _userJsonDict.GetIfAvailable<string>("user-ID");
			base.Name = _userJsonDict.GetIfAvailable<string>("name");
			base.IsVerified = _userJsonDict.GetIfAvailable("is-verified", false);
			base.IsProtected = _userJsonDict.GetIfAvailable("is-protected", false);
			base.ProfileImageURL = _userJsonDict.GetIfAvailable<string>("profile-image-URL");
		}
	}
}
