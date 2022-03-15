using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{

    Rigidbody2D body;
    Animator animator;

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
            body.AddForce(new Vector2(0, 8), ForceMode2D.Force);
        }
        /*if (Input.GetKey(KeyCode.W)){
            body.AddForce(new Vector2(0, 8), ForceMode2D.Force);
        }*/
        if (transform.position.y > -3.114){
            animator.SetBool("is_flying", true);
        }
        else {
            animator.SetBool("is_flying", false);
        }
    }
}
