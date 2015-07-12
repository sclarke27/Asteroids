using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadyPanel : PanelBaseClass
{

    public Text tapToStartText;

    // Use this for initialization
    new void Start()
    {
        if ((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) && tapToStartText != null)
        {
            tapToStartText.text = "Tap here to start";
        }
    }

}
