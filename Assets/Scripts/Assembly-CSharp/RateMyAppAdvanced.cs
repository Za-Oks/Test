using UnityEngine;
using VoxelBusters.NativePlugins;

public class RateMyAppAdvanced : MonoBehaviour, IRateMyAppDelegate
{
	private void Awake()
	{
		NPBinding.Utility.RateMyApp.Delegate = this;
	}

	public bool CanShowRateMyAppDialog()
	{
		return true;
	}

	public void OnBeforeShowingRateMyAppDialog()
	{
		NPSettings.Utility.RateMyApp.Message = "My own localised message";
	}
}
