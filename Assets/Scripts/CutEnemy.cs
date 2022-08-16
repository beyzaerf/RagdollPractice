using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPieces;
    [SerializeField] private GameObject prefab;
    private bool activeEnemy = true;

    private void Update()
    {
        if (!activeEnemy)
        {
            Instantiate(prefab, new Vector3(Random.Range(55, 95), 0, Random.Range(-15, 5)), Quaternion.Euler(0, 0, 0));
            activeEnemy = true;
        }
    }

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
            activeEnemy = false;
        }
    }
}
