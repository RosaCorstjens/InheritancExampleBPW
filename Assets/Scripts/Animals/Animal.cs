using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour, IClickable, ISelectable
{
    protected new string name;
    protected int legs;

    protected Animator animator;
    protected new Rigidbody rigidbody;

    private float jumpForce;
    private float pushForce;

    [SerializeField] private Material stndrtMaterial;
    [SerializeField] private Material selectedMaterial;
    private List<MeshRenderer> meshRenderers;

    protected virtual void Start()
    {
        jumpForce = 200;
        pushForce = 50;

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        // hmm.. this should be ok. right?
        //meshRenderers = new List<MeshRenderer>();
        meshRenderers.AddRange(transform.GetComponentsInChildren<MeshRenderer>());
    }

    protected virtual void Update()
    {
        
    }

    public void Clicked()
    {
        ReactToClick();
    }

    protected virtual void ReactToClick()
    {
        // add a little push from the player
        Vector3 pushDirection = transform.position - CameraController.myTransform.position;
        rigidbody.AddForce(pushForce * pushDirection);
        
        // more specific behavior implemented in subclasses
    }

    protected void LookAtPlayer()
    {
        // look at player
        Vector3 lookAt = CameraController.myTransform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }

    protected abstract void MakeSound();

    protected virtual void Jump()
    {
        rigidbody.AddForce(new Vector3(0, jumpForce, 0));
    }

    public void Select()
    {
        meshRenderers.ForEach(m => m.material = selectedMaterial);
    }

    public void Deselect()
    {
        meshRenderers.ForEach(m => m.material = stndrtMaterial);
    }
}
