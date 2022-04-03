using UnityEngine;

public class TeacherController : MonoBehaviour
{

    private IngameUIManager uiManager;
    
    [SerializeField]
    private GameObject extraECTSPrefab;
    private Transform playerTransform;

    Rigidbody2D body;

    [SerializeField]
    private float cbmYSpeed;

    private int type = 0; // 1 - apr, 2 - as, 3 - cbm

    private bool has_given_score;
    private bool fading;
    int counter = 0;

    float speed = 0f;

    bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        has_given_score = false;
        uiManager = GameObject.Find("IngameUIManager").GetComponent<IngameUIManager>();
        playerTransform = GameObject.Find("main_char_m").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) return;
        if ((transform.position).x < -10){
            Destroy(this.gameObject);
        } else if (transform.position.y <= -3 && body.velocity.y < 0){
            body.velocity = new Vector2(body.velocity.x, -body.velocity.y);
        } else if (transform.position.y >= 3.75 && body.velocity.y > 0){
            body.velocity = new Vector2(body.velocity.x, -body.velocity.y);
        }

        if (type == 3){
            if (playerTransform.position.y >= this.gameObject.transform.position.y){
                body.velocity = new Vector2(body.velocity.x, cbmYSpeed);
            } else {
                body.velocity = new Vector2(body.velocity.x, -cbmYSpeed);
            }
        }

    }

    void FixedUpdate(){
        if(fading && counter < 50){
            counter += 1;
            Color newColor = GetComponent<SpriteRenderer>().color;
            newColor.a = 1.0f - counter*0.02f;
            GetComponent<SpriteRenderer>().color = newColor;
        }
        if(counter >= 50){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "assignment(Clone)" && !has_given_score){
            GetComponent<AudioSource>().Play();
            Destroy(collider.gameObject);
            has_given_score = true;
            uiManager.UpdateScore(5);
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+1.25f, 0);
            GameObject extrascore;
            extrascore = GameObject.Instantiate(extraECTSPrefab, spawnPoint, new Quaternion(0,0,0,0));
            Destroy(GetComponent<BoxCollider2D>());
            GetComponent<Animator>().SetBool("death",true);
            fading = true;
        }
    }


    public void SetSpeedAndType(float newSpeed, int newType){
        speed = newSpeed;
        type = newType;
    }

    void MultiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }

    public void UpdateVelocity(float multiplyFactor){
        MultiplySpeed(multiplyFactor);
        body.velocity = new Vector2(speed, body.velocity.y);
    }

    public void Stop(){
        running = false;
        body.velocity = new Vector2(0, 0);
    }

    public void Resume(){
        running = true;
        body.velocity = new Vector2(speed, 0);
    }
}
