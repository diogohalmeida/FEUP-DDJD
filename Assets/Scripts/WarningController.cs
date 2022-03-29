using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField]
    private GameObject coffeePrefab;

    private GameObject coffeeHolder;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        timer +=1;
        if(timer >= 300){
            timer = 0;
            float y = this.transform.position.y;
            Vector3 spawnPoint = new Vector3(10.0f, y, 0);
            GameObject coffee;
            coffee = GameObject.Instantiate(coffeePrefab, spawnPoint, new Quaternion(0,0,0,0));
            coffee.transform.SetParent(coffeeHolder.transform);
            coffee.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            Destroy(this.gameObject);
        }
    }
}
