using System.Collections;
using System.Runtime.InteropServices;
using VoxelBusters.Utility;

namespace VoxelBusters.UASUtils.Internal
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ProductUpdateInfo
	{
		private const string kVersionNumberKey = "version_number";

		private const string kDownloadLinkKey = "download_link";

		private const string kAssetStoreLink = "asset_store_link";

		private const string kReleaseNoteKey = "release_notes";

		internal bool NewUpdateAvailable { get; private set; }

		internal string VersionNumber { get; private set; }

		internal string DownloadLink { get; private set; }

		internal string AssetStoreLink { get; private set; }

		internal string ReleaseNote { get; private set; }

		internal ProductUpdateInfo(bool _newUpdateAvailable)
		{
			this = default(ProductUpdateInfo);
			NewUpdateAvailable = _newUpdateAvailable;
			VersionNumber = null;
			DownloadLink = null;
			AssetStoreLink = null;
			ReleaseNote = null;
		}

		internal ProductUpdateInfo(string _currentVersion, IDictionary _dataDict)
		{
			this = default(ProductUpdateInfo);
			string ifAvailable = _dataDict.GetIfAvailable<string>("version_number");
			string ifAvailable2 = _dataDict.GetIfAvailable<string>("download_link");
			string ifAvailable3 = _dataDict.GetIfAvailable<string>("asset_store_link");
			string ifAvailable4 = _dataDict.GetIfAvailable<string>("release_notes");
			NewUpdateAvailable = ifAvailable.CompareTo(_currentVersion) > 0;
			VersionNumber = ifAvailable;
			DownloadLink = ifAvailable2;
			AssetStoreLink = ifAvailable3;
			ReleaseNote = ifAvailable4;
		}
	}
}
