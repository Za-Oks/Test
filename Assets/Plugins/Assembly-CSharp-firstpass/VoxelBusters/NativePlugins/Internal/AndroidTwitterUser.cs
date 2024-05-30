using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class AndroidTwitterUser : TwitterUser
	{
		private const string kIsVerified = "is-verified";

		private const string kUserID = "user-identifier";

		private const string kName = "name";

		private const string kProfileImageURL = "profile-image-url";

		private const string kIsProtected = "is-protected";

		public AndroidTwitterUser(IDictionary _userJsonDict)
		{
			base.UserID = _userJsonDict["user-identifier"] as string;
			base.Name = _userJsonDict["name"] as string;
			base.IsVerified = _userJsonDict.GetIfAvailable("is-verified", false);
			base.IsProtected = _userJsonDict.GetIfAvailable("is-protected", false);
			base.ProfileImageURL = _userJsonDict["profile-image-url"] as string;
		}
	}
}
