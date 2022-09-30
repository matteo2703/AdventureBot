using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour, IDataManager
{
    [SerializeField] private Light sun;
    private float secondsInFullDay = 300f; //5 minuti = 1 giorno

    [Range(0f, 1f)][SerializeField] private float currentTimeOfDay = 0f;
    private float sunInitialIntensity;

    public int hourOfDay;
    public int minutesOfHour;
    public int day;
    public int year;
    private int daysInYear = 100;

    private void Start()
    {
        sunInitialIntensity = sun.intensity;
    }
    private void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay);

        hourOfDay = (int) (currentTimeOfDay * 24);
        minutesOfHour = (int) ((currentTimeOfDay * 24 * 60) % 60);

        //new day
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            day++;
            if(day > daysInYear)
            {
                day = 1;
                year++;
            }
        }
    }
    private void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1f;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
            intensityMultiplier = 0;
        else if (currentTimeOfDay <= 0.25f)
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        else if (currentTimeOfDay >= 0.73f)
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
    public void LoadGame(GenericGameData data)
    {
        day = data.day;
        year = data.year;
        currentTimeOfDay = data.timeOfDay;
        sun.transform.localRotation = data.sunRotation;
    }

    public void SaveGame(GenericGameData data)
    {
        data.day = day;
        data.year = year;
        data.timeOfDay = currentTimeOfDay;
        data.sunRotation = sun.transform.localRotation;
    }
}
