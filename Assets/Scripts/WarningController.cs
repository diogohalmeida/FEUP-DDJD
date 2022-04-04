using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField]
    private GameObject coffeePrefab;

    [SerializeField]
    private GameObject arrowPrefab;

    private GameObject coffeeHolder;

    private GameObject arrow;

    Vector3 spawnArrow;

    Color arrowColor;

    float speed = -10f;

    private float timer;

    private bool running = true;

    private AudioSource warningSound;

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        coffeeHolder = GameObject.Find("FlyingCoffeeSpawner");
        float y = this.transform.position.y;
        spawnArrow = new Vector3(6.4f, y, 0);
        arrow = GameObject.Instantiate(arrowPrefab, spawnArrow, new Quaternion(0,0,0,0));
        warningSound = GetComponent<AudioSource>();
        warningSound.Play();
        arrowColor = arrow.GetComponent<SpriteRenderer>().color;
    }

    public void stopTimer(){
        running = false;
    }

    public void resumeTimer(){
        running = true;
    }

    public void pauseSound(){
        warningSound.Pause();
    }

    public void unpauseSound(){
        warningSound.UnPause();
    }

    void FixedUpdate()
    {
        if (running){
            timer +=1;
            if(timer % 5 == 0){
                if(timer % 10 == 0){
                    arrowColor.a = 1.0f;
                }
                else{
                    arrowColor.a = 0.0f;
                }
                arrow.GetComponent<SpriteRenderer>().color = arrowColor;
            }
            if(timer >= 150){
                timer = 0;
                float y = this.transform.position.y;
                Vector3 spawnPoint = new Vector3(10.0f, y, 0);
                GameObject coffee;
                coffee = GameObject.Instantiate(coffeePrefab, spawnPoint, new Quaternion(0,0,0,0));
                coffee.GetComponent<AudioSource>().Play();
                coffee.transform.SetParent(coffeeHolder.transform);
                coffee.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
                coffee.GetComponent<FlyingCoffeeController>().SetSpeed(speed);
                this.gameObject.GetComponent<AudioSource>().Stop();
                Destroy(this.gameObject);
                Destroy(arrow);
            }
        }
    }
}
