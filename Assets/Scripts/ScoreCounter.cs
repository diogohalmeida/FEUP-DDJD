using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void UpdateScore(int amount){
        score += amount;
        this.GetComponent<Text>().text = score.ToString() + "x";
    }
}
