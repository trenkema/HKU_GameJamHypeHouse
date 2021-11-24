using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerLook playerLook;
    public GameObject cam;

    public float rotationSpeed = 75f;

    Vector3 objectPos;
    private float distance;
    public float pickUpDistance = 2.5f;

    public bool isInspecting = false;

    public GameObject inspectDestination;
    public GameObject holdDestination;
    public bool isHolding = false;
    public bool isTalking = false;

    public GameObject pickUpText;
    public GameObject storedObject;

    public LayerMask IgnoreMe;
    public float rayDistance = 1.5f;
    RaycastHit hit;
    Ray ray;

    public List<GameObject> outlineObjects = new List<GameObject>();

    public GameObject holdingObject;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
    }

    void Update()
    {
        ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        distance = Vector3.Distance(transform.position, holdDestination.transform.position);

        MouseOver();

        if (distance >= pickUpDistance && isHolding == true)
        {
            isHolding = false;
            ReleaseObject();
        }

        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.tag == "PickUp")
                {
                    if (isHolding == false)
                    {
                        isHolding = true;
                        holdingObject = hit.transform.gameObject;
                    }
                }
            }
        }

        if (isHolding == true)
        {
            holdingObject.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            holdingObject.transform.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            holdingObject.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
            if (isInspecting == false)
            {
                holdingObject.transform.SetParent(holdDestination.transform);
                holdingObject.transform.position = holdDestination.transform.position;
                holdingObject.transform.rotation = new Quaternion(-90, -90, -90, 0);
                holdingObject.transform.rotation = cam.transform.rotation;
            }
            // Inspect object when pressing F
            if (Input.GetKeyDown(KeyCode.F) && isInspecting == false && !isTalking)
            {
                holdingObject.layer = LayerMask.NameToLayer("Food");
                Physics.IgnoreCollision(GetComponent<Collider>(), holdingObject.GetComponent<Collider>(), true);
                Inspecting();
            }
            // Exit Inspect when pressing F again
            else if (Input.GetKeyDown(KeyCode.F) && isInspecting == true && !isTalking)
            {
                holdingObject.layer = LayerMask.NameToLayer("Default");
                holdingObject.GetComponent<BoxCollider>().isTrigger = true;
                Physics.IgnoreCollision(GetComponent<Collider>(), holdingObject.GetComponent<Collider>(), false);
                // Re-enable Movement and Look
                playerMovement.enabled = true;
                playerLook.enabled = true;
                // Disable Inspecting Mode
                isInspecting = false;
            }
            // Drop object when pressing right mouse button
            if (Input.GetMouseButtonDown(1) && isInspecting == false && isHolding == true)
            {
                // Disable Holding Mode
                isHolding = false;
                // Transfer object out of Hold Position and re-enable phyics
                ReleaseObject();
            }
        }
    }

    private void MouseOver()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, ~IgnoreMe))
        {
            Transform objectHit = hit.transform;
            if (hit.transform.tag == "PickUp")
            {
                if (isHolding == false)
                {
                    storedObject = objectHit.gameObject;
                    pickUpText.SetActive(true);
                    storedObject.GetComponent<Outline>().OutlineWidth = 4;
                }
            }
            else
            {
                if (storedObject != null)
                    storedObject.GetComponent<Outline>().OutlineWidth = 0;
                storedObject = null;
                pickUpText.SetActive(false);
            }
        }
        else
        {
            storedObject = null;
            pickUpText.SetActive(false);

            foreach (GameObject outlineObject in outlineObjects)
            {
                if (outlineObject.GetComponent<Outline>() != null)
                    outlineObject.GetComponent<Outline>().OutlineWidth = 0;
            }

            if (storedObject != null)
                storedObject.GetComponent<Outline>().OutlineWidth = 0;
        }
    }

    private void ReleaseObject()
    {
        objectPos = holdingObject.transform.position;
        holdingObject.transform.SetParent(null);
        holdingObject.transform.position = objectPos;
        storedObject = null;
        holdingObject.GetComponent<Rigidbody>().useGravity = true;
        holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if (holdingObject.GetComponent<Collider>() != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), holdingObject.GetComponent<Collider>(), false);
        }
        holdingObject = null;
    }

    private void Inspecting()
    {
        // Inspect item
        isInspecting = true;
        // Freeze Movement and Look
        holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        playerMovement.enabled = false;
        playerLook.enabled = false;
        // Transform object into Inspect Position
        holdingObject.transform.SetParent(inspectDestination.transform);
        holdingObject.transform.position = inspectDestination.transform.position;
        holdingObject.GetComponent<BoxCollider>().isTrigger = false;

        RotateObject rotateObject;
        rotateObject = holdingObject.GetComponent<RotateObject>();
        rotateObject.OnMouseDrag();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp" && isHolding == true)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
        }
    }

    public void UnRegisterOutlineObject(GameObject _object)
    {
        outlineObjects.Remove(_object);
    }
}
