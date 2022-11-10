using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ForecastManager : MonoBehaviour
{
    [Header("Loaded Scene")]
    [SerializeField]  private string loadScene;
    
    [Header("Forecast Fields")]
    [SerializeField] private GameObject forecastHolder;
    [SerializeField] private GameObject forecastPanel;
    [SerializeField] private Vector2Int firstDate;
    [SerializeField] private DaysOfWeek firstDay;
    [SerializeField] private Vector2Int secondDate;
    [SerializeField] private Sprite sun;
    [SerializeField] private Sprite rain;

    [Header("UI Display Fields")]
    [SerializeField] private GameObject weatherOverview;
    [SerializeField] private GameObject info;
    [SerializeField] private GameObject bg;
    [SerializeField] private Color rainyColor;
    [SerializeField] private Color sunnyColor;
    [SerializeField] private Image fadeToBlack;

    [Header("Animation Fields")]
    [SerializeField] private float transitionTime;
    
    private const float ForecastIncrements = 140;

    [Header("Generated Fields")]
    [SerializeField] private float slideLength;
    [SerializeField] private int firstDayIndex;
    [SerializeField] private int secondDayIndex;

    IEnumerator ForecastLoadingScreen()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(loadScene);
        asyncOperation.allowSceneActivation = false;
        int currPosX = 0;
        float rainShine = 0;

        var timeTxt = info.transform.Find("Time").GetComponent<TMP_Text>();
        var conditionTxt = info.transform.Find("Condition").GetComponent<TMP_Text>();
        var temperatureTxt = weatherOverview.transform.Find("Temperature").GetComponent<TMP_Text>();
        var precipTxt = weatherOverview.transform.Find("Precipitation").GetComponent<TMP_Text>();
        var humidityTxt = weatherOverview.transform.Find("Humidity").GetComponent<TMP_Text>();
        var windTxt = weatherOverview.transform.Find("Wind").GetComponent<TMP_Text>();
        var bgImage = bg.GetComponent<UnityEngine.UI.Image>();

        yield return null;
        
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / transitionTime;
            float v = EaseInOutElastic(0, 1, t);
            var pos = new Vector3(v * slideLength, -100);
            forecastHolder.transform.localPosition = pos;
            
            var posInt = Mathf.RoundToInt(-pos.x / ForecastIncrements);
            if (posInt != currPosX)
            {
                Debug.Log(posInt);
                currPosX = posInt;
                DaysOfWeek currDay = (DaysOfWeek)((currPosX + 7) % 7);
                rainShine += 0.1f;
                
                switch (currDay)
                {
                    case DaysOfWeek.Sunday:
                        timeTxt.text = "Sunday, 5:00 PM";
                        break;
                    case DaysOfWeek.Monday:
                        timeTxt.text = "Monday, 8:00 AM";
                        break;
                    case DaysOfWeek.Tuesday:
                        timeTxt.text = "Tuesday, 8:00 AM";
                        break;
                    case DaysOfWeek.Wednesday:
                        timeTxt.text = "Wednesday, 8:00 AM";
                        break;
                    case DaysOfWeek.Thursday:
                        timeTxt.text = "Thursday, 8:00 AM";
                        break;
                    case DaysOfWeek.Friday:
                        timeTxt.text = "Friday, 8:00 AM";
                        break;
                    case DaysOfWeek.Saturday:
                        timeTxt.text = "Saturday, 5:00 PM";
                        break;
                }

                temperatureTxt.text = Random.Range(50, 72).ToString();
                windTxt.text = "Wind: " + Random.Range(3, 10) + "mph";

                if (currPosX == firstDayIndex || currPosX == secondDayIndex)
                {
                    conditionTxt.text = "Rainy";
                    precipTxt.text = "Precipitation: 95%";
                    humidityTxt.text = "Humidity: 88%";
                    bgImage.color = rainyColor;
                    rainShine = 0;
                }
                else
                {
                    conditionTxt.text = "Sunny";
                    precipTxt.text = "Precipitation: " + Random.Range(0, 20) + "%";
                    humidityTxt.text = "Humidity: " + Random.Range(30, 60) + "%";
                    bgImage.color = Color.Lerp(rainyColor, sunnyColor, rainShine);
                }
            }
            
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        yield return null;

        if (asyncOperation.progress >= 0.99f)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    IEnumerator FadeOutFromBlack()
    {
        yield return null;
    }
    
    IEnumerator FadeIntoBlack()
    {
        yield return null;
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
        totalDays += 15;

        int firstDateTotalDays = firstDate.x * 30 + firstDate.y;

        foreach (Transform child in forecastHolder.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int i = 0; i < totalDays; i++)
        {
            int offsetIncrement = i - 7;
            int dateTotalDays = firstDateTotalDays + offsetIncrement;
            int month = dateTotalDays / 30;
            int day = dateTotalDays % 30;
            DaysOfWeek currDay = (DaysOfWeek)(((int)firstDay + offsetIncrement + 7) % 7);

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
    
    /**
     * https://gist.github.com/cjddmut/d789b9eb78216998e95c
     * @author by cjddmut
     */
    public static float EaseInOutElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d * 0.5f) == 2) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if (value < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
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
