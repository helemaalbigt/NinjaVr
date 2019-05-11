
using UnityEngine;

public static class StringUtils
{
    public static string FormatSeconds(float s)
    {
        var returnString = Mathf.FloorToInt(s).ToString();
        returnString += ":";
        var decimals = Mathf.Round((s % 1) * 100);
        if (decimals == 0){
            returnString += "00";
        } else if (decimals < 10)
        {
            returnString += decimals + "0";
        }
        else
        {
            returnString += decimals.ToString();
        }
        return returnString;
    }
}
