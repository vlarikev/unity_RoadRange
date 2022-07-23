using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Vector2 parallaxEffectMultiplier;

    [SerializeField]
    private float startPosX;
    [SerializeField]
    private float startPosY;

    private Transform cam;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    void Start()
    {
        transform.position = new Vector3(startPosX, startPosY, transform.position.z);

        cam = Camera.main.transform;
        lastCameraPosition = cam.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit * 1.7f;
    }

    void Update()
    {
        Vector3 deltaMovement = cam.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cam.position;

        if (Mathf.Abs(cam.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cam.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cam.position.x + offsetPositionX, transform.position.y);
        }
    }
}
