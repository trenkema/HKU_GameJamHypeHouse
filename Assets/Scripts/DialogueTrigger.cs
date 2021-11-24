using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject NPC;
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    public GameObject talkText;
    public GameObject unableTalk;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    private PickUp player;

    public bool needItem = false;
    public GameObject neededItem;

    public float rotationSpeed = 0.5f;
    private float interp = 0;

    private bool turning = false;

    public FSM fsm;

    private void Start()
    {
        dialogueManager.headText.text = dialogue.name;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUp>();
    }

    private void Update()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 npcPos = gameObject.transform.position;
        Vector3 delta = new Vector3(playerPos.x - npcPos.x, 0.0f, playerPos.z - npcPos.z);

        Quaternion rotation = Quaternion.LookRotation(delta);

        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ray = cam.ScreenPointToRay(Input.mousePosition);

        MouseOver();

        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (Input.GetMouseButtonDown(0) && !fsm.inPauseMenu)
            {
                if (objectHit.gameObject == NPC)
                {
                    if (dialogueManager.talking == false && needItem == false)
                    {
                        dialogueManager.StartDialogue(dialogue);
                        dialogueManager.headText.enabled = false;
                        talkText.SetActive(false);
                        turning = true;
                    }
                    else if (dialogueManager.talking == false && needItem == true)
                    {
                        if (player.holdingObject == neededItem && player.isInspecting == false)
                        {
                            needItem = false;
                            Destroy(player.holdingObject);
                            player.isHolding = false;
                            dialogueManager.StartDialogue(dialogue);
                            dialogueManager.headText.enabled = false;
                            talkText.SetActive(false);
                            turning = true;
                        }
                    }
                }
            }
        }

        if (turning == true)
        {
            if (interp < 1)
            {
                interp += rotationSpeed * Time.deltaTime;
            }
            Vector3 dat = gameObject.transform.rotation.eulerAngles;
            Vector3 dat2 = rotation.eulerAngles;
            Vector3 result = Vector3.Lerp(dat, dat2, interp);
            gameObject.transform.rotation = Quaternion.Euler(result.x, result.y, result.z);
            if (gameObject.transform.rotation == rotation)
            {
                turning = false;
                interp = 0;
            } 
        }
    }

    private void MouseOver()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;

            if (objectHit.gameObject == NPC && dialogueManager.talking == false && needItem == false)
            {
                dialogueManager.headText.enabled = true;
                talkText.SetActive(true);
            }
            else if (objectHit.gameObject == NPC && dialogueManager.talking == false && needItem == true && player.holdingObject != neededItem)
            {
                dialogueManager.headText.enabled = true;
                talkText.SetActive(false);
                unableTalk.SetActive(true);
            }
            else if (objectHit.gameObject == NPC && dialogueManager.talking == false && needItem == true && player.holdingObject == neededItem)
            {
                dialogueManager.headText.enabled = true;
                talkText.SetActive(true);
                unableTalk.SetActive(false);
            }
        }
        else
        {
            dialogueManager.headText.enabled = false;
            talkText.SetActive(false);
            if (unableTalk != null)
                unableTalk.SetActive(false);
        }
    }
}
