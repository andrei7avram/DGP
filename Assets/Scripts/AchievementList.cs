using UnityEngine;

[CreateAssetMenu(fileName = "AchievementList", menuName = "Achievements")]
public class AchievementList : ScriptableObject
{
    public Achievement[] achievements;
        
}

[System.Serializable]
public class Achievement
{
    public string AchievementID;
    public string Title;
    public string Description;
}