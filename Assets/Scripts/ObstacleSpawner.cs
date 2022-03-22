using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    GameObject obstacle;
    Transform bar_left;
    Transform bar_right;
    public Rigidbody2D body;

    public MapController controller;

    bool isActive;
    int currentType;

    public void respawnObstacle(int type){
        obstacle.SetActive(true);
        isActive = true;
        bar_left.localScale = new Vector2(1, 60);
        bar_right.localScale = new Vector2(1, 70);
        if (type == 0){
            // Normal spawn
            currentType = 0;
            obstacle.transform.position = new Vector2(4, Random.Range(-3, 3));
        } else if (type == 1){
            // Bottom
            currentType = 1;
            obstacle.transform.position = new Vector2(15, -3);
        } else if (type == 2){
            // Top
            currentType = 2;
            obstacle.transform.position = new Vector2(15, 3);
        }
        body.velocity = new Vector2(-4f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentType = 0;
        isActive = false;
        obstacle = GameObject.Find("Obstacle");
        body = obstacle.GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(-4f, 0);
        bar_left = obstacle.transform.Find("bar_left");
        bar_right = obstacle.transform.Find("bar_right");
        obstacle.SetActive(false);
        //respawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (obstacle.transform.position[0] <= -16 && isActive){
            obstacle.SetActive(false);
            //respawnObstacle();
            isActive = false;
            if (currentType != 1 && currentType != 2){
                controller.NextSection();
            }
            
        }
        
    }
}
