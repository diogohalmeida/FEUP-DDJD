using UnityEngine;

public class TeacherController : MonoBehaviour
{

    private IngameUIManager uiManager;
    
    [SerializeField]
    private GameObject extraECTSPrefab;

    private bool has_given_score;
    private bool fading;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        has_given_score = false;
        uiManager = GameObject.Find("IngameUIManager").GetComponent<IngameUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position).x < -10){
            Destroy(this.gameObject);
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
}
