using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins.Internal;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins
{
	public class RateMyApp
	{
		private RateMyAppSettings m_settings;

		private IRateMyAppViewController m_viewController;

		private IRateMyAppKeysCollection m_keysCollection;

		private IRateMyAppEventResponder m_eventResponder;

		private IRateMyAppOperationHandler m_operationHandler;

		public IRateMyAppDelegate Delegate { private get; set; }

		public static RateMyApp Create(IRateMyAppViewController _viewController, IRateMyAppKeysCollection _keysCollection, IRateMyAppEventResponder _eventResponder, IRateMyAppOperationHandler _operationHandler, RateMyAppSettings _settings)
		{
			RateMyApp rateMyApp = new RateMyApp();
			rateMyApp.m_viewController = _viewController;
			rateMyApp.m_keysCollection = _keysCollection;
			rateMyApp.m_eventResponder = _eventResponder;
			rateMyApp.m_operationHandler = _operationHandler;
			rateMyApp.m_settings = _settings;
			RateMyApp rateMyApp2 = rateMyApp;
			rateMyApp2.MarkIfLaunchIsFirstTime();
			return rateMyApp2;
		}

		public void AskForReview()
		{
			if (CanAskForReview())
			{
				m_operationHandler.Execute(ShowDialogRoutine());
			}
		}

		public void AskForReviewNow()
		{
			ShowDialog();
		}

		public void RecordAppLaunch()
		{
			int value = PlayerPrefs.GetInt(m_keysCollection.AppUsageCountKeyName, 0) + 1;
			PlayerPrefs.SetInt(m_keysCollection.AppUsageCountKeyName, value);
			PlayerPrefs.Save();
		}

		private void MarkIfLaunchIsFirstTime()
		{
			bool flag = PlayerPrefs.GetInt(m_keysCollection.ShowPromptAfterKeyName, -1) == -1 || IsFirstTimeLaunch();
			PlayerPrefs.SetInt(m_keysCollection.IsFirstTimeLaunchKeyName, flag ? 1 : 0);
		}

		private int GetAppUsageCount()
		{
			return PlayerPrefs.GetInt(m_keysCollection.AppUsageCountKeyName, 0);
		}

		private bool IsFirstTimeLaunch()
		{
			return PlayerPrefs.GetInt(m_keysCollection.IsFirstTimeLaunchKeyName, 0) == 1;
		}

		private bool CanAskForReview()
		{
			try
			{
				if (PlayerPrefs.GetInt(m_keysCollection.DontShowKeyName, 0) == 1)
				{
					return false;
				}
				string @string = PlayerPrefs.GetString(m_keysCollection.VersionLastRatedKeyName);
				if (!string.IsNullOrEmpty(@string))
				{
					string bundleVersion = PlayerSettings.GetBundleVersion();
					if (bundleVersion.CompareTo(@string) <= 0)
					{
						return false;
					}
				}
				DateTime utcNow = DateTime.UtcNow;
				int num = PlayerPrefs.GetInt(m_keysCollection.ShowPromptAfterKeyName, -1);
				if (num == -1)
				{
					num = m_settings.ShowFirstPromptAfterHours;
					PlayerPrefs.SetInt(m_keysCollection.ShowPromptAfterKeyName, m_settings.ShowFirstPromptAfterHours);
					PlayerPrefs.SetString(m_keysCollection.PromptLastShownKeyName, utcNow.ToString());
				}
				string string2 = PlayerPrefs.GetString(m_keysCollection.PromptLastShownKeyName);
				DateTime dateTime = DateTime.Parse(string2);
				int num2 = (int)(utcNow - dateTime).TotalHours;
				int appUsageCount = GetAppUsageCount();
				if (num > num2)
				{
					return false;
				}
				if (!IsFirstTimeLaunch() && appUsageCount <= m_settings.SuccessivePromptAfterLaunches)
				{
					return false;
				}
				PlayerPrefs.SetInt(m_keysCollection.IsFirstTimeLaunchKeyName, 0);
				PlayerPrefs.SetInt(m_keysCollection.AppUsageCountKeyName, 0);
				PlayerPrefs.SetString(m_keysCollection.PromptLastShownKeyName, utcNow.ToString());
				return true;
			}
			finally
			{
				PlayerPrefs.Save();
			}
		}

		private IEnumerator ShowDialogRoutine()
		{
			if (Delegate != null)
			{
				while (!Delegate.CanShowRateMyAppDialog())
				{
					yield return new WaitForSeconds(1f);
				}
			}
			ShowDialog();
		}

		private void ShowDialog()
		{
			if (Delegate != null)
			{
				Delegate.OnBeforeShowingRateMyAppDialog();
			}
			List<string> list = new List<string>();
			list.Add(m_settings.RateItButtonText);
			list.Add(m_settings.RemindMeLaterButtonText);
			if (!string.IsNullOrEmpty(m_settings.DontAskButtonText))
			{
				list.Add(m_settings.DontAskButtonText);
			}
			m_viewController.ShowDialog(m_settings.Title, m_settings.Message, list.ToArray(), delegate(string _buttonName)
			{
				if (string.Equals(_buttonName, m_settings.RemindMeLaterButtonText))
				{
					OnPressingRemindMeLaterButton();
				}
				else if (string.Equals(_buttonName, m_settings.RateItButtonText))
				{
					OnPressingRateItButton();
				}
				else
				{
					OnPressingDontShowButton();
				}
				PlayerPrefs.Save();
			});
		}

		private void OnPressingRemindMeLaterButton()
		{
			PlayerPrefs.SetInt(m_keysCollection.ShowPromptAfterKeyName, m_settings.SuccessivePromptAfterHours);
			m_eventResponder.OnRemindMeLater();
		}

		private void OnPressingRateItButton()
		{
			string bundleVersion = PlayerSettings.GetBundleVersion();
			PlayerPrefs.SetString(m_keysCollection.VersionLastRatedKeyName, bundleVersion);
			m_eventResponder.OnRate();
		}

		private void OnPressingDontShowButton()
		{
			PlayerPrefs.SetInt(m_keysCollection.DontShowKeyName, 1);
			m_eventResponder.OnDontShow();
		}
	}
}
