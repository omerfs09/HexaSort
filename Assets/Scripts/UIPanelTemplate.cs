using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanelTemplate : MonoBehaviour
{
    public bool IsActiveOnStart = false;
    public PanelType panelType;
    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
    
    public virtual void Awake()
    {
        UIManager.RegisterPanel(panelType, this);
        if (IsActiveOnStart) ShowPanel();
        else gameObject.SetActive(false);
    }
}
public enum PanelType
{
    BoostersPanel,
    SettingsPanel,
    ClearSkillPanel,
    MoveSkillPanel,
    RefreshDeskPanel,
}