using System.IO;
using NearClientUnity.Utilities;

namespace Near
{
    [System.Serializable]
    public struct SignatureData : IByteArrData
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
}
