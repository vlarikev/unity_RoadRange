using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Land_Generator : MonoBehaviour
{
    private SpriteShapeController shape;

    [Header("Variables")]
    [SerializeField] private int scale;
    [SerializeField] private float tangent;
    [SerializeField] private int pointsQuantity;

    [Header("First pattern")]
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [Header("Second pattern")]
    [SerializeField] private bool is2Include;
    [SerializeField] private float amplitude2;
    [SerializeField] private float frequency2;
    [Header("Third pattern")]
    [SerializeField] private bool is3Include;
    [SerializeField] private float amplitude3;
    [SerializeField] private float frequency3;

    private float posX;
    private float posY;

    private int seed1;
    private int seed2;
    private int seed3;

    void Start()
    {
        shape = GetComponent<SpriteShapeController>();
        seed1 = Random.Range(-100000, 100000);
        seed2 = Random.Range(-100000, 100000);
        seed3 = Random.Range(-100000, 100000);

        SpawnLand();
    }

    void SpawnLand()
    {
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * scale);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * scale);

        float distance = (float)scale / pointsQuantity;

        for (int i = 0; i < pointsQuantity; i++)
        {
            posX = shape.spline.GetPosition(i + 1).x + distance;

            if (is2Include == true && is3Include == false)
                posY = (Mathf.PerlinNoise(i / frequency, seed1) * amplitude + Mathf.PerlinNoise(i / frequency2, seed2) * amplitude2) / 2;

            else if (is3Include == true && is2Include == false)
                posY = (Mathf.PerlinNoise(i / frequency, seed1) * amplitude + Mathf.PerlinNoise(i / frequency3, seed3) * amplitude3) / 2;

            else if (is2Include == true && is3Include == true)
                posY = (Mathf.PerlinNoise(i / frequency, seed1) * amplitude + Mathf.PerlinNoise(i / frequency2, seed2) * amplitude2 * Mathf.PerlinNoise(i / frequency3, seed3) * amplitude3) / 3;

            else
                posY = Mathf.PerlinNoise(i / frequency, seed1) * amplitude;

            shape.spline.InsertPointAt(i + 2, new Vector3(posX, posY));
        }

        for (int i = 0; i < pointsQuantity; i++)
        {
            shape.spline.SetTangentMode(i + 2, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i + 2, new Vector3(-tangent, 0, 0));
            shape.spline.SetRightTangent(i + 2, new Vector3(tangent, 0, 0));
        }
    }
}
