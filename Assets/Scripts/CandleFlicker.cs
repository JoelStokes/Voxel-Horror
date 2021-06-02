using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    private Light lightsource;

    float minFlickerIntensity = 1.5f;
    float maxFlickerIntensity = 2.5f;
    float flickerSpeedMin = 0.03f;
    float flickerSpeedMax = 0.07f;

    float randomizer = 0;

    bool waiting = false;
    float time;

    private void Start()
    {
        lightsource = GetComponent<Light>();
    }

    private void Update()
    {
        {
            time += Time.deltaTime;

            if (!waiting)
            {
                lightsource.intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));

                randomizer = Random.Range(flickerSpeedMin, flickerSpeedMax);
                waiting = true;
            } else
            {
                if (time > randomizer)
                {
                    waiting = false;
                    time = 0;
                }
            }
        }
    }
}
