using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D friction;
    public void PlayGame()
    {
        friction.friction = PlayerPrefs.GetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "friction");
        SceneManager.LoadScene("Stage" + PlayerPrefs.GetInt("currentStageId"));
    }
}
