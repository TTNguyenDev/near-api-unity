using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _btn;

    private void Awake()
    {
        _btn.onClick.AddListener(OnButton);
    }

    private void Start()
    {
        Debug.Log("We all love you very much, Neil. Sometimes not.");
    }

    private void OnButton()
    {
        Debug.Log("Button is clicked!");
    }
}
