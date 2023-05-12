using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    [SerializeField] private float secondPerRealTime=1000;

    private bool isNight=false;

    private float nightFogDensity=(float)0.1;
    private float dayFogDensity;

    private float fogDensityRate=(float)0.2;
    private float currentFogDensity;

    // Start is called before the first frame update
    void Start()
    {
        dayFogDensity=RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;

    if(currentTime > secondPerRealTime * 0.25 && currentTime < secondPerRealTime * 0.75)
    {
        isNight = false;

        transform.Rotate(Vector3.right * (Time.deltaTime * 100));
    }
    else
    {
        isNight = true;

        transform.Rotate(Vector3.right * (-Time.deltaTime * 100));
    }

    if(isNight)
    {
        currentFogDensity = Mathf.Lerp(currentFogDensity, nightFogDensity, Time.deltaTime * fogDensityRate);
    }
    else
    {
        currentFogDensity = Mathf.Lerp(currentFogDensity, dayFogDensity, Time.deltaTime * fogDensityRate);
    }

    RenderSettings.fogDensity = currentFogDensity;
    }
}
