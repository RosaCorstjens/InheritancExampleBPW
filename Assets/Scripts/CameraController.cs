using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float rotateSpeed = 6.0f;

    private Vector3 moveDirection = Vector3.zero;

    public void DoUpdate()
    {
        // handle clicks on objects
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // something was hit
                // if it was an animal, handle clicked
                hit.transform.gameObject.GetComponent<IClickable>()?.Clicked(Input.GetMouseButtonDown(0), Input.GetMouseButtonDown(1));
            }
        }

        // get move input
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        
        // apply movement
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // apply rotation
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
    }


}
