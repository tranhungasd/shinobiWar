using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;
    private float currentFill;
    public float myMaxValue
    {
        get;
        set;
    }
    private float currentValue;
    public float myCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > myMaxValue)
            {
                currentValue = myMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
            currentFill = currentValue / myMaxValue;
        }
    }
    void Start()
    {
        
        content = GetComponent<Image>();
    }
    void Update()
    {
        content.fillAmount = currentFill;
    }
    public void Initialized(float currentValue, float maxValue)
    {
        myMaxValue = maxValue;
        myCurrentValue = currentValue;
        
    }
}
