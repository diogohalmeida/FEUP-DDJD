using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    AudioManager audio;

    float speed = 0f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position[0] >= 9){
            Destroy(this.gameObject);
        }
    }

    public void Stop()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void Resume()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

}
