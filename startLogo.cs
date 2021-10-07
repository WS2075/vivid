using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startLogo : MonoBehaviour
{
    public Sprite btnLogo1;
    public Sprite btnLogo2;
    public float width;
    public float height;
    private RectTransform rect;

    GameObject TitleMgr;
    TitleManager TitleScrpt;

    // Start is called before the first frame update
    void Start()
    {
        TitleMgr = GameObject.Find("TitleManager");
        TitleScrpt = TitleMgr.GetComponent<TitleManager>();

        rect = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleScrpt.state == TitleScrpt.GetModeStart())
        {
            this.gameObject.GetComponent<Image>().sprite = btnLogo1;
            rect.sizeDelta = new Vector2(width, height);
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = btnLogo2;
            rect.sizeDelta = new Vector2(width / 1.5f, height / 1.5f);
        }
    }

    //public void changeSprite()
    //{
        
    //}
}
