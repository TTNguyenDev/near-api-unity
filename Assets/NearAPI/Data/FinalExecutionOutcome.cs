namespace Near
{
    [System.Serializable]
    public class FinalExecutionOutcome
    {
        public FinalExecutionStatus status;
    }

    [System.Serializable]
    public class FinalExecutionStatus
    {
        public ExecutionError Failure;
        public string SuccessValue;
    }

    [System.Serializable]
    public class ExecutionError
    {
        public string error_message, error_type;
    }
}
