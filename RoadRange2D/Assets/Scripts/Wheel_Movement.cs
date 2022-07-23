using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wheel_Movement : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField] private WheelJoint2D frontWheel;
    [SerializeField] private WheelJoint2D backWheel;

    [SerializeField] private GameObject frontWheelObject;
    [SerializeField] private GameObject backWheelObject;

    [Header("Vehicle Stats")]
    private float speed;
    private float motorTorque = 10000;
    private float acceleration;
    private float suspensionFreq;
    private int rotationSpeed;

    private int maxFuel;
    private float currentFuel;

    private float constSpeed;
    private float accelerationMultiply = 1;

    private bool isOnce = false;
    private bool isCarBodyGrounded = false;

    [SerializeField] private UnityEvent UpdateUI;
    [SerializeField] private UnityEvent ExpEvent;
    [SerializeField] private UnityEvent AirCoinUI;
    [SerializeField] private UnityEvent AirBonusReset;

    [SerializeField] private UnityEvent FuelBarEvent;
    [SerializeField] private UnityEvent FuelTextEvent;
    [SerializeField] private UnityEvent OutOfFuelEvent;

    [SerializeField] private UnityEvent FrontflipEvent;
    [SerializeField] private UnityEvent BackflipEvent;
    
    private float lastPosExpX;

    void Start()
    {
        speed = PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "speed");
        acceleration = PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "speed");
        suspensionFreq = PlayerPrefs.GetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "suspension");
        rotationSpeed = PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "rotation");
        maxFuel = PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "fuel");

        JointSuspension2D suspension = new JointSuspension2D { dampingRatio = frontWheel.suspension.dampingRatio, frequency = suspensionFreq, angle = frontWheel.suspension.angle };

        frontWheel.suspension = suspension;
        backWheel.suspension = suspension;

        constSpeed = speed;
        speed = 0;

        currentFuel = maxFuel;

        PlayerPrefs.SetInt("maxFuel", maxFuel);
        PlayerPrefs.SetFloat("currentFuel", currentFuel);
        PlayerPrefs.SetInt("distanceFuel", 1000);

        lastPosExpX = transform.position.x;
    }

    private void Update()
    {
        Acceleration();
        FrontFlip();
        BackFlip();
        Exp();

        if (FuelDistance() < 100)
        {
            PlayerPrefs.SetInt("distanceFuel", FuelDistance());
            FuelTextEvent.Invoke();
        }
        else
        {
            PlayerPrefs.SetInt("distanceFuel", 1000);
            FuelTextEvent.Invoke();
        }

        if (!isFuelStart && currentFuel != 0)
            StartCoroutine("Fuel");

        if (!isOutOfFuel && currentFuel == 0)
            StartCoroutine("OutOfFuel");

        if (Air() && isOnce)
        {
            InvokeRepeating(nameof(AddAirCoin), 1, 0.5f);
            isOnce = false;
        }
        if (Air() == false && isOnce == false)
        {
            CancelInvoke(nameof(AddAirCoin));
            isOnce = true;
        }

        if (Input.GetKey(KeyCode.W) && currentFuel != 0)
        {
            frontWheel.useMotor = true;
            backWheel.useMotor = true;

            MoveFront();
            gameObject.GetComponent<Rigidbody2D>().AddTorque(rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && currentFuel != 0)
        {
            frontWheel.useMotor = true;
            backWheel.useMotor = true;

            MoveBack();
            gameObject.GetComponent<Rigidbody2D>().AddTorque(-rotationSpeed * Time.deltaTime);
        }
        else
        {
            frontWheel.useMotor = false;
            backWheel.useMotor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            AddCoin(1);
        }
        if (collision.gameObject.tag == "Fuel")
        {
            Destroy(collision.gameObject);
            AddFuel(maxFuel);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isCarBodyGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isCarBodyGrounded = false;
        }
    }

    // Exp.
    private void Exp()
    {
        if (Mathf.Abs(transform.position.x - lastPosExpX) > 10)
        {
            lastPosExpX = transform.position.x;
            PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + 1);
            ExpEvent.Invoke();
        }
    }

    // Fuel.
    bool isOutOfFuel = false;
    bool isFuelStart = false;
    private IEnumerator OutOfFuel()
    {
        isOutOfFuel = true;
        yield return new WaitForSeconds(5);
        if (currentFuel == 0)
            OutOfFuelEvent.Invoke();
        else
            isOutOfFuel = false;
    }
    private IEnumerator Fuel()
    {
        isFuelStart = true;
        while (currentFuel > 0)
        {
            PlayerPrefs.SetFloat("currentFuel", currentFuel);
            FuelBarEvent.Invoke();
            yield return new WaitForSeconds(0.05f);
            currentFuel = currentFuel -0.1f;
        }
        if (currentFuel < 0)
        {
            currentFuel = 0;
            PlayerPrefs.SetFloat("currentFuel", currentFuel);
            isFuelStart = false;
        }
    }
    private void AddFuel(int value)
    {
        if (currentFuel + value > maxFuel)
            currentFuel = maxFuel;
        else
            currentFuel = currentFuel + value;
    }

    private int FuelDistance()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Fuel");
        GameObject target = null;
        bool isOnceLocal = false;

        foreach(var t in targets)
        {
            if (t.transform.position.x > transform.position.x && !isOnceLocal)
            {
                isOnceLocal = true;
                target = t;
            }
        }

        int distance;

        if (target != null)
            distance = Mathf.Abs((int)transform.position.x - (int)target.transform.position.x);
        else
            distance = 1000;

        return distance;
    }

    // Flips.
    float f_lastRotation = 90;
    bool f_isLastRotation = true;
    float f_flipCount = 1;

    private void FrontFlip()
    {
        if (f_isLastRotation == false)
        {
            f_isLastRotation = true;
            f_lastRotation = GetComponent<Rigidbody2D>().rotation;
            b_lastRotation = GetComponent<Rigidbody2D>().rotation - 180;
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().rotation - f_lastRotation) > 360 && f_isLastRotation == true && (f_lastRotation > GetComponent<Rigidbody2D>().rotation))
        {
            f_isLastRotation = false;

            FrontflipEvent.Invoke();
            AddCoin((int)(5 * f_flipCount));

            b_flipCount = 1;
            f_flipCount += .5f;
        }
    }

    float b_lastRotation = -90;
    bool b_isLastRotation = true;
    float b_flipCount = 1;
    private void BackFlip()
    {
        if (b_isLastRotation == false)
        {
            b_isLastRotation = true;
            b_lastRotation = GetComponent<Rigidbody2D>().rotation;
            f_lastRotation = GetComponent<Rigidbody2D>().rotation + 180;
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().rotation - b_lastRotation) > 360 && b_isLastRotation == true && (b_lastRotation < GetComponent<Rigidbody2D>().rotation))
        {
            b_isLastRotation = false;

            BackflipEvent.Invoke();
            AddCoin((int)(5 * b_flipCount));

            f_flipCount = 1;
            b_flipCount += .5f;
        }
    }

    private bool Air()
    {
        if ((Physics2D.OverlapCircle(new Vector2(frontWheelObject.transform.position.x, frontWheelObject.transform.position.y),
            frontWheelObject.GetComponent<CircleCollider2D>().radius, LayerMask.GetMask("Ground")) &&
            Physics2D.OverlapCircle(new Vector2(backWheelObject.transform.position.x, backWheelObject.transform.position.y),
            backWheelObject.GetComponent<CircleCollider2D>().radius, LayerMask.GetMask("Ground"))) == false && isCarBodyGrounded == false)
        {
            return true;
        }
        else
        {
            AirBonusReset.Invoke();

            b_flipCount = 1;
            f_flipCount = 1;
            airBonusCoin = 1;

            return false;
        }
    }

    private void AddCoin(int value)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + value);
        UpdateUI.Invoke();
    }

    float airBonusCoin = 1;
    private void AddAirCoin()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1 * (int)airBonusCoin);
        AirCoinUI.Invoke();
        airBonusCoin += .5f;
    }

    // Movement.
    private void Acceleration()
    {
        if (Mathf.Abs(speed) < constSpeed)
            accelerationMultiply = 1;

        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -1 && gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            if(accelerationMultiply < constSpeed / 2)
                accelerationMultiply += constSpeed / 10 * Time.deltaTime;
        }
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 1 && gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            if (accelerationMultiply > -(constSpeed / 3))
                accelerationMultiply -= constSpeed / 10 * Time.deltaTime;
        }
    }

    private void MoveBack()
    {
        if (speed < 0)
            speed = 0;

        speed += acceleration * 3 * Time.deltaTime;
        speed = Mathf.Clamp(speed, -constSpeed, constSpeed/1.5f);

        Motor();
    }

    private void MoveFront()
    {
        if (speed > 0)
            speed = 0;

        speed -= acceleration * 3 * Time.deltaTime;
        speed = Mathf.Clamp(speed, -constSpeed - accelerationMultiply, constSpeed);

        Motor();
    }

    private void Motor()
    {
        JointMotor2D motor = new JointMotor2D { motorSpeed = speed, maxMotorTorque = motorTorque };
        frontWheel.motor = motor;
        backWheel.motor = motor;
    }
}
