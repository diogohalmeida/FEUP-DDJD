using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{

    [SerializeField]
    private ScoreCounter scoreUI;

    [SerializeField]
    private GameObject coffeePowerup;

    [SerializeField]
    private GameObject notesPowerup;

    public void UpdateScore(int amount){
        scoreUI.UpdateScore(amount);
    }

    public void SetCoffeePowerup(bool status){
        Color powerUpColor = coffeePowerup.GetComponent<Image>().color;
        if(status){
            powerUpColor.a = 1.0f;
        }
        else{
            powerUpColor.a = 0.5f;
        }
        coffeePowerup.GetComponent<Image>().color = powerUpColor;
    }

    public void SetNotesPowerup(bool status){
        Color powerUpColor = notesPowerup.GetComponent<Image>().color;
        if(status){
            powerUpColor.a = 1.0f;
        }
        else{
            powerUpColor.a = 0.5f;
        }
        notesPowerup.GetComponent<Image>().color = powerUpColor;
    }

}
