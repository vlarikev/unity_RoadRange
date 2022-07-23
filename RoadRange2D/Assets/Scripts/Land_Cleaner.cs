using UnityEngine;

public class Land_Cleaner : MonoBehaviour
{
    private void LateUpdate()
    {
        if (Camera.main.transform.position.x > transform.position.x + 100)
            transform.position = new Vector3(Camera.main.transform.position.x - 100, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
            Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Fuel")
            Destroy(collision.gameObject);

        if (collision.gameObject.name == "SpeedBump(Clone)")
            Destroy(collision.gameObject);

        if (collision.gameObject.name == "Stone(Clone)")
            Destroy(collision.gameObject);
    }
}
