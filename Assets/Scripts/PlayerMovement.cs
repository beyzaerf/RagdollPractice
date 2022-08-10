using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;
    public float turnSpeed, speed, lerpValue;

    private void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Movement();
        }
    }

    private void Movement() //Joystick-like movement
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.localPosition.z;

        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 hitVec = hit.point;
            hitVec.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, hitVec, lerpValue), Time.deltaTime * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hitVec - transform.position), turnSpeed * Time.deltaTime);
        }
    }
}
