using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : UIPanelTemplate
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button hapticButton;
    [SerializeField] private Sprite soundOnIMG, soundOffIMG;
    [SerializeField] private Sprite hapticOnIMG, hapticOffIMG;
    public override void Awake()
    {
        base.Awake();
        settingsButton.onClick.AddListener(() => OpenClose());
        soundButton.onClick.AddListener(() => SoundButtonOnClick());
        hapticButton.onClick.AddListener(() => HapticButtonOnClick());
        UpdateImgs();
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
            GameController.Instance.ChangeControlState(ControlState.DragAndDrop);

            UIManager.HideSettingsPanel();
        }
        else
        {
            GameController.Instance.ChangeControlState(ControlState.InActive);

            UIManager.ShowSettingsPanel();
        }
    }
    public void SoundButtonOnClick() {
        if (Settings.GetSetting(SettingsEnum.SOUND) > 0)
        {
            Settings.SetSetting(SettingsEnum.SOUND, 0);
            soundButton.image.sprite = soundOffIMG;
        }
        else
        {
            Settings.SetSetting(SettingsEnum.SOUND, 1);
            soundButton.image.sprite = soundOnIMG;

        }
    }
    public void HapticButtonOnClick()
    {
        if (Settings.GetSetting(SettingsEnum.VIBRATION) > 0)
        {
            Settings.SetSetting(SettingsEnum.VIBRATION, 0);
            hapticButton.image.sprite = hapticOffIMG;

        }
        else
        {
            Settings.SetSetting(SettingsEnum.VIBRATION, 1);
            hapticButton.image.sprite = hapticOnIMG;

        }
    }
    void Start()
    {
        
    }
    public void UpdateImgs()
    {
        if(Settings.GetSetting(SettingsEnum.SOUND) > 0)
        {
            soundButton.image.sprite = soundOnIMG;
        }
        else
        {
            soundButton.image.sprite = soundOffIMG;

        }
        if (Settings.GetSetting(SettingsEnum.VIBRATION) > 0)
        {
            hapticButton.image.sprite = hapticOnIMG;
        }
        else
        {
            hapticButton.image.sprite = hapticOffIMG;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.Instance.ChangeControlState(ControlState.DragAndDrop);
            UIManager.HideSettingsPanel();
        }
    }
}
