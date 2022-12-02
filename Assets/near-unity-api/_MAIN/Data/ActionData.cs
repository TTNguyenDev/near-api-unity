using NearClientUnity.Utilities;

namespace Near
{
    [System.Serializable]
    public class ActData : IByteArrData
    {
        public object args;
        public ActTypeEnum type;

        public byte[] ToByteArr()
        {
            throw new System.NotImplementedException();
        }
    }

    public enum ActTypeEnum
    {
        CreateAcc = 0,
        DeployCtr,
        FuncCall,
        Transfer,
        Stake,
        AddKey,
        DelKey,
        DelAcc
    }

    public class FuncCallActData
    {
        public string method;
        public byte[] args;
        public ulong? gas;
        public UInt128 deposit;
    }
}
