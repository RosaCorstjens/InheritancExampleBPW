using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float rotateSpeed = 6.0f;

    public void Update()
    {
        // apply movement
        transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime);

        // apply rotation
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
    }
}
