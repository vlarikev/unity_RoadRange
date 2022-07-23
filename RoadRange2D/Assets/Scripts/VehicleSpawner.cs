using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    private void Start()
    {
        VehicleSpawn();
    }
    private void VehicleSpawn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == PlayerPrefs.GetInt("currentVehicleId"));
        }
    }
}
