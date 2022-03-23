using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeacherController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    private Rigidbody2D rb;

    private ScoreCounter scoreUI;

    private bool has_given_score;

    void Start()
    {
        has_given_score = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        scoreUI = GameObject.Find("Score").GetComponent<Text>().GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position).x < -10){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        Destroy(collider.gameObject);
        if (collider.gameObject.name == "assignment(Clone)" && !has_given_score){
            has_given_score = true;
            scoreUI.UpdateScore(5);
            Destroy(this.gameObject);
        }
    }
}
