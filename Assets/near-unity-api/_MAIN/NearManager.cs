using System;
using System.Text;
using System.Threading.Tasks;
using NearClientUnity.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Near
{
    public class NearManager : MonoBehaviour
    {
        public async Task<object> FunctionCall(string accId, string method, object args, Nullable<UInt64> gas = null, Nullable<UInt128> amount = null)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(args == null ? new { } : args));
            return null;
        }

        public async Task<object> SignAndSendTx(string ctrId)
        {
            return null;
        }

        public async Task<Tuple<byte[], object>> SignTx(string ctrId, ulong nonce, ByteArray32 blockHash, string accId, string nwId)
        {
            return new Tuple<byte[], object>(null, null);
        }
    }
}
