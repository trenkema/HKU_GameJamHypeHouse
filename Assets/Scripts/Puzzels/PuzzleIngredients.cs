using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleIngredients : MonoBehaviour
{
    private PickUp player;

    public List<GameObject> goodIngredients = new List<GameObject>();
    public List<GameObject> badIngredients = new List<GameObject>();

    public ParticleSystem goodParticles;
    public ParticleSystem badParticles;

    public AudioSource feedbackAudio;
    public AudioClip badSound;
    public AudioClip goodSound;

    public SceneTransitions NextSceneTransition;
    public SceneTransitions RestartSceneTransition;

    public Light[] lightSources;

    public int badIngredientsLimit = 2;
    public int currentBadIngredients = 0;

    public int goodIngredientsLimit = 2;
    public int currentGoodIngredients = 0;

    public GameObject foodPlate;
    public Collider potCollider;
    public GameObject key;

    public GameObject Talk;
    public GameObject TalkWithoutIngredients;

    private GameObject cam;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    public DialogueManager dialogueManager;
    public GameObject firstDialogue;
    public GameObject secondDialogue;
    public bool enableKey = false;
    public bool goalDone = false;

    void Start()
    {
        key.SetActive(false);
        firstDialogue.SetActive(true);
        secondDialogue.SetActive(false);
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        currentBadIngredients = 0;
        currentGoodIngredients = 0;
        potCollider.enabled = false;
        foodPlate.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUp>();
        enableKey = false;
        goalDone = false;
    }

    void Update()
    {
        ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        RevealPlate();
        GameOver();

        if (secondDialogue.activeSelf == true)
        {
            if (dialogueManager.talking == true)
                dialogueManager.talkingDone = true;
            if (dialogueManager.talking == false && dialogueManager.talkingDone == true && !enableKey)
            {
                key.SetActive(true);
                enableKey = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!player.isHolding)
        {
            for (int i = 0; i < goodIngredients.Count; i++)
            {
                if (collision.gameObject == goodIngredients[i])
                {
                    goodParticles.Play();
                    feedbackAudio.clip = goodSound;
                    feedbackAudio.Play();
                    player.UnRegisterOutlineObject(collision.gameObject);
                    Destroy(goodIngredients[i]);
                    goodIngredients.Remove(goodIngredients[i]);
                    currentGoodIngredients++;
                }
            }

            for (int i = 0; i < badIngredients.Count; i++)
            {
                if (collision.gameObject == badIngredients[i])
                {
                    badParticles.Play();
                    feedbackAudio.clip = badSound;
                    feedbackAudio.Play();
                    player.UnRegisterOutlineObject(collision.gameObject);
                    Destroy(badIngredients[i]);
                    badIngredients.Remove(badIngredients[i]);

                    foreach (Light light in lightSources)
                    {
                        light.intensity = light.intensity - 0.4f;
                    }

                    currentBadIngredients++;
                }
            }
        }
    }

    public void GameOver()
    {
        if (currentBadIngredients >= badIngredientsLimit)
        {
            // Game Over, Restart Scene?
            RestartSceneTransition.endScene = true;
        }
    }

    public void RevealPlate()
    {
        if (currentGoodIngredients >= goodIngredientsLimit && goalDone == false)
        {
            // Reveal Key
            goalDone = true;
            potCollider.enabled = true;
            foodPlate.SetActive(true);
            firstDialogue.SetActive(false);
            secondDialogue.SetActive(true);
        }
    }
}
