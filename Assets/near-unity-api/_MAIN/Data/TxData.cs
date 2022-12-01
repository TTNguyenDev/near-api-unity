using System.IO;
using NearClientUnity.Utilities;

namespace Near
{
    [System.Serializable]
    public struct TxData : IByteArrData
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
