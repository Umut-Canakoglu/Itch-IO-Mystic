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
    private void OnDrawGizmos()
    {
        float rotationAngle = rb.rotation;
        Gizmos.color = Color.red;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(rb.position, Quaternion.Euler(0, 0, rotationAngle), Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, transform.localScale*4);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
