using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSkillPanel : UIPanelTemplate
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
        UIManager.HideMoveSkillPanel();
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
}
