using UnityEngine;

public class Land_Controller : MonoBehaviour
{
    [Header("Stage offset")]
    [SerializeField] private int offsetX;

    [Header("Stage parts")]
    [SerializeField] private GameObject part1;
    [SerializeField] private GameObject part2;

    [Header("Spawnable items")]
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject fuelCan;
    [SerializeField] private GameObject speedBump;
    [SerializeField] private GameObject stone;

    [Header("Reward")]
    [SerializeField] private int rewardScale;
    [SerializeField] private int coinStartPosX;
    [SerializeField] private int coinScale;
    [SerializeField] private int fuelStartPosX;
    [SerializeField] private int fuelScale;

    private int count = 1;
    private bool isSwitch = false;

    private Vector3 highestPos;
    private bool posCheck = false;

    private int speedBumpPosX;
    private int stonePosX;

    private void Start()
    {
        coinStartPosX = coinStartPosX - 100;
        fuelStartPosX = fuelStartPosX - 100;
    }

    private void LateUpdate()
    {
        LandSwap();
        SpawnItems();
    }

    private void SpawnItems()
    {
        if (Camera.main.transform.position.x > highestPos.x)
        {
            highestPos = Camera.main.transform.position;
            posCheck = true;
        }

        if (highestPos.x > rewardScale)
        {
            rewardScale += 1000;

            if (coinScale > 8)
                coinScale -= 5;
            else
                coinScale = 7;
        }

        transform.position = new Vector3(highestPos.x + 100, transform.position.y);

        if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), .1f) != null)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), .1f).tag == "Ground" && posCheck)
            {
                posCheck = false;
                if (highestPos.x > fuelStartPosX)
                {
                    fuelStartPosX = (int)highestPos.x + fuelScale;
                    fuelScale += 5;
                    Instantiate(fuelCan, transform.position + Vector3.up * 1.5f, Quaternion.identity);
                }
                else if (highestPos.x > coinStartPosX)
                {
                    coinStartPosX = (int)highestPos.x + coinScale;
                    Instantiate(coin, transform.position + Vector3.up * 1.5f, Quaternion.identity);
                }

                // Speed bumpes for stage0.
                if (highestPos.x > speedBumpPosX && PlayerPrefs.GetInt("currentStageId") == 0)
                {
                    speedBumpPosX = (int)highestPos.x + 60;
                    Instantiate(speedBump, transform.position + Vector3.down * 0.1f, Quaternion.identity);
                }

                // Stones for stage1.
                else if (highestPos.x > stonePosX && PlayerPrefs.GetInt("currentStageId") == 1)
                {
                    stonePosX = (int)highestPos.x + 25;
                    Instantiate(stone, transform.position + Vector3.down * 0.1f, Quaternion.identity);
                }

                highestPos.x += 5;
                transform.position = new Vector3(transform.position.x, -45);
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + .1f);
        }
    }

    private void LandSwap()
    {
        if (Camera.main.transform.position.x > (offsetX / 4 * 3) * count)
        {
            if (isSwitch == false)
            {
                isSwitch = true;
                part1.transform.position = new Vector3(part1.transform.position.x + offsetX, part1.transform.position.y);
            }
            else
            {
                isSwitch = false;
                part2.transform.position = new Vector3(part2.transform.position.x + offsetX, part2.transform.position.y);
            }
            count++;
        }
    }
}
