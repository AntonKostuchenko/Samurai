using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float speed = 40;
    [SerializeField] private GameObject colision;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D cd;

    private bool move = true;
    void Update()
    {   if(move == true) {
        rb.velocity = transform.forward * speed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Stop");
            move = false;
        }
    }
}
