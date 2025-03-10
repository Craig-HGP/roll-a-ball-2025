using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // Score of coins
    private int count = 0;
    public GameObject bG;
    public TextMeshProUGUI storyCopy;
    public Button button1, button2;
    public GameObject winTextObject;
    private PlayerController playerController;
    public GameObject door1, door2;
    // Text variable
    public TextMeshProUGUI countText;
    public GameObject player;
    public Boolean isFinalRoom = false; // Is this the final room?

    
    void Start()
    {
        count = 0;
        SetCountText();

        // Make sure the UI elements are hidden at the start
        winTextObject.SetActive(false);
        storyCopy.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        bG.SetActive(false);

        // Add listeners to buttons
        button1.onClick.AddListener(Button1Action);
        button2.onClick.AddListener(Button2Action);

    }

    public void AddCount(int amount)
    {
        count += amount;
        SetCountText();
    }

    void SetCountText() 
    {
        countText.text = "Count: " + count.ToString();
        
        if (count >= 12)
        {
            if (isFinalRoom)
            {
                HandleGameWin(); // Player has reached the final goal!
            }
            else
            {
                DisablePlayerMovement();
                ShowRoomChoiceUI();
            }
        }

    }

    public void Button1Action()
    {
        door1.SetActive(false); // Disable the first door
        count = 0;  // Reset pickup count
        SetCountText(); // Update UI
        EnablePlayerMovement();
    }

    public void Button2Action()
    {
        door2.SetActive(false); // Disable the second door
        count = 0;  // Reset pickup count
        SetCountText(); // Update UI
        EnablePlayerMovement();
    }

    void CloseUI()
    {
        storyCopy.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        if (playerController != null)
        {
            playerController.enabled = true; // Re-enable player movement
        }
    }

    public void SetupNextRoom(GameObject door1, string button1Text, GameObject door2, string button2Text)
    {
        // Disable player movement
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Show UI elements
        storyCopy.gameObject.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

        // Change button text
        button1.GetComponentInChildren<TextMeshProUGUI>().text = button1Text;
        button2.GetComponentInChildren<TextMeshProUGUI>().text = button2Text;

        // Assign button actions to remove doors
        button1.onClick.RemoveAllListeners(); // Clear previous listeners
        button1.onClick.AddListener(() => RemoveDoorAndResume(door1));

        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(() => RemoveDoorAndResume(door2));
    }

    // Helper function to remove door and resume player movement
    void RemoveDoorAndResume(GameObject door)
    {
        if (door != null)
        {
            door.SetActive(false);
        }

        // Hide UI and resume player movement
        storyCopy.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

    public void HandleGameOver()
    {
        winTextObject.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, 1f);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        winTextObject.SetActive(true);
    }

    void HandleGameWin()
    {
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().color = new Color(0f, 1f, 0f, 1f);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";
        DisablePlayerMovement();
    }

    void DisablePlayerMovement()
    {
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    void ShowRoomChoiceUI()
    {
        storyCopy.gameObject.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
    }

    void EnablePlayerMovement()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

}