using System.IO;
using NearClientUnity.Utilities;

namespace Near
{
    [System.Serializable]
    public class SignedTxData : IByteArrData
    {
        public SignatureData signature;
        public TxData tx;

        public byte[] ToByteArr()
        {
            using (var ms = new MemoryStream())
            {
                using (var wr = new NearBinaryWriter(ms))
                {
                    wr.Write(signature.ToByteArr());
                    wr.Write(tx.ToByteArr());

                    return ms.ToArray();
                }
            }
        }
    }

    [System.Serializable]
    public class SignatureData : IByteArrData
    {
        public ByteArray64 data;

        public byte[] ToByteArr()
        {
            using (var ms = new MemoryStream())
            {
                using (var wr = new NearBinaryWriter(ms))
                {
                    wr.Write((byte)KeyType.Ed25519);
                    wr.Write(data.Buffer);

                    return ms.ToArray();
                }
            }
        }
    }

    [System.Serializable]
    public class TxData : IByteArrData
    {
        public ByteArray32 block_hash;
        public ulong nonce;
        public PublicKey public_key;
        public string receiver_id, signer_id;

        public byte[] ToByteArr()
        {
            using (var ms = new MemoryStream())
            {
                using (var wr = new NearBinaryWriter(ms))
                {
                    wr.Write(signer_id);
                    wr.Write(public_key.ToByteArray());
                    wr.Write(nonce);
                    wr.Write(receiver_id);
                    wr.Write(block_hash.Buffer);

                    return ms.ToArray();
                }
            }
        }
    }
}
