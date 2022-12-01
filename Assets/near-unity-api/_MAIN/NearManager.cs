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
        [SerializeField] private JsonRpcProvider _provider;

        public async Task<object> FunctionCall(string accId, string method, object args, Nullable<UInt64> gas = null, Nullable<UInt128> amount = null)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(args == null ? new { } : args));
            return null;
        }

        public async Task<object> SignAndSendTx(string ctrId)
        {
            var status = await _provider.GetStatus();
            var (txHash, tx) = await SignTx("", 1, new ByteArray32
            {
                Buffer = Base58.Decode(status.sync_info.latest_block_hash)
            }, "", "");
            var res = await _provider.SendTx(tx);
            return null;
        }

        public async Task<Tuple<byte[], SignedTxData>> SignTx(string ctrId, ulong nonce, ByteArray32 blockHash, string accId, string nwId)
        {
            return new Tuple<byte[], SignedTxData>(null, new SignedTxData());
        }
    }
}
