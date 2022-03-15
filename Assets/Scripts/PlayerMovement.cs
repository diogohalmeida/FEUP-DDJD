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
            body.AddForce(new Vector2(0, 8), ForceMode2D.Force);
        }

        //Vector3 vertical = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);
        //transform.position = transform.position + 5*vertical * Time.deltaTime;
    }

    private void OnTriggerEnter2D() {
        Debug.Log("Trigger");
    }
}
