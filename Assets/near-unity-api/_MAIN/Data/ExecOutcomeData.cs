namespace Near
{
    [System.Serializable]
    public class ExecOutcomeData
    {
        public int gas_burnt;
        public string[] logs, receipt_ids;
    }
}
