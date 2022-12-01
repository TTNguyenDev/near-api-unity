using System.IO;
using NearClientUnity.Utilities;

namespace Near
{
    [System.Serializable]
    public struct SignedTxData : IByteArrData
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
}
