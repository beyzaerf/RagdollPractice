using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPieces;
    [SerializeField] private Transform character;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            for (int i = 0; i < enemyPieces.Length; i++)
            {
                Instantiate(enemyPieces[i], other.transform.position, other.transform.rotation);
                enemyPieces[i].GetComponent<Rigidbody>().AddForce(Vector3.up);
            }
            Destroy(other.gameObject);
        }
        // If i enable the trigger of these objects, they dont go flying because i have to make their rigidbodies stable.
        // So instead i did it in onCollision, need a better alternative. 
        //else if (other.CompareTag("MiniHuman")) 
        //{
        //    character.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        //    Destroy(other.gameObject);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("MiniHuman"))
        {
            character.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            Destroy(collision.gameObject);
        }
    }
}
