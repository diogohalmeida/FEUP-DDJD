
using UnityEngine;
using UnityEngine.UI;

public class ProjectileManager : MonoBehaviour
{

    [SerializeField]
    private GameObject assignmentPrefab;

    [SerializeField]
    private GameObject projectileHolder;

    [SerializeField]
    private Slider shotBar;

    public bool notesPowerup;
    public bool coffeePowerup;

    public bool canShoot;

    private float fillTime;

    // Start is called before the first frame update
    void Start()
    {
        notesPowerup = false;
        coffeePowerup = false;
        canShoot = true;
        fillTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot){
            FillSlider();
        }
    }

    void FillSlider()
    {
        if (shotBar.value >= shotBar.maxValue){
            canShoot = true;
        }
        shotBar.value = Mathf.Lerp(shotBar.minValue, shotBar.maxValue, fillTime);
        fillTime += 1f * Time.deltaTime;
    }

    void ResetSlider()
    {
        fillTime = 0f;
        shotBar.value = shotBar.minValue;
    }

    public void SetPowerUp(int powerUpN, bool status){
        switch (powerUpN){
            case 0:
                coffeePowerup = status;
                break;
            case 1:
                notesPowerup = status;
                break;
        }
    }

    public void Shoot(float x, float y){
        if (canShoot){
            if (!coffeePowerup){
                canShoot = false;
                ResetSlider();
            }
            GameObject assignment = GameObject.Instantiate(assignmentPrefab, new Vector2(x, y), new Quaternion(0, 0, 0, 0));
            assignment.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 0);
            assignment.transform.SetParent(projectileHolder.transform);
            if (notesPowerup){
                assignment = GameObject.Instantiate(assignmentPrefab, new Vector2(x, y), new Quaternion(0, 0, 0, 0));
                assignment.GetComponent<Rigidbody2D>().velocity = new Vector2(8, -1.25f);
                assignment.transform.SetParent(projectileHolder.transform);
                assignment = GameObject.Instantiate(assignmentPrefab, new Vector2(x, y), new Quaternion(0, 0, 0, 0));
                assignment.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 1.25f);
                assignment.transform.SetParent(projectileHolder.transform);
            }
        }
    }
}
