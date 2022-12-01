namespace Near
{
    [System.Serializable]
    public struct ExecOutcomeData
    {
        public int gas_burnt;
        public string[] logs, receipt_ids;
    }
}
