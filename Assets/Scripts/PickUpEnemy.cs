using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEnemy : MonoBehaviour
{
    [SerializeField] private Transform character;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("MiniHuman"))
        {
            character.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            Destroy(collision.gameObject);
        }
    }
}
