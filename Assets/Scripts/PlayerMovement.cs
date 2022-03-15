using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            if (body.drag != 0){
                body.drag = 0;
            }
            body.AddForce(new Vector2(0, 8), ForceMode2D.Force);
        } else if (body.velocity[1] > 0) {
            Debug.Log("Decelerate");
            body.drag = 4;
        } else {
            body.drag = 0;
        }

    }
}
