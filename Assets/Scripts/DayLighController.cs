using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLighController : MonoBehaviour
{
    public float dayLengthInSeconds = 120.0f; // тривал≥сть дн€ у секундах
    public Light sunLight; // вказуЇмо джерело св≥тла —онц€
    public Color dayColor; // кол≥р фону вдень
    public Color nightColor; // кол≥р фону вноч≥

    private float timeOfDay; // поточний час доби в секундах
    private bool isDaytime; // чи зараз день?


    void Start()
    {
        timeOfDay = 0.0f; // починаЇмо з 0
        isDaytime = true; // починаЇмо вдень
    }


    void Update()
    {
        // додаЇмо час доби, в залежност≥ в≥д часу, що пройшов з останнього оновленн€
        timeOfDay += Time.deltaTime / dayLengthInSeconds;


        // €кщо час доби перевищуЇ 1, то повертаЇмос€ на початок доби
        if (timeOfDay > 1)
        {
            timeOfDay -= 1;
            isDaytime = !isDaytime; // зм≥нюЇмо день ≥ н≥ч
        }


        // зм≥нюЇмо кольори фону в≥дпов≥дно до часу доби
        if (isDaytime)
        {
            RenderSettings.skybox.SetColor("_Tint", dayColor);
        }
        else
        {
            RenderSettings.skybox.SetColor("_Tint", nightColor);
        }


        // зм≥нюЇмо напр€мок св≥тла в≥дпов≥дно до часу доби
        sunLight.transform.localRotation = Quaternion.Euler(new Vector3((timeOfDay * 360f) - 90f, 170f, 0f));
    }
}


