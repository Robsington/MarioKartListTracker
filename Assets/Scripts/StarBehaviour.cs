using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarBehaviour : MonoBehaviour
{
    private Vector3 startScale = Vector3.zero;
    private Vector3 firstScale = new Vector3(1.5f, 1.5f, 1);
    private Vector3 endScale = Vector3.one;

    public float rotateSpeed = 1.0f;
    public float rotateDistance = 25.0f;
    private Vector3 objectRotation = Vector3.zero;
    private Vector3 startRotation = Vector3.zero;
    [SerializeField] private Toggle starToggle = null;
    //private bool rotateObject = false;

    void Start()
    {
        startRotation = transform.rotation.eulerAngles;
        transform.localScale = startScale;
        starToggle.onValueChanged.AddListener(OnStarToggledOn);
    }

    private void OnSequenceComplete()
    {
        //rotateObject = true;
    }

    private void Update()
    {
        //if (rotateObject)
        RotateObject();
    }
    private void RotateObject()
    {
        objectRotation.z = Mathf.Sin(Time.time * rotateSpeed) * rotateDistance;
        transform.rotation = Quaternion.Euler(startRotation + objectRotation);
    }
    private void ScaleToFirstPosition()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(firstScale, 1).OnComplete(ScaleToLastPosition);
    }

    private void ScaleToLastPosition()
    {
        transform.DOScale(endScale, 1).OnComplete(OnSequenceComplete);
    }

    private void OnStarToggledOn(bool isOn)
    {
        ScaleToFirstPosition();
    }
}
