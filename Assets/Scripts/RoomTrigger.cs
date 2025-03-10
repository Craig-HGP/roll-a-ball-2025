using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject door1;
    public string button1Text;
    public GameObject door2;
    public string button2Text;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true; // Prevent multiple calls
            GameManager gameManager = FindFirstObjectByType<GameManager>();

            if (gameManager != null)
            {
                gameManager.SetupNextRoom(door1, button1Text, door2, button2Text);
            }
        }
    }
}
