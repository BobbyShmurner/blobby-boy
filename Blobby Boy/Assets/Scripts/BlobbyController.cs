using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobbyController : MonoBehaviour
{
    public static List<Vector3> blobbyPositions = new List<Vector3>();
    public static Vector3 blobbyMouseTargetPos;

    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float mouseDistanceMultiplierMax = 3f;
    [SerializeField] private float socialDistance = 0.5f;
    [SerializeField] private float socialDistanceFactor = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float defect = 0.1f;

    private Animator animator;

    private int blobbyId;

    private float blobbyDistance;

    void Start() {
        animator = GetComponentInChildren<Animator>();

        blobbyPositions.Add(transform.position);
        blobbyId = blobbyPositions.Count - 1;

        //Setup Defects
        defect = Random.Range(-defect, defect);
        moveSpeed += defect;
        animator.SetFloat("hopSpeed", animator.GetFloat("hopSpeed") + defect);
    }

    void Update() {
        blobbyDistance = ((transform.localScale.x / 16) + socialDistance) * socialDistanceFactor;

        if (Input.GetMouseButtonDown(0)) {
            UpdateMouseTargetPos(Input.mousePosition);
        }

        Move();
        UpdatePositionList();
    }

    void Move() {
        Vector3 dir = (blobbyMouseTargetPos - transform.position);

        float mouseDistanceMultiplier = Mathf.Clamp(dir.magnitude, 0f, mouseDistanceMultiplierMax);

        Vector3 targetPos = dir.normalized * moveSpeed * mouseDistanceMultiplier;

        targetPos = DistanceGroupCheck(targetPos);

        transform.position += targetPos * Time.deltaTime;
    }

    Vector3 DistanceGroupCheck(Vector3 targetPos) {
        /*foreach (Vector3 blobbyPos in blobbyPositions) {
            if (blobbyPos == blobbyPositions[blobbyId]) { continue; }

            Vector3 differance = blobbyPos - blobbyPositions[blobbyId];
            float magnitude = differance.magnitude;
            if (magnitude > blobbyDistance) { continue; }

            targetPos -= differance.normalized * (blobbyDistance - magnitude);
        }*/

        for (int i = 0; i < blobbyPositions.Count; i++) {
            if (blobbyPositions[i] == blobbyPositions[blobbyId]) { continue; }

            Vector3 differance = blobbyPositions[i] - blobbyPositions[blobbyId];
            float magnitude = differance.magnitude;
            if (magnitude > blobbyDistance) { continue; }

            targetPos -= differance.normalized * (blobbyDistance - magnitude);
        }

        return targetPos;
    }

    void UpdateMouseTargetPos(Vector2 mousePos) {
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        if (blobbyMouseTargetPos == target) { return; }

        blobbyMouseTargetPos = new Vector3(target.x, target.y, 0f);
    }

    void UpdatePositionList() {
        blobbyPositions[blobbyId] = transform.position;
    }
}
