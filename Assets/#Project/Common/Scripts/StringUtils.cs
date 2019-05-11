
using UnityEngine;

public static class StringUtils
{
    public static string FormatSeconds(float s)
    {
        var returnString = Mathf.FloorToInt(s).ToString();
        returnString += ":";
        returnString += Mathf.Round((s % 1) * 100);
        return returnString;
    }
}
