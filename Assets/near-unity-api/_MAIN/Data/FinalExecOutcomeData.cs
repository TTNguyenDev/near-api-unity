namespace Near
{
    [System.Serializable]
    public class FinalExecOutcomeData
    {
        public FinalExecStatusData status;
    }

    [System.Serializable]
    public class FinalExecStatusData
    {
        public ExecErrorData Failure;
        public string SuccessValue;
    }

    [System.Serializable]
    public class ExecErrorData
    {
        public string error_message, error_type;
    }
}
