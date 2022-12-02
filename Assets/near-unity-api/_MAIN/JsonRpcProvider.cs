using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Near
{
    public class JsonRpcProvider : MonoBehaviour
    {
        private const string TESTNET = "https://rpc.testnet.near.org";
        private const string MAINNET = "https://rpc.testnet.near.org";
        private int _id = 0;

        public async Task<StatusData> GetStatus()
        {
            var res = await SendJsonRpc("status");
            return JsonConvert.DeserializeObject<StatusData>(res);
        }

        public async Task<FinalExecOutcomeData> SendTx(SignedTxData tx)
        {
            var txData = tx.ToByteArr();
            var body = new object[] { Convert.ToBase64String(txData, 0, txData.Length) };
            var res = await SendJsonRpc("broad_tx_commit", body);
            return JsonConvert.DeserializeObject<FinalExecOutcomeData>(res);
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

            var req = new UnityWebRequest(TESTNET, "POST");
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

            return req.downloadHandler.text;
        }
    }
}
