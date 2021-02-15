using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] private GameObject clickFeedback;

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            UpdateMouseTargetPos(Input.mousePosition);
            //Instantiate(clickFeedback, BlobbyController.blobbyMouseTargetPos, transform.rotation);
        }
    }

    void UpdateMouseTargetPos(Vector2 mousePos) {
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        BlobbyController.blobbyMouseTargetPos = new Vector3(target.x, target.y, 0f);
    }
}
