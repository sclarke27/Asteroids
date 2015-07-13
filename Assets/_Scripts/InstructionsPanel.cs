using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionsPanel : PanelBaseClass
{

    public Text tapToStartText;
    public Text pcControls;
    public Text mobileControls;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        if ((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) && tapToStartText != null)
        {
            tapToStartText.text = "Tap here to start";
           // mobileControls.gameObject.SetActive(true);
           // pcControls.gameObject.SetActive(false);
        }
        else
        {
          //  mobileControls.gameObject.SetActive(false);
          //  pcControls.gameObject.SetActive(true);
        }
    }

}
