using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmblemRotator : MonoBehaviour
{
    public float rotateSpeed = .5f;
    public float rotateDistance = 30.0f;
    private Vector3 objectRotation = Vector3.zero;
    private Vector3 startRotation = Vector3.zero;
    [SerializeField] private bool mirrorRotation = false;
    void Start()
    {
        startRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        RotateObject();
    }


    private void RotateObject()
    {
        if (mirrorRotation)
            objectRotation.z = Mathf.Sin(-Time.time * rotateSpeed) * rotateDistance;
        else
            objectRotation.z = Mathf.Sin(Time.time * rotateSpeed) * rotateDistance;
        transform.rotation = Quaternion.Euler(startRotation + objectRotation);
    }
}
