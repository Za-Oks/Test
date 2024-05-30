using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class iOSTwitterAuthSession : TwitterAuthSession
	{
		public const string kUserID = "user-ID";

		public const string kAuthToken = "auth-token";

		public const string kAuthTokenSecret = "auth-token-secret";

		public iOSTwitterAuthSession(IDictionary _sessionJsonDict)
		{
			base.AuthToken = _sessionJsonDict.GetIfAvailable<string>("auth-token");
			base.AuthTokenSecret = _sessionJsonDict.GetIfAvailable<string>("auth-token-secret");
			base.UserID = _sessionJsonDict.GetIfAvailable<string>("user-ID");
		}
	}
}
