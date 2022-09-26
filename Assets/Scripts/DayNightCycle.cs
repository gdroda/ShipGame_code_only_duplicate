using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Gradient lightColor;
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject[] lanternLights;

    private int days;

    public int Days => days;

    private float time = 00f;

    private bool canChangeDay = true;

    public delegate void OnDayChanged();

    public OnDayChanged DayChanged;

    void Update()
    {
        if (time > 500f)
        {
            time = 0f;
        }

        if ((int)time == 250 && canChangeDay)
        {
            canChangeDay = false;
            DayChanged?.Invoke(); //delegate and event for if u need something to happen on day change
            days++;
        }

        if ((int) time == 255)
        {
            canChangeDay = true;
        }

        time += 10f*Time.deltaTime;
        lights.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = lightColor.Evaluate(time * 0.002f);

        if ((int)time > 160 && (int)time < 330)
        {
            foreach (GameObject light in lanternLights)
            {
                light.SetActive(true);
            }
        }else
        {
            foreach (GameObject light in lanternLights)
            {
                light.SetActive(false);
            }
        }
    }
}
