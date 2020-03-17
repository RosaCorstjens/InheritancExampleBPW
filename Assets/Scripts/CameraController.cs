using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform environment;

    public static Transform myTransform;
    public static List<Transform> destinations;

    public float speed = 6.0f;
    public float rotateSpeed = 6.0f;

    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        myTransform = gameObject.transform;

        destinations = new List<Transform>();
        for(int i = 0; i < environment.childCount; i++)
        {
            if (environment.GetChild(i).name.Contains("Flower"))
            {
                destinations.Add(environment.GetChild(i));
            }
        }
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
                hit.transform.gameObject.GetComponent<IClickable>()?.Clicked();
            }
        }

        // get move input
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        
        // apply movement
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // apply rotation
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }
}
