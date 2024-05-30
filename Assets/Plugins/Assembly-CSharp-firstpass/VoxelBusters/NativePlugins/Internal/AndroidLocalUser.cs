using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class AndroidLocalUser : LocalUser
	{
		private const string kLocalUserFriendsKey = "local-user-friends";

		private const string kLocalUserInfoKey = "local-user-info";

		private AndroidUser m_user;

		private IDictionary m_authResponseData;

		public override string Identifier
		{
			get
			{
				if (m_user == null)
				{
					return null;
				}
				return m_user.Identifier;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override string Name
		{
			get
			{
				if (m_user == null)
				{
					return null;
				}
				return m_user.Name;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override string Alias
		{
			get
			{
				if (m_user == null)
				{
					return null;
				}
				return m_user.Alias;
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override bool IsAuthenticated
		{
			get
			{
				AndroidJavaObject plugin = GameServicesAndroid.Plugin;
				if (plugin == null)
				{
					return false;
				}
				return GameServicesAndroid.Plugin.Call<bool>("isSignedIn", new object[0]);
			}
			protected set
			{
				throw new Exception("[GameServices] Only getter is supported.");
			}
		}

		public override User[] Friends { get; protected set; }

		public override void Authenticate(AuthenticationCompletion _onCompletion)
		{
			base.Authenticate(_onCompletion);
			GameServicesAndroid.Plugin.Call("authenticateLocalUser");
		}

		protected override bool NeedsInit()
		{
			return true;
		}

		public override void SignOut(SignOutCompletion _onCompletion)
		{
			base.SignOut(_onCompletion);
			GameServicesAndroid.Plugin.Call("signOut");
		}

		public override void LoadFriends(LoadFriendsCompletion _onCompletion)
		{
			base.LoadFriends(_onCompletion);
			if (IsAuthenticated)
			{
				GameServicesAndroid.Plugin.Call("loadLocalUserFriends", false);
			}
		}

		public override void GetImageAsync(DownloadTexture.Completion _onCompletion)
		{
			if (m_user == null)
			{
				if (_onCompletion != null)
				{
					_onCompletion(null, "The requested operation could not be completed because local player has not been authenticated.");
				}
			}
			else
			{
				m_user.GetImageAsync(_onCompletion);
			}
		}

		protected override void AuthenticationFinished(IDictionary _dataDict)
		{
			m_user = null;
			string ifAvailable = _dataDict.GetIfAvailable<string>("error");
			bool flag = ifAvailable == null;
			if (flag)
			{
				m_authResponseData = _dataDict;
			}
			AuthenticationFinished(flag, ifAvailable);
		}

		protected override void OnInitSuccess(string _error)
		{
			IDictionary ifAvailable = m_authResponseData.GetIfAvailable<IDictionary>("local-user-info");
			m_user = new AndroidUser(ifAvailable);
			m_authResponseData = null;
			base.OnInitSuccess(_error);
		}

		protected override void OnInitFail(string _error)
		{
			m_user = null;
			m_authResponseData = null;
			base.OnInitFail(_error);
		}

		protected override void SignOutFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("error");
			SignOutFinished(ifAvailable == null, ifAvailable);
		}

		protected override void LoadFriendsFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("error");
			IList ifAvailable2 = _dataDict.GetIfAvailable<List<object>>("local-user-friends");
			if (ifAvailable2 != null)
			{
				Friends = AndroidUser.ConvertToUserList(ifAvailable2);
			}
			LoadFriendsFinished(Friends, ifAvailable);
		}
	}
}
