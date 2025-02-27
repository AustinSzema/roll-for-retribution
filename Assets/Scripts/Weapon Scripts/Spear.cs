using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    private bool shouldRotate = true;
    // Update is called once per frame
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        if (shouldRotate)
        {
            // Get the rotation based on the camera's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(mainCam.transform.forward);

            // Apply the rotation to the spear and its Rigidbody
            transform.rotation = targetRotation;
            rb.rotation = targetRotation;

            //Debug.Log("ROTATION CAMERA " + targetRotation);
        }
    }
    
    public override void Slam()
    {
        rb.linearVelocity = Vector3.zero;
        // Use Quaternion.Euler to specify the rotation in degrees
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.rotation = rotation;
        rb.rotation = rotation;
        rb.AddForce(slamForce * Vector3.down);
    }


    public override void Shoot(Vector3 magnetForwardDirection)
    {
        rb.AddForce(shootForce * magnetForwardDirection);
    }
    
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(TagManager.groundTag) || other.gameObject.CompareTag(TagManager.cageTag))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            shouldRotate = false;
        }
        HitEnemy(other.gameObject);
    }
    
    
    public override void Attract(Vector3 magnetPosition)
    {
        shouldRotate = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.linearVelocity = Vector3.zero;
        rb.position = Vector3.MoveTowards(rb.position, magnetPosition,
            Time.deltaTime * pullSpeed);
    }
}