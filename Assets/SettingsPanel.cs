using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : UIPanelTemplate
{
    public Button settingsButton;
    public override void Awake()
    {
        base.Awake();
        settingsButton.onClick.AddListener(() => OpenClose());
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
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
