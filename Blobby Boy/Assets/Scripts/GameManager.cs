using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blobbyCount;

    void Update()
    {
        blobbyCount.text = BlobbyController.blobbyPositions.Count.ToString();
    }
}
