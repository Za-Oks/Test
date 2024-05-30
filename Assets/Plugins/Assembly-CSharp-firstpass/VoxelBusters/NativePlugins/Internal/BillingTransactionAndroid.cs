using System;
using System.Collections;
using System.Collections.Generic;
using VoxelBusters.UASUtils;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Internal
{
	public sealed class BillingTransactionAndroid : BillingTransaction
	{
		private const string kProductIdentifier = "product-identifier";

		private const string kTransactionDate = "transaction-date";

		private const string kTransactionIdentifier = "transaction-identifier";

		private const string kTransactionReceipt = "transaction-receipt";

		private const string kTransactionState = "transaction-state";

		private const string kVerificationState = "verification-state";

		private const string kError = "error";

		private const string kRawPurchaseData = "raw-purchase-data";

		private const string kNoValidationDone = "no-validation-done";

		private const string kValidationSuccess = "success";

		private const string kValidationFailed = "failed";

		private const string kPurchaseFailed = "failed";

		private const string kPurchaseSuccess = "purchased";

		private const string kPurchaseRefunded = "refunded";

		private const string kPurchaseRestored = "restored";

		public BillingTransactionAndroid(IDictionary _transactionInfo)
		{
			base.ProductIdentifier = _transactionInfo.GetIfAvailable<string>("product-identifier");
			long ifAvailable = _transactionInfo.GetIfAvailable("transaction-date", 0L);
			DateTime dateTime = ifAvailable.ToDateTimeFromJavaTime();
			base.TransactionDateUTC = dateTime.ToUniversalTime();
			base.TransactionDateLocal = dateTime.ToLocalTime();
			base.TransactionIdentifier = _transactionInfo.GetIfAvailable<string>("transaction-identifier");
			base.TransactionReceipt = _transactionInfo.GetIfAvailable<string>("transaction-receipt");
			string ifAvailable2 = _transactionInfo.GetIfAvailable<string>("transaction-state");
			base.TransactionState = GetTransactionState(ifAvailable2);
			string ifAvailable3 = _transactionInfo.GetIfAvailable<string>("verification-state");
			base.VerificationState = GetValidationState(ifAvailable3);
			base.Error = _transactionInfo.GetIfAvailable<string>("error");
			base.RawPurchaseData = _transactionInfo.GetIfAvailable<string>("raw-purchase-data");
		}

		public static IDictionary CreateJSONObject(BillingTransaction _transaction)
		{
			IDictionary dictionary = new Dictionary<string, object>();
			dictionary["product-identifier"] = _transaction.ProductIdentifier;
			dictionary["transaction-date"] = _transaction.TransactionDateUTC.ToJavaTimeFromDateTime();
			dictionary["transaction-identifier"] = _transaction.TransactionIdentifier;
			dictionary["transaction-receipt"] = _transaction.TransactionReceipt;
			dictionary["transaction-state"] = GetTransactionState(_transaction.TransactionState);
			dictionary["verification-state"] = GetValidationState(_transaction.VerificationState);
			dictionary["error"] = _transaction.Error;
			dictionary["raw-purchase-data"] = _transaction.RawPurchaseData;
			return dictionary;
		}

		private static eBillingTransactionVerificationState GetValidationState(string _validationState)
		{
			if (_validationState.Equals("failed"))
			{
				return eBillingTransactionVerificationState.FAILED;
			}
			if (_validationState.Equals("success"))
			{
				return eBillingTransactionVerificationState.SUCCESS;
			}
			if (_validationState.Equals("no-validation-done"))
			{
				return eBillingTransactionVerificationState.NOT_CHECKED;
			}
			DebugUtility.Logger.LogError("Native Plugins", "[BillingTransaction] Invalid state " + _validationState);
			return eBillingTransactionVerificationState.FAILED;
		}

		private static string GetValidationState(eBillingTransactionVerificationState _state)
		{
			switch (_state)
			{
			case eBillingTransactionVerificationState.FAILED:
				return "failed";
			case eBillingTransactionVerificationState.SUCCESS:
				return "success";
			case eBillingTransactionVerificationState.NOT_CHECKED:
				return "no-validation-done";
			default:
				DebugUtility.Logger.LogError("Native Plugins", "[BillingTransaction] Invalid state " + _state);
				return "failed";
			}
		}

		private static eBillingTransactionState GetTransactionState(string _transactionState)
		{
			eBillingTransactionState result = eBillingTransactionState.FAILED;
			if (_transactionState.Equals("failed"))
			{
				result = eBillingTransactionState.FAILED;
			}
			else if (_transactionState.Equals("purchased"))
			{
				result = eBillingTransactionState.PURCHASED;
			}
			else if (_transactionState.Equals("refunded"))
			{
				result = eBillingTransactionState.REFUNDED;
			}
			else if (_transactionState.Equals("restored"))
			{
				result = eBillingTransactionState.RESTORED;
			}
			return result;
		}

		private static string GetTransactionState(eBillingTransactionState _state)
		{
			string result = "failed";
			switch (_state)
			{
			case eBillingTransactionState.FAILED:
				result = "failed";
				break;
			case eBillingTransactionState.PURCHASED:
				result = "purchased";
				break;
			case eBillingTransactionState.REFUNDED:
				result = "refunded";
				break;
			case eBillingTransactionState.RESTORED:
				result = "restored";
				break;
			}
			return result;
		}

		public override void OnCustomVerificationFinished(eBillingTransactionVerificationState _newState)
		{
			base.OnCustomVerificationFinished(_newState);
			DebugUtility.Logger.Log("Native Plugins", "[Billing] On Android, all the transactions are validated implicitely, so this call has no effect.");
		}
	}
}
