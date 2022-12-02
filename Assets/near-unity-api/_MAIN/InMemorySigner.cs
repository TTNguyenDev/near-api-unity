using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NearClientUnity.Utilities;
using UnityEngine;

namespace Near
{
    public class InMemorySigner
    {
        private string _ctrId, _accId, _privateKey;

        public InMemorySigner(string ctrId, string accId, string privateKey)
        {
            _ctrId = ctrId;
            _accId = accId;
            _privateKey = privateKey;
        }

        public Tuple<byte[], SignedTxData> SignTx(ulong nonce, ActData[] acts, ByteArray32 blockHash)
        {
            var tx = new TxData
            {
                signer_id = _accId,
                public_key = GetKeyPair().GetPublicKey(),
                nonce = nonce,
                receiver_id = _ctrId,
                actions = acts,
                block_hash = blockHash,
            };

            var txData = tx.ToByteArr();

            return new Tuple<byte[], SignedTxData>(txData, new SignedTxData
            {
                tx = tx,
                signature = new SignatureData
                {
                    data = new ByteArray64
                    {
                        Buffer = SignMsg(txData).SignatureBytes
                    }
                }
            });
        }

        public Signature SignMsg(byte[] msg)
        {
            var data = SHA256.Create().ComputeHash(msg);
            return SignHash(data);
        }

        private Signature SignHash(byte[] hash)
        {
            return GetKeyPair().Sign(hash);
        }

        private KeyPair GetKeyPair()
        {
            return KeyPairEd25519.FromString(_privateKey);
        }
    }
}
