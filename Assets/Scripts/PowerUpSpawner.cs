using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject coffeePrefab;
    [SerializeField]
    private GameObject notesPrefab;
    [SerializeField]
    private GameObject obstacleHolder;

    private float speed = -4f;

    public void multiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }


    public void SpawnPowerUp(){
        int powerUpN = Random.Range(0,2);
        float x = 10.0f;
        float y = Random.Range(-3.0f, 3.7f);
        Vector3 spawnPoint = new Vector3(x, y, 0);
        bool valid = false;
        while (!valid){
            valid = true;
            foreach (Transform obstacle in obstacleHolder.transform){
                if (obstacle.position[0] >= -9){
                    if (spawnPoint.y <= obstacle.position[1] + 0.95f && spawnPoint.y >= obstacle.position[1] - 1.33f){
                        valid = false;
                        break;
                    }
                }
            }
            if (!valid){
                spawnPoint.y = Random.Range(-3.0f, 3.7f);
            }
        }
        GameObject powerup;
        if(powerUpN == 0){ // spawn coffee
            powerup = GameObject.Instantiate(coffeePrefab, spawnPoint, Quaternion.identity);
            powerup.transform.parent = this.gameObject.transform;
            powerup.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else{ // spawn notes
            powerup = GameObject.Instantiate(notesPrefab, spawnPoint, Quaternion.identity);
            powerup.transform.parent = this.gameObject.transform;
            powerup.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
    }
    
}
