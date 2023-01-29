using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] Obi.ObiEmitter emitter;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = emitter.particleCount;
        slider.value = slider.maxValue - emitter.activeParticleCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Paintable)
        {
            slider.value = slider.maxValue - emitter.activeParticleCount;
            if (slider.value == 0)
            {
                time += Time.deltaTime;
                if (time >= 2.65f)
                {
                    time = 0;
                    slider.value = slider.maxValue;
                    GameManager.Instance.PaintIsOverOP();
                }
            }
        }
    }
}
