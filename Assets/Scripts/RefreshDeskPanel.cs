using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshDeskPanel : UIPanelTemplate
{
    public override void HidePanel()
    {
        gameObject.SetActive(false);
    }

    public override void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
