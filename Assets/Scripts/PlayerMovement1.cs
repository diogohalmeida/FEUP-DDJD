using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{

    Rigidbody2D body;
    Animator animator;

    public enum KeyState { Space, Off }
    public KeyState pressed = KeyState.Off;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("Space pressed");
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
}
