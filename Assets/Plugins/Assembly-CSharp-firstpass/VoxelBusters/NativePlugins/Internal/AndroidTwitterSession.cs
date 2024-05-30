using System.Collections;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class AndroidTwitterSession : TwitterSession
	{
		private const string kUserName = "user-name";

		public AndroidTwitterSession(IDictionary _sessionJsonDict)
		{
			base.AuthToken = _sessionJsonDict["auth-token"] as string;
			base.AuthTokenSecret = _sessionJsonDict["auth-token-secret"] as string;
			base.UserID = _sessionJsonDict["user-identifier"] as string;
			base.UserName = _sessionJsonDict["user-name"] as string;
		}
	}
}
