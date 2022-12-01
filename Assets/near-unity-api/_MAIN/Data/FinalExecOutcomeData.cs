namespace Near
{
    [System.Serializable]
    public struct FinalExecOutcomeData
    {
        public IdExecOutcomeData[] receipts;
        public IdExecOutcomeData transaction;
        public FinalExecStatusData status;
    }
}
