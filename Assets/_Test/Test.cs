using System.Text;
using NearClientUnity.Utilities.Ed25519;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Text _log;

    private void Awake()
    {
        _btn.onClick.AddListener(OnButton);
    }

    private void Start()
    {
        Log("We all love you very much, Neil. Sometimes not.");

        try
        {
            var str = "Ayo, what's up bro?";
            var key = Ed25519.ExpandedPrivateKeyFromSeed(Encoding.ASCII.GetBytes(str));
            Log(JsonConvert.SerializeObject(key));
        }
        catch (System.Exception err)
        {
            Log(JsonConvert.SerializeObject(err));
        }
    }

    private void OnButton()
    {
        Log("Button is clicked!");
    }

    private void Log(string log)
    {
        _log.text = log;
        Debug.Log(log);
    }
}
