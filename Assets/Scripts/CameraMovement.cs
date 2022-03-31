using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 4f;
    public int current_selected1 = 0;
    public int current_selected2 = 1;
    public Vector3 startPos;
    public List<Renderer> corridors1;
    public List<Renderer> corridors2;

    float prevSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        corridors1 = new List<Renderer>();
        corridors2 = new List<Renderer>();

        for(int i = 1; i < 9; i++){
            corridors1.Add(GameObject.Find("Corridor" + i).GetComponent<Renderer>());
            corridors2.Add(GameObject.Find("Corridor" + i + "_2").GetComponent<Renderer>());
        }
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime);
        if (transform.position.x < -17.8)
        {
            corridors1[current_selected2].sortingOrder = 1;
            corridors1[current_selected1].sortingOrder = 0;
            transform.position = startPos;
            current_selected1 = current_selected2;
            int randomN = Random.Range(0,8);
            corridors2[randomN].sortingOrder = 1;
            corridors2[current_selected2].sortingOrder = 0;
            current_selected2 = randomN;
        }
    }

    public void UpdateSpeed(float multiplyFactor)
    {
        speed = speed * multiplyFactor;
    }

    public void Stop()
    {
        prevSpeed = speed;
        speed = 0f;
    }

    public void Resume()
    {
        speed = prevSpeed;
    }

}
