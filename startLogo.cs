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

    public GameObject TitleMgr;
    TitleManager scrMgr;

    // Start is called before the first frame update
    void Start()
    {
        scrMgr = TitleMgr.GetComponent<TitleManager>();

        rect = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scrMgr.state == scrMgr.GetModeStart())
        {
            this.gameObject.GetComponent<Image>().sprite = btnLogo1;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = btnLogo2;
        }
    }

    //public void changeSprite()
    //{
        
    //}
}
