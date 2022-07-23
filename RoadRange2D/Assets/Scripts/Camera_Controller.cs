using System.Collections;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private Transform car;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;

    private void Start()
    {
        StartCoroutine("Preload");
    }
    IEnumerator Preload()
    {
        Camera.main.orthographicSize = 6000;
        yield return new WaitForSeconds(1f);
        Camera.main.orthographicSize = 8;
    }
    private void LateUpdate()
    {
        if (car == null)
            car = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = new Vector3(car.position.x + offsetX, car.position.y + offsetY, -10);

        if (car.gameObject.GetComponent<Rigidbody2D>().velocity.x > 15)
        {
            if (Camera.main.orthographicSize < 9)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize + .5f * Time.deltaTime;
                offsetX += .75f * Time.deltaTime;
            }
            else
            {
                Camera.main.orthographicSize = 9;
                offsetX = 9.5f;
            }
        }
        else
        {
            if (Camera.main.orthographicSize > 8)
            {
                if (car.gameObject.GetComponent<Rigidbody2D>().velocity.x > 1)
                {
                    Camera.main.orthographicSize = Camera.main.orthographicSize - 1f * Time.deltaTime;
                    offsetX -= 1.5f * Time.deltaTime;
                }
                else
                {
                    Camera.main.orthographicSize = Camera.main.orthographicSize - 4f * Time.deltaTime;
                    offsetX -= 6f * Time.deltaTime;
                }
            }
            else
            {
                Camera.main.orthographicSize = 8;
                offsetX = 8;
            }
        }
    }
}
