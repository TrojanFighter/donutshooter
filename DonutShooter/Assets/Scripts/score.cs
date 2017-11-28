using System.Collections;
using System.Collections.Generic;
using DonutShooter.Base;
using UnityEngine;


public class score : MonoBehaviour
{
    //private int points;
    private int love;
    public TextMesh scorer;
    public TextMesh lovemesh;


    // Use this for initialization
    void Start()
    {
   
    }

    // Update is called once per frame

    void killed()
    {
        BaseValue.lastScore += 1;
        scorer.text = BaseValue.lastScore.ToString();
    }
    void returned()
    {
        love += 1;
        lovemesh.text = love.ToString();
    }
}
   
    

