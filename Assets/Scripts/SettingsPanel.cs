using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : UIPanelTemplate
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button hapticButton;
    public override void Awake()
    {
        base.Awake();
        settingsButton.onClick.AddListener(() => OpenClose());
        soundButton.onClick.AddListener(() => SoundButtonOnClick());
        hapticButton.onClick.AddListener(() => HapticButtonOnClick());
    }
    public override void HidePanel()
    {
        holder.SetActive(false);
    }

    public override void ShowPanel()
    {
        holder.SetActive(true);
    }
    public void OpenClose()
    {
        if (holder.activeInHierarchy)
        {
            UIManager.HideSettingsPanel();
        }
        else
        {
            UIManager.ShowSettingsPanel();
        }
    }
    public void SoundButtonOnClick() {
        if (Settings.GetSetting(SettingsEnum.SOUND) > 0) Settings.SetSetting(SettingsEnum.SOUND,0);
        else Settings.SetSetting(SettingsEnum.SOUND, 1);
    }
    public void HapticButtonOnClick()
    {
        if (Settings.GetSetting(SettingsEnum.VIBRATION) > 0) Settings.SetSetting(SettingsEnum.VIBRATION, 0);
        else Settings.SetSetting(SettingsEnum.VIBRATION, 1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
