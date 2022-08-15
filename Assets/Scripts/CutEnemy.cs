using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPieces;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            for (int i = 0; i < enemyPieces.Length; i++)
            {
                Instantiate(enemyPieces[i], other.transform.position, other.transform.rotation);
                enemyPieces[i].GetComponent<Rigidbody>().AddForce(Vector3.up * 10);
            }
            Destroy(other.gameObject);
        }
    }
}
