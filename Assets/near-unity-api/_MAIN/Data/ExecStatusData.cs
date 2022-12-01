namespace Near
{
    [System.Serializable]
    public struct ExecStatusData
    {
        public ExecErrorData Failure;
        public string SuccessReceiptId, SuccessValue;
    }
}
