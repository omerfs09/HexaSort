using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    [Header("Care to the syntax of the clip names")]

    public List<AudioClip> sounds;

    public static SFXManager Instance;

    private AudioSource _oneShotAudioSource;

    private Dictionary<AudioEnums, AudioSource> loopSources = new Dictionary<AudioEnums, AudioSource>();

    //public AudioSource bgSound;
    //public AudioSource ticktockSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _oneShotAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
    }

    public void PlayClipOneShot(AudioEnums soundName, float volume = 1)
    {
        if (Settings.GetSetting(SettingsEnum.SOUND) == 0) return;
        _oneShotAudioSource.PlayOneShot(sounds[(int)soundName], volume);
    }

    public void PlayClipAtLoop(AudioEnums soundName, float volume = 1)
    {
        if (loopSources.ContainsKey(soundName)) { ContinueLoopClip(soundName); return; }
        GameObject audioGameObject = new GameObject(soundName.ToString() + " Loop Sound");
        AudioSource tmpAudioSource = audioGameObject.AddComponent<AudioSource>();

        loopSources.Add(soundName, tmpAudioSource);
        tmpAudioSource.playOnAwake = true;
        tmpAudioSource.loop = true;
        tmpAudioSource.volume = volume;
        tmpAudioSource.clip = sounds[(int)soundName];
        tmpAudioSource.Play();
    }

    public void PauseLoopClip(AudioEnums soundName)
    {
        if (!loopSources.ContainsKey(soundName)) { return; }
        loopSources[soundName].Pause();
    }

    public void StopLoopClip(AudioEnums soundName)
    {
        if (!loopSources.ContainsKey(soundName)) { return; }
        loopSources[soundName].Stop();
    }

    public void ContinueLoopClip(AudioEnums soundName)
    {
        if (!loopSources.ContainsKey(soundName)) { return; }
        loopSources[soundName].Play();
    }

    public void PlayClipOnLocation(AudioEnums soundName, Vector3 position, float volume = 1)
    {
        
        AudioSource.PlayClipAtPoint(sounds[(int)soundName], position, volume);
    }
    public void HapticLow()
    {
        Vibration.Vibrate(100);
    }
    public void HapticMedium()
    {
        Vibration.Vibrate(500);
    }
    public void HapticHigh()
    {
        Vibration.Vibrate(1000);
    }

}
public enum AudioEnums
{
    AddToSlot,
    Lift,
    Pour,
    ClearDesk,
    ClearSlot,
    ClearSkill,
}
public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static bool IsOn()
    {
        return Settings.GetSetting(SettingsEnum.VIBRATION) > 0;
    }    
    public static void Vibrate()
    {
        if (IsOn())
        {
            if (isAndroid())
                vibrator.Call("vibrate");
            else
                Handheld.Vibrate();
        }

    }


    public static void Vibrate(long milliseconds)
    {
        if (IsOn())
        {
            if (isAndroid())
                vibrator.Call("vibrate", milliseconds);
            else
                Handheld.Vibrate();
        }

    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if (IsOn())
        {
            if (isAndroid())
                vibrator.Call("vibrate", pattern, repeat);
            else
                Handheld.Vibrate();
        }

    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
}