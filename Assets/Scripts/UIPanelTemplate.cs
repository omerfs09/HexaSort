using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanelTemplate : MonoBehaviour
{
    public GameObject holder;
    public bool IsActiveOnStart = false;
    public PanelType panelType;
    public virtual void ShowPanel()
    {
        holder.SetActive(true);
    }
    public virtual void HidePanel()
    {
        holder.SetActive(false);
    }
    
    
    public virtual void Awake()
    {
        UIManager.RegisterPanel(panelType, this);
        if (IsActiveOnStart) ShowPanel();
        else holder.SetActive(false);
    }
}
public enum PanelType
{
    BoostersPanel,
    SettingsPanel,
    ClearSkillPanel,
    MoveSkillPanel,
    RefreshDeskPanel,
    GameOverPanel,
    ProgressBar,
}