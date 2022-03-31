using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField]
    private GameObject coffeePrefab;

    [SerializeField]
    private GameObject arrowPrefab;

    private GameObject coffeeHolder;

    private GameObject arrow;

    Vector3 spawnArrow;

    Color arrowColor;

    public float speed;

    private float timer;
    // Start is called before the first frame update

    public void multiplySpeed(float multiplyFactor)
    {
        speed *= multiplyFactor;
    }
    void Start()
    {
        timer = 0;
        coffeeHolder = GameObject.Find("FlyingCoffeeSpawner");
        float y = this.transform.position.y;
        spawnArrow = new Vector3(8.5f, y, 0);
        arrow = GameObject.Instantiate(arrowPrefab, spawnArrow, new Quaternion(0,0,0,0));
        this.gameObject.GetComponent<AudioSource>().Play();
        arrowColor = arrow.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        timer +=1;
        if(timer % 5 == 0){
            if(timer % 10 == 0){
                arrowColor.a = 1.0f;
            }
            else{
                arrowColor.a = 0.0f;
            }
            arrow.GetComponent<SpriteRenderer>().color = arrowColor;
        }
        if(timer >= 150){
            timer = 0;
            float y = this.transform.position.y;
            Vector3 spawnPoint = new Vector3(10.0f, y, 0);
            GameObject coffee;
            coffee = GameObject.Instantiate(coffeePrefab, spawnPoint, new Quaternion(0,0,0,0));
            coffee.GetComponent<AudioSource>().Play();
            coffee.transform.SetParent(coffeeHolder.transform);
            coffee.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            this.gameObject.GetComponent<AudioSource>().Stop();
            Destroy(this.gameObject);
            Destroy(arrow);
        }
    }
}
