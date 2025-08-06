using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalAnimationPanel : UIPanelTemplate
{
    [SerializeField]private TextMeshProUGUI messageTMP;
    private string message;
    public Animator animator;
    public override void Awake()
    {
        base.Awake();
        animator = gameObject.GetComponent<Animator>();
    }
    public void SetMessage(string message)
    {
        this.message = message;
    }
    public override void ShowPanel()
    {
        holder.SetActive(true);
        messageTMP.text = message;
        animator.SetTrigger("Open");
    }
}
