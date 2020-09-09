using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinpickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
