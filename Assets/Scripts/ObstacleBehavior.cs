using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{

    int type;
    private MapController controller;

    Rigidbody2D body;

    float speed = 0f;

    void Start()
    {
        controller = GameObject.Find("MapController").GetComponent<MapController>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position[0] <= -16){
            Destroy(this.gameObject);
            if (type != 1 && type != 2){
                controller.NextSection();
            }
        }
    }

    public void setType(int t){
        type = t;
    }

    public void SetSpeed(float newSpeed){
        speed = newSpeed;
    }

    void MultiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }

    public void UpdateVelocity(float multiplyFactor)
    {
        MultiplySpeed(multiplyFactor);
        body.velocity = new Vector2(speed, 0);
    }

    public void Stop()
    {
        body.velocity = new Vector2(0, 0);
    }

    public void Resume()
    {
        body.velocity = new Vector2(speed, 0);
    }
}
