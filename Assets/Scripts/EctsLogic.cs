
using UnityEngine;

public class EctsLogic : MonoBehaviour
{

    float speed = 0f;

    Rigidbody2D body;

    void Start(){
        body = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(float newSpeed){
        speed = newSpeed;
    }

    public void UpdateVelocity(float multiplyFactor){
        MultiplySpeed(multiplyFactor);
        body.velocity = new Vector2(speed, 0);
    }

    void MultiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }

    public void Stop()
    {
        body.velocity = new Vector2(0, 0);
    }

    public void Resume()
    {
        body.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position[0] <= -12){
            Destroy(this.gameObject);
        }
    }
}
