using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobbyController : MonoBehaviour
{
    public static List<Vector3> blobbyPositions = new List<Vector3>();

    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float mouseDistanceMultiplierMax = 3f;
    [SerializeField] private float socialDistance = 0.5f;
    [SerializeField] private float socialDistanceFactor = 1f;

    private int blobbyId;

    private float blobbyDistance;

    void Start() {
        blobbyPositions.Add(transform.position);
        blobbyId = blobbyPositions.Count - 1;
    }

    void Update() {
        blobbyDistance = ((transform.localScale.x / 16) + socialDistance) * socialDistanceFactor;

        Move();
        UpdatePositionList();
    }

    void Move() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (new Vector3(mousePos.x, mousePos.y, 0) - transform.position);

        float mouseDistanceMultiplier = Mathf.Clamp(dir.magnitude, 0f, mouseDistanceMultiplierMax);

        Vector3 targetPos = dir.normalized * moveSpeed * mouseDistanceMultiplier;

        for (int i = 0; i < blobbyPositions.Count; i++) {
            if (i == blobbyId) { continue; }

            Vector3 differance = blobbyPositions[i] - transform.position;
            if (differance.magnitude > blobbyDistance) { continue; }

            targetPos -= differance.normalized * (blobbyDistance - differance.magnitude);
        }

        transform.position += targetPos * Time.deltaTime;
    }

    void UpdatePositionList() {
        blobbyPositions[blobbyId] = transform.position;
    }
}
