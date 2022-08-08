using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndGrow : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform player;
    private bool activeItem = true;

    private void Start()
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, parent);
    }
    private void Update()
    {
        if (!activeItem)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            Instantiate(prefab, pos, Quaternion.identity, parent);
            activeItem = true;
        }
    }

    // When the character picks up an item (itself but smaller), it grows.
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("MiniHuman"))
        {
            player.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            Destroy(other.gameObject);
            activeItem = false;
        }
    }
}
