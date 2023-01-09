using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(val => SoundManager.SMInstance.ChangeVolume(val));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
