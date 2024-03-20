using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKYPRO_Sun_Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float startTime = 0.0f;

    void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        //transform.localEulerAngles.x + ((rotationSpeed / 10) * Time.deltaTime)
        transform.localEulerAngles = new Vector3((Time.time * rotationSpeed) + startTime, 20, 0);
    }
}
