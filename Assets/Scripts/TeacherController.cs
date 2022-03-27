using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeacherController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;

    private IngameUIManager uiManager;
    
    [SerializeField]
    private GameObject extraECTSPrefab;

    private bool has_given_score;

    // Start is called before the first frame update
    void Start()
    {
        has_given_score = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        uiManager = GameObject.Find("IngameUIManager").GetComponent<IngameUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position).x < -10){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "assignment(Clone)" && !has_given_score){
            Destroy(collider.gameObject);
            has_given_score = true;
            uiManager.UpdateScore(5);
            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+1.25f, 0);
            GameObject extrascore;
            extrascore = GameObject.Instantiate(extraECTSPrefab, spawnPoint, new Quaternion(0,0,0,0));
            Destroy(this.gameObject);
        }
    }
}
