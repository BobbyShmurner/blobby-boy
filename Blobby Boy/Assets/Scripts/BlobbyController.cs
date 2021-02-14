using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobbyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float mouseDistanceMultiplierMax = 3f;

    void Update() {
        Move();
    }

    void Move() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (new Vector3(mousePos.x, mousePos.y, 0) - transform.position);

        float mouseDistanceMultiplier = Mathf.Clamp(dir.magnitude, 1f, mouseDistanceMultiplierMax);
        print(mouseDistanceMultiplier);

        transform.position += dir.normalized * moveSpeed * mouseDistanceMultiplier * Time.deltaTime;
    }
}
