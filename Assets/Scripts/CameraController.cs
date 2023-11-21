using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float rotateSpeed = 6.0f;

    public void Update()
    {
        // handle clicks on animals
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // something was hit
                // if it was a clickable, handle clicked
                hit.transform.gameObject.GetComponent<IClickable>()?.Clicked(Input.GetMouseButtonDown(0), Input.GetMouseButtonDown(1));
            }
        }

        // apply movement
        transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime);

        // apply rotation
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
    }
}
