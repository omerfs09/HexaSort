using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSkillPanel : UIPanelTemplate
{
    
    [SerializeField] private Button cancelButton;
    private Animator animator;
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
        UIManager.HideClearSkillPanel();
        GameController.Instance.ChangeControlState(ControlState.DragAndDrop);

    }
   

    public override void ShowPanel()
    {
        holder.SetActive(true);
        animator.SetTrigger("Open");
                
    }

    public override void HidePanel()
    {
        animator.SetTrigger("Close");
    }
    public void Close()
    {
        holder.SetActive(false);
    }
}
