using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    Transform obstacle;
    Transform bar_left;
    Transform bar_right;
    public Rigidbody2D body;

    void respawnObstacle(){
        obstacle.transform.position = new Vector2(4, Random.Range(-3, 3));
        bar_left.localScale = new Vector2(1, 60);
        bar_right.localScale = new Vector2(1, 70);
    }

    // Start is called before the first frame update
    void Start()
    {
        obstacle = transform.Find("Obstacle");
        body = obstacle.GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(-4f, 0);
        bar_left = obstacle.transform.Find("bar_left");
        bar_right = obstacle.transform.Find("bar_right");
        respawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacle.transform.position[0] <= -16){
            respawnObstacle();
        }
    }
}
