using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    Rigidbody2D body;

    float speed = 0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position).x < -10){
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float newSpeed){
        speed = newSpeed;
    }

    void MultiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }

    public void UpdateVelocity(float multiplyFactor){
        MultiplySpeed(multiplyFactor);
        body.velocity = new Vector2(speed, 0);
    }

    public void Stop(){
        body.velocity = new Vector2(0, 0);
    }

    public void Resume(){
        body.velocity = new Vector2(speed, 0);
    }
}
