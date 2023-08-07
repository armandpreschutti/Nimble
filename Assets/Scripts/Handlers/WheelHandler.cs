using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

//[ExecuteInEditMode]
public class WheelHandler : MonoBehaviour
{
    public float startValue = 0f;
    public float endValue = 10f;
    public float interpolationFactor;
    public bool isMoving;
    public float interpolatedValue;
    public float speed;
    public float currentValue;
    void Update()
    {
        // Clamp the currentValue between minValue and maxValue
        
        if (Input.GetButton("Fire1") && interpolationFactor < 1)
        {

            interpolationFactor += speed * Time.deltaTime;
            // Perform linear interpolation using Mathf.Lerp
            interpolatedValue = Mathf.Lerp(startValue, endValue, interpolationFactor);
        }
        else
        {
            return;
        }
        /* else
         {
             interpolationFactor -= speed * Time.deltaTime;
         }*/
        if (Input.GetButton("Fire2"))
        {

            currentValue -= speed * Time.deltaTime;
            // Perform linear interpolation using Mathf.Lerp
            interpolatedValue = Mathf.Lerp(startValue, endValue, interpolationFactor);
        }
    }
}
