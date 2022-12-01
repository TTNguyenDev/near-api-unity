namespace Near
{
    [System.Serializable]
    public class FinalExecOutcomeData
    {
        public IdExecOutcomeData[] receipts;
        public IdExecOutcomeData transaction;
        public FinalExecStatusData status;
    }
}
