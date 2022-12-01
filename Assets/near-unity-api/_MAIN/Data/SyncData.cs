using Newtonsoft.Json.Linq;

namespace Near
{
    [System.Serializable]
    public class SyncData
    {
        public string latest_block_hash, latest_block_time, latest_state_root;
        public int latest_block_height;
        public bool syncing;
    }
}
