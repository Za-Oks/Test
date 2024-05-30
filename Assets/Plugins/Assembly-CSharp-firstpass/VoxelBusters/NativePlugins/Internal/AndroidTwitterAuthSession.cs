using System.Collections;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class AndroidTwitterAuthSession : TwitterAuthSession
	{
		public const string kUserID = "user-identifier";

		public const string kAuthToken = "auth-token";

		public const string kAuthTokenSecret = "auth-token-secret";

		public AndroidTwitterAuthSession(IDictionary _sessionJsonDict)
		{
			base.AuthToken = _sessionJsonDict["auth-token"] as string;
			base.AuthTokenSecret = _sessionJsonDict["auth-token-secret"] as string;
			base.UserID = _sessionJsonDict["user-identifier"] as string;
		}
	}
}
