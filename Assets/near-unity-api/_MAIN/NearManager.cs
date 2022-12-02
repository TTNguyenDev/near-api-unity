using System;
using System.Security.Cryptography;
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
        [SerializeField] private InMemorySigner _signer;

        public async Task<object> FunctionCall(string accId, string method, object args, ulong? gas = null, Nullable<UInt128> amount = null)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(args == null ? new { } : args));
            var act = new ActData
            {
                type = ActTypeEnum.FuncCall,
                args = new FuncCallActData
                {
                    method = method,
                    args = data,
                    gas = gas ?? 0,
                    deposit = amount ?? 0
                }
            };

            return await SignAndSendTx("", new ActData[] { act });
        }

        public async Task<FinalExecOutcomeData> SignAndSendTx(string ctrId, ActData[] acts)
        {
            var status = await _provider.GetStatus();

            var (txHash, tx) = _signer.SignTx(ctrId, 1, acts, new ByteArray32
            {
                Buffer = Base58.Decode(status.sync_info.latest_block_hash)
            }, "acc-id", "network-id");

            var res = await _provider.SendTx(tx);
            if (res.status != null && res.status.Failure != null)
            {
                throw new Exception();
            }

            return res;
        }
    }
}
