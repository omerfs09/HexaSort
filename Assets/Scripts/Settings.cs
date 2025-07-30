using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    
   
    public static int GetSetting(SettingsEnum settingsEnum)
    {
        return PlayerPrefs.GetInt(settingsEnum.ToString(),1);
    }

}
public enum SettingsEnum
{
    MUSIC,
    SOUND,
    VIBRATION,
}
