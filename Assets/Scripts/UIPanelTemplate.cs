using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanelTemplate : MonoBehaviour
{
    public bool IsActiveOnStart = false;
    public PanelType panelType;
    public abstract void ShowPanel();
    public abstract void HidePanel();
    
    
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