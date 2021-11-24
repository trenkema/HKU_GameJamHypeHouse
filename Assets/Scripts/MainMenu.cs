using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public PlayerLook playerLook;
    public PlayerMovement playerMovement;
    public PickUp playerPickup;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerLook.enabled = false;
        playerMovement.enabled = false;
        playerPickup.enabled = false;
    }

    public void StartGame()
    {
        playerLook.enabled = true;
        playerMovement.enabled = true;
        playerPickup.enabled = true;
    }
}
