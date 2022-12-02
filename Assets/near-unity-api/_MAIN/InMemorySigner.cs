using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NearClientUnity.Utilities;
using UnityEngine;

namespace Near
{
    public class InMemorySigner : MonoBehaviour
    {
        public Tuple<byte[], SignedTxData> SignTx(string ctrId, ulong nonce, ActData[] acts, ByteArray32 blockHash, string accId, string nwId)
        {
            var tx = new TxData
            {
                signer_id = accId,
                public_key = KeyPairEd25519.FromString("ed25519:private-key").GetPublicKey(),
                nonce = nonce,
                receiver_id = ctrId,
                actions = acts,
                block_hash = blockHash,
            };

            var txData = tx.ToByteArr();

            var hash = SHA256.Create().ComputeHash(txData);

            var signature = SignMsg(txData, accId, nwId);
            var signedTx = new SignedTxData
            {
                tx = tx,
                signature = new SignatureData
                {
                    data = new ByteArray64
                    {
                        Buffer = signature.SignatureBytes
                    }
                }
            };

            return new Tuple<byte[], SignedTxData>(txData, signedTx);
        }

        public Signature SignMsg(byte[] msg, string accId, string nwId)
        {
            var data = SHA256.Create().ComputeHash(msg);
            return SignHash(data, accId, nwId);
        }

        public Signature SignHash(byte[] hash, string accId, string nwId)
        {
            return KeyPairEd25519.FromString("ed25519:private-key").Sign(hash);
        }
    }
}
