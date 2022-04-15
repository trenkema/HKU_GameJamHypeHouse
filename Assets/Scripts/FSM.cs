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

    bool isTalking = false;

    private void OnEnable()
    {
        EventSystemNew<bool>.Subscribe(Event_Type.TALKING, SetTalking);
    }

    private void OnDisable()
    {
        EventSystemNew<bool>.Unsubscribe(Event_Type.TALKING, SetTalking);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && !inPauseMenu && !isTalking)
        {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf && inPauseMenu && !isTalking)
        {
            ClosePauseMenu();
        }
    }

    public void SetTalking(bool _isTalking)
    {
        isTalking = _isTalking;
    }

    public void OpenPauseMenu()
    {
        EventSystemNew<bool>.RaiseEvent(Event_Type.PAUSED, true);

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

        EventSystemNew<bool>.RaiseEvent(Event_Type.PAUSED, false);
    }
}
