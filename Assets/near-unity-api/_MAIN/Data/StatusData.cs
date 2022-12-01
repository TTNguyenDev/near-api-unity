using Newtonsoft.Json.Linq;

namespace Near
{
    [System.Serializable]
    public struct StatusData
    {
        public string chain_id, rpc_addr;
        public SyncData sync_info;
    }
}
