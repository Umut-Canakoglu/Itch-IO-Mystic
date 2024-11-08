using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class movement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpPower = 4f;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private Transform transform;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }
    void Update()
    {
        Rotation();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        FixedUpdate();
        Rotation();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal*speed, vertical*speed);
    }    
    private void Rotation()
    {
        Vector2 directPosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(directPosition);
        Vector2 lookDir = worldPosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
