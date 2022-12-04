using System.IO;
using NearClientUnity.Utilities;
using Newtonsoft.Json;

namespace Near
{
    [System.Serializable]
    public class ActData : IByteArrData
    {
        public object args;
        public ActTypeEnum type;

        public byte[] ToByteArr()
        {
            using (var ms = new MemoryStream())
            {
                using (var wr = new NearBinaryWriter(ms))
                {
                    wr.Write((byte)type);

                    var funcCallArgs = GetArgs<FuncCallActData>();
                    wr.Write(funcCallArgs.method);
                    wr.Write((uint)funcCallArgs.args.Length);
                    wr.Write(funcCallArgs.args);
                    wr.Write((ulong)funcCallArgs.gas);
                    wr.Write((UInt128)funcCallArgs.deposit);

                    return ms.ToArray();
                }
            }
        }

        private T GetArgs<T>()
        {
            var json = JsonConvert.SerializeObject(args);
            return JsonConvert.DeserializeObject<T>(json);
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

    [System.Serializable]
    public class FuncCallActData
    {
        public string method;
        public byte[] args;
        public ulong? gas;
        public UInt128 deposit;
    }
}
