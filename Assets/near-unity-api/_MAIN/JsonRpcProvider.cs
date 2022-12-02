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
            var res = await SendJsonRpc("status");
            return JsonConvert.DeserializeObject<StatusData>(res);
        }

        public async Task<string> Query(string path, string data)
        {
            var paramArr = new string[] { path, data };
            return await SendJsonRpc("query", paramArr);
        }

        public async Task<FinalExecOutcomeData> SendTx(SignedTxData tx)
        {
            var txData = tx.ToByteArr();
            var body = new object[] { Convert.ToBase64String(txData, 0, txData.Length) };
            var res = await SendJsonRpc("broad_tx_commit", body);
            return JsonConvert.DeserializeObject<FinalExecOutcomeData>(res);
        }

        private async Task<string> SendJsonRpc(string method, object[] paramArr = null)
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

            var resTxt = req.downloadHandler.text;
            var res = JsonConvert.DeserializeAnonymousType(resTxt, new
            {
                result = new object()
            }).result;

            return JsonConvert.SerializeObject(res);
        }
    }
}
