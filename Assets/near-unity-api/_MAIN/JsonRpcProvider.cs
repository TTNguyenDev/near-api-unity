using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Near
{
    public class JsonRpcProvider : MonoBehaviour
    {
        private int _id = 0;

        public async Task<StatusData> GetStatus()
        {
            var res = await SendJsonRpc("status");
            return JsonConvert.DeserializeObject<StatusData>(res);
        }

        public async Task<object> SendTx(SignedTxData tx)
        {
            var txData = tx.ToByteArr();
            var body = new object[] { Convert.ToBase64String(txData, 0, txData.Length) };
            var res = await SendJsonRpc("broad_tx_commit", body);
            return null;
        }

        public async Task<string> SendJsonRpc(string method, object[] paramArr = null)
        {
            paramArr ??= new object[] { };
            var body = new
            {
                method,
                paramArr,
                jsonrpc = "2.0",
                id = _id++,
            };

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body).Replace("paramArr", "params"));
            return null;
        }
    }
}
