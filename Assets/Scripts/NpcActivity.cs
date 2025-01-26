using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NpcActivity : MonoBehaviour
{
    public List<NpcActivityData> Activities = new List<NpcActivityData>();
    [TableList]
    public List<NpcActivityByDay> DailyActivity = new List<NpcActivityByDay>();
}

[System.Serializable]
public class NpcActivityByDay
{
    public GameCalendar.DayOfWeek DayOfWeek;
    [TableList]
    public List<NpcActivityByTime> ActivitiesByDaytime = new List<NpcActivityByTime>();
}
[System.Serializable]
public class NpcActivityByTime
{
    public GameCalendar.TimeOfDay TimeOfDay;
    public List<int> activityIds = new List<int>();
}
[System.Serializable]
public class NpcActivityData
{
    public string activityId; // Unique ID for the activity
    public string Description; // e.g., "Cooking in the kitchen"
    public string SceneName; // Name of the scene
    public Vector3 Position; // Position in the scene
    public string AnimationName; // Name of the animation
    public List<string> ConversationIds; // IDs for available conversations
    public string Requirement; // Placeholder for complex requirements

    public NpcActivityData(string description, string sceneName, Vector3 position, string animationName, List<string> conversationIds, string requirement)
    {
        Description = description;
        SceneName = sceneName;
        Position = position;
        AnimationName = animationName;
        ConversationIds = conversationIds;
        Requirement = requirement;
    }
}