using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    
    [SerializeField]
    private PowerUpSpawner puSpawner;

    int numberOfVelocityIncreases = 1;

    bool running = true;

    int currentMeters = 0;
    float incrementMetersCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
       if (instance == null)
       {
            instance = this;
       }
    }

    public void stopScore(){
        running = false;
    }

    public void resumeScore(){
        running = true;
    }

    void FixedUpdate()
    {
        if (!running){
            return;
        }
        incrementMetersCounter += 1 * Mathf.Pow(1.1f, numberOfVelocityIncreases);
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

    public void incrementVelocityIncrease(){
        numberOfVelocityIncreases += 1;
    }

    public int GetDistanceTravelled()
    {
        return currentMeters;
    }

}
