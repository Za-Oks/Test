using UnityEngine;

namespace VoxelBusters.NativePlugins.Demo
{
	public class SharingDemo : NPDisabledFeatureDemo
	{
		[SerializeField]
		[Header("Message Sharing Properties")]
		private string m_smsBody = "SMS body holds text message that needs to be sent to recipients";

		[SerializeField]
		private string[] m_smsRecipients;

		[SerializeField]
		[Header("Mail Sharing Properties")]
		private string m_mailSubject = "Demo Mail";

		[SerializeField]
		private string m_plainMailBody = "This is plain text mail.";

		[SerializeField]
		private string m_htmlMailBody = "<html><body><h1>Hello</h1></body></html>";

		[SerializeField]
		private string[] m_mailToRecipients;

		[SerializeField]
		private string[] m_mailCCRecipients;

		[SerializeField]
		private string[] m_mailBCCRecipients;

		[SerializeField]
		[Header("Share Sheet Properties")]
		private eShareOptions[] m_excludedOptions = new eShareOptions[0];

		[SerializeField]
		[Header("Share Properties ")]
		private string m_shareMessage = "share message";

		[SerializeField]
		private string m_shareURL = "http://www.google.com";

		[SerializeField]
		[Tooltip("This demo consideres image relative to Application.persistentDataPath")]
		private string m_shareImageRelativePath;
	}
}
