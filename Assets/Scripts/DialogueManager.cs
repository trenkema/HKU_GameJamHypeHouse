using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshPro headText;

    public Animator anim;
    public Animator playerAnim;

    private Queue<string> sentences;

    public float typingSpeed;

    public PlayerMovement playerMovement;
    public PlayerLook playerLook;
    public PickUp pickUp;
    public bool talking;
    public bool talkingDone = false;

    public AudioSource talkAudio;

    void Start()
    {
        talkingDone = false;
        talking = false;
        sentences = new Queue<string>();
        headText.enabled = false;
    }

    private void Update()
    {
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        headText.transform.LookAt(camera.transform);
        headText.transform.rotation = Quaternion.LookRotation(camera.transform.forward);

        if (playerAnim.GetBool("isTalking") == true)
        {
            if (talkAudio.isPlaying == false)
            {
                playerAnim.SetBool("isTalking", false);
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (pickUp.isInspecting)
        {
            return;
        }

        pickUp.isTalking = true;
        talking = true;
        talkAudio.Play();
        playerAnim.SetBool("isTalking", true);

        playerLook.enabled = false;
        playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        anim.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        pickUp.isTalking = false;
        anim.SetBool("IsOpen", false);
        playerAnim.SetBool("isTalking", false);

        playerLook.enabled = true;
        playerMovement.enabled = true;

        talking = false;
        talkAudio.Stop();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
