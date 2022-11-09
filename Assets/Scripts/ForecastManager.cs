using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ForecastManager : MonoBehaviour
{
    [SerializeField] private GameObject forecastHolder;
    [SerializeField] private GameObject forecastPanel;
    [SerializeField] private Vector2Int firstDate;
    [SerializeField] private DaysOfWeek firstDay;
    [SerializeField] private Vector2Int secondDate;
    [SerializeField] private Sprite sun;
    [SerializeField] private Sprite rain;

    [SerializeField] private float transitionTime;
    
    private const float ForecastIncrements = 140;

    [SerializeField] private float slideLength;
    [SerializeField] private int firstDayIndex;
    [SerializeField] private int secondDayIndex;

    IEnumerator ForecastLoadingScreen()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ParkAndStreets");
        asyncOperation.allowSceneActivation = false;

        yield return null;
        
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / transitionTime;
            forecastHolder.transform.localPosition = new Vector3(t * slideLength, -100);
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        yield return null;

        if (asyncOperation.progress >= 0.99f)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    private void Start()
    {
        StartCoroutine(ForecastLoadingScreen());
    }

    [ContextMenu("GenerateDays")]
    public void GenerateDays()
    {
        int totalDays = (secondDate.x - firstDate.x) * 30 + (secondDate.y - firstDate.y);
        slideLength = -(ForecastIncrements * totalDays);
        totalDays += 7;

        int firstDateTotalDays = firstDate.x * 30 + firstDate.y;

        foreach (Transform child in forecastHolder.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int i = 0; i < totalDays; i++)
        {
            int offsetIncrement = i - 3;
            int dateTotalDays = firstDateTotalDays + offsetIncrement;
            int month = dateTotalDays / 30;
            int day = dateTotalDays % 30;
            DaysOfWeek currDay = (DaysOfWeek)(((int)firstDay + offsetIncrement) % 7);

            GameObject forecast = PrefabUtility.InstantiatePrefab(forecastPanel, forecastHolder.transform) as GameObject;
            var dateText =  forecast.transform.Find("Date").GetComponent<TMP_Text>();
            var dayText =  forecast.transform.Find("Day").GetComponent<TMP_Text>();
            var weatherSprite =  forecast.transform.Find("Image").GetComponent<Image>();

            forecast.name = offsetIncrement.ToString();

            dateText.text = month + "/" + day;

            switch (currDay)
            {
                case DaysOfWeek.Sunday:
                    dayText.text = "Sun";
                    break;
                case DaysOfWeek.Monday:
                    dayText.text = "Mon";
                    break;
                case DaysOfWeek.Tuesday:
                    dayText.text = "Tue";
                    break;
                case DaysOfWeek.Wednesday:
                    dayText.text = "Wed";
                    break;
                case DaysOfWeek.Thursday:
                    dayText.text = "Thu";
                    break;
                case DaysOfWeek.Friday:
                    dayText.text = "Fri";
                    break;
                case DaysOfWeek.Saturday:
                    dayText.text = "Sat";
                    break;
            }

            if ((month == firstDate.x && day == firstDate.y))
            {
                weatherSprite.sprite = rain;
                firstDayIndex = offsetIncrement;
            }
            else if (month == secondDate.x && day == secondDate.y)
            {
                weatherSprite.sprite = rain;
                secondDayIndex = offsetIncrement;
            } 
            else
            {
                weatherSprite.sprite = sun;
            }

            forecast.transform.localPosition = new Vector3(offsetIncrement * ForecastIncrements, 0, 0);
        }
    }

    private enum DaysOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }
}
