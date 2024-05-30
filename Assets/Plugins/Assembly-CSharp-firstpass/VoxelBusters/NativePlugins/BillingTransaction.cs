using System;

namespace VoxelBusters.NativePlugins
{
	public class BillingTransaction
	{
		public string ProductIdentifier { get; protected set; }

		public DateTime TransactionDateUTC { get; protected set; }

		public DateTime TransactionDateLocal { get; protected set; }

		public string TransactionIdentifier { get; protected set; }

		public string TransactionReceipt { get; protected set; }

		public eBillingTransactionState TransactionState { get; protected set; }

		public eBillingTransactionVerificationState VerificationState { get; protected set; }

		public string Error { get; protected set; }

		public string RawPurchaseData { get; protected set; }

		protected BillingTransaction()
		{
		}

		internal BillingTransaction(string _error)
		{
			if (_error == null)
			{
				throw new ArgumentNullException("Error is null.");
			}
			ProductIdentifier = null;
			TransactionDateUTC = DateTime.UtcNow;
			TransactionDateLocal = DateTime.Now;
			TransactionIdentifier = null;
			TransactionReceipt = null;
			TransactionState = eBillingTransactionState.FAILED;
			VerificationState = eBillingTransactionVerificationState.FAILED;
			Error = _error;
			RawPurchaseData = null;
		}

		public virtual void OnCustomVerificationFinished(eBillingTransactionVerificationState _newState)
		{
			VerificationState = _newState;
		}

		public override string ToString()
		{
			return string.Format("[BillingTransaction: ProductIdentifier={0}, TransactionDateUTC={1}, TransactionIdentifier={2}, TransactionState={3}, VerificationState={4}, Error={5}]", ProductIdentifier, TransactionDateUTC, TransactionIdentifier, TransactionState, VerificationState, Error);
		}
	}
}
