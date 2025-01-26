using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameCalendar : MonoBehaviour
{
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Autumn = 2,
        Winter = 3
    }
    public enum DayOfWeek
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }
    public enum TimeOfDay
    {
        Late = 0,    // 00:00 - 05:00
        Morning = 1, // 05:00 - 10:00
        Midday = 2,  // 10:00 - 14:00
        Afternoon = 3, // 14:00 - 18:00
        Evening = 4, // 18:00 - 22:00
        Night = 5    // 22:00 - 24:00
    }
    [System.Serializable]
    public class GameCalendarData
    {
        public int TotalDaysPassed = 1; // Tracks total days passed (e.g., Day 45)
        public int CurrentDay = 1; // Day of the current season (1-30)
        public Season CurrentSeason = Season.Spring; // Current season
        public DayOfWeek CurrentDayOfWeek = DayOfWeek.Monday; // Day of the week
        public float CurrentTime = 6.0f; // In-game time (24h format)
        public TimeOfDay CurrentTimeOfDay = TimeOfDay.Morning; // Time of day
    }
    
    public static event System.Action<GameCalendarData> OnTimeOrDayChanged;

    [SerializeField]
    private float HourTickDuration = 10f; // Duration of each hour in seconds

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Runtime")]
    public GameCalendarData CalendarData { get; private set; } = new GameCalendarData();

    // private IEnumerator Start()
    // {
    //     // Start the time loop
    //     while (true)
    //     {
    //         TickTime();
    //         yield return new WaitForSeconds(HourTickDuration);
    //     }
    // }

    [Button]
    public void SkipToNextDaytime()
    {
        // Get the current time of day
        TimeOfDay currentTimeOfDay = GetTimeOfDay(CalendarData.CurrentTime);

        // Determine the next time of day
        TimeOfDay nextTimeOfDay = (TimeOfDay)(((int)currentTimeOfDay + 1) % System.Enum.GetValues(typeof(TimeOfDay)).Length);

        // Map the start time for each TimeOfDay
        float[] daytimeStartTimes = { 0f, 5f, 10f, 14f, 18f, 22f }; // Late, Morning, Midday, Afternoon, Evening, Night

        // Set the current time to the start of the next daytime
        CalendarData.CurrentTime = daytimeStartTimes[(int)nextTimeOfDay];

        // Check if we've advanced to the next day
        if (nextTimeOfDay == TimeOfDay.Late)
            AdvanceDay();

        // Update the current time of day
        CalendarData.CurrentTimeOfDay = nextTimeOfDay;

        Debug.Log($"Time skipped to: {GetFormattedTime()} | Date: {GetFormattedDate()}");
    }

    public void TickTime()
    {
        // Ticks the time by 1 hour. The game progresses 1 hour every 10 seconds.
        CalendarData.CurrentTime++;
        if (CalendarData.CurrentTime >= 24f)
            AdvanceDay();

        // Update the time of day
        CalendarData.CurrentTimeOfDay = GetTimeOfDay(CalendarData.CurrentTime);

        // Trigger the event to notify subscribers
        OnTimeOrDayChanged?.Invoke(CalendarData);

        Debug.Log($"Time: {GetFormattedTime()} | Date: {GetFormattedDate()}");
    }

    private void AdvanceDay()
    {
        CalendarData.CurrentTime = 0f;
        CalendarData.CurrentDay++;
        CalendarData.TotalDaysPassed++;

        if (CalendarData.CurrentDay > 30)
        {
            CalendarData.CurrentDay = 1;

            // Advance to the next season
            CalendarData.CurrentSeason = (Season)(((int)CalendarData.CurrentSeason + 1) % 4);
        }

        // Update the day of the week
        CalendarData.CurrentDayOfWeek = (DayOfWeek)(CalendarData.TotalDaysPassed % 7);
        
        // Trigger the event to notify subscribers
        OnTimeOrDayChanged?.Invoke(CalendarData);
    }

    private TimeOfDay GetTimeOfDay(float hour)
    {
        if (hour >= 0 && hour < 5)
            return TimeOfDay.Late;
        else if (hour >= 5 && hour < 10)
            return TimeOfDay.Morning;
        else if (hour >= 10 && hour < 14)
            return TimeOfDay.Midday;
        else if (hour >= 14 && hour < 18)
            return TimeOfDay.Afternoon;
        else if (hour >= 18 && hour < 22)
            return TimeOfDay.Evening;
        else return TimeOfDay.Night;
    }

    public string GetFormattedDate()
    {
        return $"{CalendarData.CurrentDayOfWeek}, {CalendarData.CurrentDay}, {CalendarData.CurrentSeason}";
    }

    public string GetFormattedTime()
    {
        TimeOfDay timeOfDay = GetTimeOfDay(CalendarData.CurrentTime);
        return $"{CalendarData.CurrentTime:00.00} ({timeOfDay})";
    }
}
