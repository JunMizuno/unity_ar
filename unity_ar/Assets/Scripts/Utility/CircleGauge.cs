using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGauge : MonoBehaviour
{
    [SerializeField]
    public Image gauge;

    [SerializeField, Range(0.0f, 1.0f)]
    public float fillValue = 0.0f;

    public void Update()
    {
        if (gauge != null)
        {
            gauge.fillAmount = fillValue;
        }
    }
}
