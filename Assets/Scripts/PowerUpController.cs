using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if ((transform.position).x < -10){
            Destroy(this.gameObject);
        }
    }
}
