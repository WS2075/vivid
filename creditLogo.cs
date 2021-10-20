using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditLogo : MonoBehaviour
{
    public Sprite creditLogo1;
    public Sprite creditLogo2;
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
        if (scrMgr.state == scrMgr.GetModeCredit())
        {
            this.gameObject.GetComponent<Image>().sprite = creditLogo1;
            //rect.sizeDelta = new Vector2(width, height);
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = creditLogo2;
            //rect.sizeDelta = new Vector2(width / 1.5f, height / 1.5f);
        }
    }
}
