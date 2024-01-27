using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class RotateBridge : MonoBehaviour
{
    [SerializeField] private float rotation;
    [SerializeField] private float speed;
    [SerializeField] private GameObject hinge;
    private bool dropBridge;
    void Start()
    {
        dropBridge = false;
    }
    // Update is called once per frame
    void Update()
    {
       // transform.Rotate(rotation * Time.deltaTime);
       if(dropBridge == true && rotation < 0)
        {
            hinge.transform.localRotation = Quaternion.Euler(0, 0, (rotation += speed * Time.deltaTime));
        }
    }

    private void OnTriggerStay()
    {
        if (rotation > -90)
        {
            hinge.transform.localRotation = Quaternion.Euler(0, 0, (rotation -= speed*Time.deltaTime));
        }
    }
    private void OnTriggerEnter()
    {
        dropBridge = false;
    }
    private void OnTriggerExit()
    {
        dropBridge = true;
    }
}
