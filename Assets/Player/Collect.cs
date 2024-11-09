using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public int score;
    private Rigidbody2D rb;
    private Transform transform;
    public LayerMask itemLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }
    void Update() {
        Collider2D[] itemColliders = Physics2D.OverlapBoxAll(rb.position, transform.localScale*4, rb.rotation, itemLayer);
        if (Input.GetKeyDown(KeyCode.E) && itemColliders.Length > 0)
        {
            foreach(Collider2D itemCollider in itemColliders)
            {
                Destroy(itemCollider.gameObject);
            }
        }

    }
    }
