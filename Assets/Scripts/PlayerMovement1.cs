using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement1 : MonoBehaviour
{

    Rigidbody2D body;
    Animator animator;

    SpriteRenderer spriteRenderer;

    public Sprite deadSprite;

    public enum KeyState { Space, Off }
    KeyState pressed = KeyState.Off;

    public bool gameOver;

    bool canMove;
    public CameraMovement cm;
    public ObstacleSpawner os;

    public Text scoreUI;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
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

            if (transform.position.y > -3.0){
                animator.SetBool("is_flying", true);
            }
            else {
                animator.SetBool("is_flying", false);
            }
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
            score++;
            scoreUI.text = score.ToString();
            Destroy(collider.gameObject);
        } else if (collider.gameObject.name =="Obstacle"){
            // Hits FEUP banner
            gameOver = true;
            canMove = false;
            cm.speed = 0f;
            os.body.velocity = new Vector2(0, 0);
            animator.enabled = false;
            spriteRenderer.sprite = deadSprite;
        }
    }
}
