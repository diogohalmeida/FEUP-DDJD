using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        coffeePowerup.SetActive(status);
    }

    public void SetNotesPowerup(bool status){
        notesPowerup.SetActive(status);
    }

}
