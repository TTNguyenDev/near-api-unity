using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NearClientUnity.Utilities;
using UnityEngine;

namespace Near
{
    public class InMemorySigner : MonoBehaviour
    {
        public async Task<Tuple<byte[], SignedTxData>> SignTx(string ctrId, ulong nonce, ByteArray32 blockHash, string accId, string nwId)
        {
            var tx = new TxData
            {
                signer_id = accId,
                public_key = KeyPair.FromString("").GetPublicKey(),
                nonce = nonce,
                receiver_id = ctrId,
                block_hash = blockHash,
            };
            var txData = tx.ToByteArr();
            var hash = SHA256.Create().ComputeHash(txData);
            var signature = await SignMsg(txData, accId, nwId);
            var signedTx = new SignedTxData
            {
                tx = tx,
                signature = new SignatureData()
            };

            return new Tuple<byte[], SignedTxData>(txData, signedTx);
        }

        public async Task<Signature> SignMsg(byte[] msg, string accId, string nwId)
        {
            return null;
        }
    }
}
