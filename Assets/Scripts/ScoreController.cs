using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    
    [SerializeField]
    private PowerUpSpawner puSpawner;

    int currentMeters = 0, incrementMetersCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
       if (instance == null)
       {
            instance = this;
       }
    }

    void FixedUpdate()
    {
        incrementMetersCounter += 1;
        if (incrementMetersCounter > 10)
        {
            currentMeters += 1;
            this.GetComponent<Text>().text = currentMeters.ToString().PadLeft(4, '0') + "M";
            incrementMetersCounter = 0;
            if(currentMeters % 500 == 0){
                puSpawner.SpawnPowerUp();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
