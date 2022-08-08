using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GrabItem : MonoBehaviour
{

    private GameObject grabbedObject;
    private float grabRange = 3f;
    private bool isGrabbing;
    [SerializeField] private Animator animator;
    private bool canGrab = false;
    [SerializeField] private Rig leftHandRig;
    [SerializeField] private Rig rightHandRig;
    [SerializeField] private Transform leftTarget;
    [SerializeField] private Transform rightTarget;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, grabRange);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Grabbable"))
            {
                grabbedObject = collider.gameObject;
                canGrab = true;
            }
        }

        if (canGrab && grabbedObject)
        {
            //float topLeft = grabbedObject.GetComponent<Renderer>().bounds.max.y;
            rightTarget.position = grabbedObject.GetComponent<Renderer>().bounds.min;
            leftTarget.position = grabbedObject.GetComponent<Renderer>().bounds.max;
            ChangeRigWeight(1);

            grabbedObject.transform.SetParent(transform);
            isGrabbing = true;
        }

        if (!grabbedObject)
        {
            isGrabbing = false;
        }

        if (isGrabbing && Input.GetKeyDown(KeyCode.E))
        {
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            ChangeRigWeight(0);
        }
    }

    private void ChangeRigWeight(float weight)
    {
        if (weight >= 0 && weight <= 1)
        {
            leftHandRig.weight = weight;
            rightHandRig.weight = weight;
        }
    }
}
