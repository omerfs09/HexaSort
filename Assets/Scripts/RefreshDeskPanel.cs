using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshDeskPanel : UIPanelTemplate
{
    public override void HidePanel()
    {
        holder.SetActive(false);
    }

    public override void ShowPanel()
    {
        holder.SetActive(true);
    }
}
