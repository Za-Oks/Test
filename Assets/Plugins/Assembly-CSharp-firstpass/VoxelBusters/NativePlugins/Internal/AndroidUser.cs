using System.Collections;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class AndroidUser : User
	{
		internal const string kIdentifier = "identifier";

		internal const string kName = "name";

		internal const string kHighResImageURL = "high-res-image-url";

		internal const string kIconImageURL = "icon-image-url";

		internal const string kTimeStamp = "timestamp";

		internal const string kImageFilePath = "image-file-path";

		private string m_imagePath;

		public override string Identifier { get; protected set; }

		public override string Name { get; protected set; }

		public override string Alias { get; protected set; }

		internal AndroidUser(IDictionary _userProfileData)
		{
			if (_userProfileData != null)
			{
				Identifier = _userProfileData.GetIfAvailable<string>("identifier");
				Name = _userProfileData.GetIfAvailable<string>("name");
				m_imagePath = _userProfileData.GetIfAvailable<string>("high-res-image-url");
				Alias = Name;
			}
		}

		internal static AndroidUser ConvertToUser(IDictionary _user)
		{
			if (_user == null)
			{
				return null;
			}
			return new AndroidUser(_user);
		}

		internal static User[] ConvertToUserList(IList _userList)
		{
			if (_userList == null)
			{
				return null;
			}
			int count = _userList.Count;
			User[] array = new AndroidUser[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new AndroidUser(_userList[i] as IDictionary);
			}
			return array;
		}

		protected override void RequestForImage()
		{
			string text = null;
			if (string.IsNullOrEmpty(m_imagePath))
			{
				text = "Image not available!";
				RequestForImageFinished(URL.URLWithString(null), text);
				return;
			}
			string instanceID = GetInstanceID();
			if (m_imagePath.ToLower().StartsWith("http"))
			{
				RequestForImageFinished(URL.URLWithString(m_imagePath), null);
				return;
			}
			GameServicesAndroid.Plugin.Call("loadProfilePicture", instanceID, m_imagePath);
		}

		protected override void RequestForImageFinished(IDictionary _dataDict)
		{
			string ifAvailable = _dataDict.GetIfAvailable<string>("error");
			if (ifAvailable == null)
			{
				string ifAvailable2 = _dataDict.GetIfAvailable<string>("image-file-path");
				RequestForImageFinished(URL.FileURLWithPath(ifAvailable2), null);
			}
			else
			{
				RequestForImageFinished(URL.FileURLWithPath(null), ifAvailable);
			}
		}
	}
}
