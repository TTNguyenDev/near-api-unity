namespace Near
{
    [System.Serializable]
    public class ExecStatusData
    {
        public ExecErrorData Failure;
        public string SuccessReceiptId, SuccessValue;
    }
}
