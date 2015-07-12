using UnityEngine;
using System.Collections;

public class GameAnalytics : MonoBehaviour
{

    public GoogleAnalyticsV3 googleAnalytics;
    public bool isAnalyticsEnabled = true;

    public enum gaEventCategories
    {
        GameEvent,
        UIEvent
    }

    void Awake()
    {
        EnableAnalytics(true);
    }

    // Use this for initialization
    void Start()
    {
        if (isAnalyticsEnabled && googleAnalytics != null)
        {
            googleAnalytics.DispatchHits();
            googleAnalytics.StartSession();
        }
        else
        {
            googleAnalytics = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsAnalyticsEnabled()
    {
        return isAnalyticsEnabled;
    }

    public void EnableAnalytics(bool enable)
    {
        isAnalyticsEnabled = enable;
    }

    public void LogScreen(string screenName)
    {
        if (isAnalyticsEnabled && googleAnalytics != null)
        {
            googleAnalytics.LogScreen(screenName);
        }
    }

    public void LogEvent(gaEventCategories eventType, string actionTaken, string label, long eventValue)
    {
        if (isAnalyticsEnabled == true && googleAnalytics != null)
        {
            googleAnalytics.LogEvent(new EventHitBuilder()
                .SetEventCategory(eventType.ToString())
                .SetEventAction(actionTaken)
                .SetEventLabel(label)
                .SetEventValue(eventValue));
        }
    }

    public void LogEvent(gaEventCategories eventType, string actionTaken, string label)
    {
        if (isAnalyticsEnabled && googleAnalytics != null)
        {
            LogEvent(eventType, actionTaken, label, 0);
        }
    }
}
