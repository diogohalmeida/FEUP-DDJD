using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement1 : MonoBehaviour
{

    Rigidbody2D body;
    Animator animator;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite deadSprite;

    public enum KeyState { Space, Off }
    KeyState pressed = KeyState.Off;

    public bool gameOver;

    bool canMove;

    [SerializeField]
    private CameraMovement cm;

    [SerializeField]
    private ObstacleSpawner os;

    [SerializeField]
    private MapController controller;

    [SerializeField]
    private ProjectileManager pm;

    [SerializeField]
    private GameObject coinSequence;

    [SerializeField]
    private GameObject teachersHolder;

    [SerializeField]
    private GameObject projectileHolder;

    [SerializeField]
    private ScoreCounter scoreUI;

    [SerializeField]
    public GameObject scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        gameOver = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        //positions = GetComponentsInChildren<Transform>();
        //n_players = bodies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove){
            if (Input.GetKey(KeyCode.Space)){
                pressed = KeyState.Space;
            } else {
                pressed = KeyState.Off;
            }

            if (Input.GetKeyDown(KeyCode.P)){
                pm.Shoot(this.gameObject.transform.position[0], this.gameObject.transform.position[1]);
            }

            if (transform.position.y > -3.0){
                animator.SetBool("is_flying", true);
            }
            else {
                animator.SetBool("is_flying", false);
            }

            reduceUIOpacity();
        }
    }

    void FixedUpdate()
    {
        if (pressed == KeyState.Space){
            
            if (body.drag != 0){
                body.drag = 0;
            }
            body.AddForce(new Vector2(0, 50), ForceMode2D.Force);
        } else if (body.velocity[1] > 0) {
            body.drag = 4;
        } else {
            body.drag = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "ects(Clone)"){
            // Hits ects
            this.GetComponent<AudioSource>().Play();
            scoreUI.UpdateScore(1);
            Destroy(collider.gameObject);
        } else if (collider.gameObject.name =="Obstacle" || collider.gameObject.name =="apr(Clone)" || collider.gameObject.name =="as(Clone)" || collider.gameObject.name =="cbm(Clone)"){
            // Hits FEUP banner or teacher
            controller.spawnActive = false;
            pressed = KeyState.Off;
            os.body.velocity = new Vector2(0, 0);
            os.body.angularVelocity = 0.0f;
            os.body.gravityScale = 1.0f;
            gameOver = true;
            canMove = false;
            cm.speed = 0f;
            stopCoins();
            stopProjectiles();
            stopTeachers();
            animator.enabled = false;
            spriteRenderer.sprite = deadSprite;
        }
    }

    void stopCoins()
    {
        foreach (Transform coin in coinSequence.transform){
            coin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void stopProjectiles()
    {
        foreach (Transform projectile in projectileHolder.transform){
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void stopTeachers()
    {
        foreach (Transform teacher in teachersHolder.transform){
            teacher.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void reduceUIOpacity(){
        if (transform.position.y > 2.0){
            GameObject board = scoreBoard.transform.GetChild (0).gameObject;
            //board.GetComponent<Image>().color.a = 0.1f;
        }
    }
}
