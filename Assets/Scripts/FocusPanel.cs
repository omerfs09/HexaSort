using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusPanel : UIPanelTemplate
{
    [SerializeField]private Image focusIMG;
    Animator animator;
    public override void Awake()
    {
        base.Awake();
        animator = gameObject.GetComponent<Animator>();
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        animator.SetTrigger("Open");

    }
    public override void HidePanel()
    {
        animator.SetTrigger("Close");
    }
    //public void SetFocusColor(Color color)
    //{
    //    focusIMG.color = color;
    //}
}
