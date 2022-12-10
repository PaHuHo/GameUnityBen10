using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreController : MonoBehaviour
{
    public TextMeshProUGUI rankScore1;
    public TextMeshProUGUI rankScore2;
    public TextMeshProUGUI rankScore3;
    public TextMeshProUGUI rankScore4;
    public TextMeshProUGUI rankScore5;
    public TextMeshProUGUI rankScore6;
    public TextMeshProUGUI rankScore7;
    public TextMeshProUGUI rankScore8;
    public TextMeshProUGUI rankScore9;
    public TextMeshProUGUI rankScore10;

    void Start()
    {
        rankScore1.text = GameStats.Stats.highScores[0].ToString();
        rankScore2.text = GameStats.Stats.highScores[1].ToString();
        rankScore3.text = GameStats.Stats.highScores[2].ToString();
        rankScore4.text = GameStats.Stats.highScores[3].ToString();
        rankScore5.text = GameStats.Stats.highScores[4].ToString();
        rankScore6.text = GameStats.Stats.highScores[5].ToString();
        rankScore7.text = GameStats.Stats.highScores[6].ToString();
        rankScore8.text = GameStats.Stats.highScores[7].ToString();
        rankScore9.text = GameStats.Stats.highScores[8].ToString();
        rankScore10.text = GameStats.Stats.highScores[9].ToString();
    }

}
