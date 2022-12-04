using Newtonsoft.Json.Linq;

namespace Near
{
    [System.Serializable]
    public class StatusData
    {
        public string chain_id, rpc_addr;
        public SyncInfoData sync_info;
    }

    [System.Serializable]
    public class SyncInfoData
    {
        public string latest_block_hash, latest_block_time, latest_state_root;
        public int latest_block_height;
        public bool syncing;
    }
}
