using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class iOSTwitterSession : TwitterSession
	{
		private const string kUserName = "user-name";

		public iOSTwitterSession(IDictionary _sessionJsonDict)
		{
			base.AuthToken = _sessionJsonDict.GetIfAvailable<string>("auth-token");
			base.AuthTokenSecret = _sessionJsonDict.GetIfAvailable<string>("auth-token-secret");
			base.UserID = _sessionJsonDict.GetIfAvailable<string>("user-ID");
			base.UserName = _sessionJsonDict.GetIfAvailable<string>("user-name");
		}
	}
}
