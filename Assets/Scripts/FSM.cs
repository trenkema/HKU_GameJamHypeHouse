using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerLook playerLook;
    public PlayerMovement playerMovement;
    public PlayerHeadBob playerHeadBob;
    public PickUp pickUp;
    public GameObject crossHair;
    public bool inPauseMenu = false;
    public bool canOpenPauseMenu = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && !inPauseMenu && canOpenPauseMenu)
        {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf && inPauseMenu)
        {
            ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        inPauseMenu = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerLook.enabled = false;
        playerMovement.enabled = false;
        playerHeadBob.enabled = false;
        pickUp.enabled = false;
        crossHair.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        inPauseMenu = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerLook.enabled = true;
        playerMovement.enabled = true;
        playerHeadBob.enabled = true;
        pickUp.enabled = true;
        crossHair.SetActive(true);
    }

    public void InPauseMenu()
    {
        inPauseMenu = true;
    }

    public void NotInPauseMenu()
    {
        inPauseMenu = false;
    }

    public void CanOpenMenu()
    {
        canOpenPauseMenu = true;
    }
}
