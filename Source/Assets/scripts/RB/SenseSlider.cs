using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenseSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    public static float sensitivity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sensitivity = slider.value;
    }
}
