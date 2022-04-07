using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    
    [SerializeField]
    private GameObject obstaclePrefab;

    float speed = -4f;

    public void multiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }

    public void SpawnObstacle(int type)
    {
        int x;
        float y;
        if (type == 0){
            x = 4;
            x = 4;
            int location = Random.Range(0, 3);

            switch (location)
            {
                case 0:
                    // Obstacle on the top
                    y = 3;
                    break;
                case 1:
                    // Obstacle on the bottom
                    y = -3;
                    break;
                case 2:
                default:
                    // Obstacle on the middle
                    y = Random.Range(-0.5f, 0.5f);
                    break;
            }
        } else if (type == 1) {
            x = 15;
            y = -3;
        } else if (type == 2){
            x = 15;
            y = 3;
        } else {
            x = 4;
            int location = Random.Range(0, 3);

            switch (location)
            {
                case 0:
                    // Obstacle on the top
                    y = 3;
                    break;
                case 1:
                    // Obstacle on the bottom
                    y = -3;
                    break;
                case 2:
                default:
                    // Obstacle on the middle
                    y = Random.Range(-0.5f, 0.5f);
                    break;
            }
        }
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(x, y, 0), Quaternion.identity);
        obstacle.transform.Find("bar_left").localScale = new Vector2(1, 60);
        obstacle.transform.Find("bar_right").localScale = new Vector2(1, 70);
        obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        obstacle.transform.SetParent(this.gameObject.transform);
        obstacle.GetComponent<ObstacleBehavior>().SetSpeed(speed);
        obstacle.GetComponent<ObstacleBehavior>().setType(type);

    }

}
