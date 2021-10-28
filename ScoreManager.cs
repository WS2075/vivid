using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI Cleartext;

    [SerializeField]
    private TextMeshProUGUI Overtext;

    private int Score;
    private int beforScore;
    private string str;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        beforScore = 0;
        str = Score.ToString("000000");
        text.text = str;

        Cleartext.text = str;
        Overtext.text = str;
    }

    // Update is called once per frame
    void Update()
    {
        if(Score != beforScore)
        {
            beforScore = Score;
            str = Score.ToString("000000");
            text.text = str;

            Cleartext.text = str;
            Overtext.text = str;
        }
    }

    public void addScore(int value)
    {
        Score += value;
    }
}
