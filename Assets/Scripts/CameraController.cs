using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Transform myTransform;
    public List<Animal> animals;

    public float speed = 6.0f;
    public float rotateSpeed = 6.0f;

    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        myTransform = gameObject.transform;
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

                // for profiling demonstration purposes:
                // spawn 100 cubes
                /*for (int i = 0; i < 100; i++)
                {
                    SpawnCube(hit.transform.position);
                }*/
            }
        }

        // get move input
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

        // apply movement
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // apply rotation
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }

    private void SpawnCube(Vector3 position)
    {
        // for profiling demonstration purposes:
        // spawns a cube with a random velocity applied
        Vector3 rndmVel = Random.insideUnitCircle.normalized * Random.Range(1, 10);
        float rndmSize = Random.Range(0.1f, 0.5f);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        cube.transform.localScale = new Vector3(rndmSize, rndmSize, rndmSize);
        cube.AddComponent<Rigidbody>().velocity = rndmVel;
    }
}
