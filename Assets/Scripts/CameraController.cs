using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Transform myTransform;

    public float speed = 6.0f;
    public float rotateSpeed = 6.0f;

    private Vector3 moveDirection = Vector3.zero;

    private GameObject clicked

    private void Start()
    {
        // teehee
        //myTransform = gameObject.transform;
    }

    private void Update()
    {
        // handle clicks on objects
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // something was hit
                // if it was an animal, handle clicked
                Clicked(hit.transform.gameObject);
            }
        }

        // get move input
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        
        // apply movement
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // apply rotation
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }

    private void Clicked(GameObject gameobject)
    {
        // did I forget to check something(s)? ... 

        // if there was a last clicked object
        // deselect it
        if (clicked != null) { }
            clicked.GetComponent<ISelectable>().Deselect();

        // remember the clicked objects
        clicked = gameobject;

        // click on the clicked object
        clicked.GetComponent<IClickable>().Clicked();

        // select the clicked object
        clicked.GetComponent<ISelectable>().Select();
    }
}
