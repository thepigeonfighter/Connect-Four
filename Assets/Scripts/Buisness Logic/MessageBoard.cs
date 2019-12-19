using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoard : MonoBehaviour
{
    public static MessageBoard Instance;
    private Text _messageText;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        _messageText = GetComponent<Text>();
    }

    public void ShowMessage(string text)
    {
        _messageText.text = text;
    }
}
