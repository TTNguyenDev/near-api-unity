using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NearClientUnity.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Near
{
    public class NearManager
    {
        private const ulong DEFAULT_GAS = 300000000000000;
        private const ushort DEFAULT_DEPOSIT = 0;
        private JsonRpcProvider _provider;
        private InMemorySigner _signer;
        private string _accId;
        private AccessKeyData _accessKey = null;

        public NearManager(string ctrId, string accId, string privateKey)
        {
            _accId = accId;
            _provider = new JsonRpcProvider();
            _signer = new InMemorySigner(ctrId, accId, privateKey);
        }

        public async Task<object> FunctionCall(string method, object args, ulong? gas = null, Nullable<UInt128> amount = null)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(args == null ? new { } : args));
            var act = new ActData
            {
                type = ActTypeEnum.FuncCall,
                args = new FuncCallActData
                {
                    method = method,
                    args = data,
                    gas = gas ?? DEFAULT_GAS,
                    deposit = amount ?? (uint)DEFAULT_DEPOSIT
                }
            };

            return await SignAndSendTx(new ActData[] { act });
        }

        private async Task<FinalExecOutcomeData> SignAndSendTx(ActData[] acts)
        {
            if (_accessKey == null)
            {
                try
                {
                    var pubKey = _signer.GetKeyPair().GetPublicKey();
                    var qRes = await _provider.Query($"access_key/{_accId}/{pubKey.ToString()}", "");
                    _accessKey = JsonConvert.DeserializeObject<AccessKeyData>(qRes);
                }
                catch (Exception err)
                {
                    _accessKey = null;
                    throw err;
                }
            }

            var status = await _provider.GetStatus();

            var (txHash, tx) = _signer.SignTx(++_accessKey.nonce, acts, new ByteArray32
            {
                Buffer = Base58.Decode(status.sync_info.latest_block_hash)
            });

            var res = await _provider.SendTx(tx);
            if (res.status != null && res.status.Failure != null)
            {
                throw new Exception();
            }

            return res;
        }
    }
}
