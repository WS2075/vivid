using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private bool isFadeOut = false;
    private bool isFadeIn = true;

    [SerializeField]
    private float FadeSpeed;

    [SerializeField]
    private Image FadeImage;
    private float red, green, blue, alfa;

    private string aferScene;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        SetRGBA(0, 0, 0, 1);
    }

    private void FadeInStart(Scene scene, LoadSceneMode mode)
    {
        isFadeIn = true;
    }

    public void FadeOutStart(int r, int g, int b, int a, string nextScene)
    {
        SetRGBA(r, g, b, a);
        SetColor();
        isFadeOut = true;
        aferScene = nextScene;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFadeIn)
        {
            alfa -= FadeSpeed * Time.deltaTime;

            SetColor();
            if(alfa <= 0)
            {
                isFadeIn = false;
            }
        }

        if(isFadeOut)
        {
            alfa += FadeSpeed * Time.deltaTime;

            SetColor();
            if(alfa >= 1)
            {
                isFadeOut = false;
                isFadeIn = true;
                SceneManager.LoadScene(aferScene);
            }
        }
    }

    private void SetColor()
    {
        FadeImage.color = new Color(red, green, blue, alfa);
    }

    public void SetRGBA(int r, int g, int b, int a)
    {
        red = r;
        green = g;
        blue = b;
        alfa = a;
    }
}
