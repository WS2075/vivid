using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionLogo : MonoBehaviour
{
    public Sprite optionLogo1;
    public Sprite optionLogo2;

    public GameObject TitleMgr;
    TitleManager scrMgr;

    // Start is called before the first frame update
    void Start()
    {
        scrMgr = TitleMgr.GetComponent<TitleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scrMgr.state == TitleManager.MODE_STATE.MODE_OPTION)
        {
            this.gameObject.GetComponent<Image>().sprite = optionLogo1;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = optionLogo2;
        }
    }
}
