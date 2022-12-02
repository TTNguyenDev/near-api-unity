using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Near
{
    public class JsonRpcProvider
    {
        private const string TESTNET_URL = "https://rpc.testnet.near.org";
        private const string MAINNET_URL = "https://rpc.mainnet.near.org";
        private int _id = 0;

        public async Task<StatusData> GetStatus()
        {
            return await SendJsonRpc("status") as StatusData;
        }

        public async Task<FinalExecOutcomeData> SendTx(SignedTxData tx)
        {
            var txData = tx.ToByteArr();
            var body = new object[] { Convert.ToBase64String(txData, 0, txData.Length) };

            return await SendJsonRpc("broad_tx_commit", body) as FinalExecOutcomeData;
        }

        private async Task<object> SendJsonRpc(string method, object[] paramArr = null)
        {
            paramArr ??= new object[] { };
            var body = new
            {
                method,
                paramArr,
                jsonrpc = "2.0",
                id = _id++,
            };

            var json = JsonConvert.SerializeObject(body).Replace("paramArr", "params");
            var data = Encoding.UTF8.GetBytes(json);

            var req = new UnityWebRequest(TESTNET_URL, "POST");
            req.SetRequestHeader("Content-Type", "application/json");
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);

            var sender = req.SendWebRequest();
            while (!sender.isDone)
            {
                await Task.Delay(1000 / 24);
            }

            if (req.result != UnityWebRequest.Result.Success)
            {
                throw new Exception(req.error);
            }

            var res = req.downloadHandler.text;
            return JsonConvert.DeserializeAnonymousType(res, new
            {
                result = new object()
            }).result;
        }
    }
}
