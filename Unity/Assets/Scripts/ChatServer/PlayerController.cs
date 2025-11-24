using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float roateSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * Vertical;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * horizontal * roateSpeed * Time.deltaTime);
    }
}
