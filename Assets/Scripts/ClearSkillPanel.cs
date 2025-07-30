using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSkillPanel : UIPanelTemplate
{
    
    public Button cancelButton;
     Animator animator;
    public override void Awake()
    {
        base.Awake();
        animator = gameObject.GetComponent<Animator>();

    }
    void Start()
    {
        cancelButton.onClick.AddListener(() => OnCancelButtonClick());
    }
    public void OnCancelButtonClick()
    {
        UIManager.HidePanel(panelType);
        GameController.Instance.ChangeControlState(ControlState.DragAndDrop);

    }
   

    public override void ShowPanel()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Open");
                
    }

    public override void HidePanel()
    {
        animator.SetTrigger("Close");
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
