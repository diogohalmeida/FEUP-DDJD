using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{

    Rigidbody2D body;
    Animator animator;

    public enum KeyState { Space, Off }
    KeyState pressed = KeyState.Off;

    public bool gameOver;

    public CameraMovement cm;
    public ObstacleSpawner os;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        //positions = GetComponentsInChildren<Transform>();
        //n_players = bodies.Length;
        //body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            pressed = KeyState.Space;
        } else {
            pressed = KeyState.Off;
        }

        if (transform.position.y > -3.114){
            animator.SetBool("is_flying", true);
        }
        else {
            animator.SetBool("is_flying", false);
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
        gameOver = true;
        cm.speed = 0f;
        os.body.velocity = new Vector2(0, 0);
    }

}
