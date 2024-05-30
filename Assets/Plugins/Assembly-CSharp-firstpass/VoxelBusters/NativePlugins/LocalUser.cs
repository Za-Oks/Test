using System;
using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public abstract class LocalUser : User
	{
		public delegate void AuthenticationCompletion(bool _success, string _error);

		public delegate void LoadFriendsCompletion(User[] _users, string _error);

		public delegate void SignOutCompletion(bool _success, string _error);

		private const string kInitErrorMessage = "The requested operation could not be completed because GameServices component initialisation failed.";

		protected AuthenticationCompletion AuthenticationFinishedEvent;

		protected LoadFriendsCompletion LoadFriendsFinishedEvent;

		protected SignOutCompletion SignOutFinishedEvent;

		public abstract bool IsAuthenticated { get; protected set; }

		public abstract User[] Friends { get; protected set; }

		public virtual void Authenticate(AuthenticationCompletion _onCompletion)
		{
			AuthenticationFinishedEvent = _onCompletion;
		}

		protected virtual bool NeedsInit()
		{
			return false;
		}

		protected void Init()
		{
			NPBinding.GameServices.InvokeMethod("LoadAchievementDescriptions", new object[2]
			{
				false,
				(AchievementDescription.LoadAchievementDescriptionsCompletion)delegate(AchievementDescription[] _descriptionList, string _error)
				{
					if (_error == null)
					{
						OnInitSuccess(null);
					}
					else
					{
						OnInitFail("The requested operation could not be completed because GameServices component initialisation failed.");
					}
				}
			}, new Type[2]
			{
				typeof(bool),
				typeof(AchievementDescription.LoadAchievementDescriptionsCompletion)
			});
		}

		public virtual void LoadFriends(LoadFriendsCompletion _onCompletion)
		{
			LoadFriendsFinishedEvent = _onCompletion;
			if (!IsAuthenticated)
			{
				LoadFriendsFinished(null, "The requested operation could not be completed because local player has not been authenticated.");
			}
		}

		public virtual void SignOut(SignOutCompletion _onCompletion)
		{
			SignOutFinishedEvent = _onCompletion;
		}

		public override string ToString()
		{
			return string.Format("[LocalUser: Name={0}, IsAuthenticated={1}]", Name, IsAuthenticated);
		}

		protected virtual void AuthenticationFinished(IDictionary _dataDict)
		{
		}

		protected void AuthenticationFinished(bool _success, string _error)
		{
			Friends = null;
			if (_success)
			{
				if (NeedsInit())
				{
					Init();
				}
				else
				{
					OnInitSuccess(_error);
				}
			}
			else
			{
				OnInitFail(_error);
			}
		}

		protected virtual void OnInitSuccess(string _error)
		{
			if (AuthenticationFinishedEvent != null)
			{
				AuthenticationFinishedEvent(true, _error);
			}
			AuthenticationFinishedEvent = null;
		}

		protected virtual void OnInitFail(string _error)
		{
			if (AuthenticationFinishedEvent != null)
			{
				AuthenticationFinishedEvent(false, _error);
			}
			AuthenticationFinishedEvent = null;
		}

		protected virtual void LoadFriendsFinished(IDictionary _dataDict)
		{
		}

		protected void LoadFriendsFinished(User[] _users, string _error)
		{
			if (LoadFriendsFinishedEvent != null)
			{
				LoadFriendsFinishedEvent(_users, _error);
			}
		}

		protected virtual void SignOutFinished(IDictionary _dataDict)
		{
		}

		protected void SignOutFinished(bool _success, string _error)
		{
			if (SignOutFinishedEvent != null)
			{
				SignOutFinishedEvent(_success, _error);
			}
		}
	}
}
