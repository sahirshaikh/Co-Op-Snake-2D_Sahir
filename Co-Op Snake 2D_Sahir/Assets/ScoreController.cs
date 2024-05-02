using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI ScoreUI;
    private int score = 0 ;
    private bool scoremultiplier;

    private void Awake()
    {
        ScoreUI = GetComponent<TextMeshProUGUI>();
        scoremultiplier= false;
    }

    void Start()
    {
        RefreshUI();       
    }

    public void ScoreIncrement(int SC)
    {
        if(scoremultiplier==true)
        {
            score +=SC*2;

        }
        else if(scoremultiplier==false)
        {
            score +=SC;
        }
        

        RefreshUI();
    }

    public void ScoreDecrement(int SC)
    {
        if(scoremultiplier==true)
        {
            score -=SC*2;

        }
        else if(scoremultiplier==false)
        {
            score -=SC;
        }
        RefreshUI();
    }


    void RefreshUI()
    {
        ScoreUI.text = "Score: " + score;
    }

    public void ScoreMultiplier()
    {
        scoremultiplier=true;
        Invoke("NormalScore",10f);
    }

    void NormalScore()
    {
        Debug.Log("normalscore");
        scoremultiplier=false;
    }
}
