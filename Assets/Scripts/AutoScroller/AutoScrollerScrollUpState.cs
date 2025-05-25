using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoScrollerScrollUpState : AutoScrollerBaseState
{
    private float scrollUpSpeed = 500.0f;
    public override void InitState(SettingsData settings)
    {
        scrollUpSpeed = settings.scrollUpSpeed;
    }

    public override void EnterState(AutoScrollerStateManager stateManager)
    {
        Debug.Log("[AutoScrollerScrollUpState.OnEnter]");
    }

    public override void UpdateState(AutoScrollerStateManager stateManager)
    {
        Debug.Log("[AutoScrollerScrollUpState.UpdateState]");
        stateManager.GetScrollRect().verticalScrollbar.value += (scrollUpSpeed * .01f) * Time.deltaTime;
        //contentRect.localPosition += (Vector3.down * downScrollSpeed) * Time.deltaTime;
        if (stateManager.GetScrollRect().verticalScrollbar.value >= 1)
            stateManager.TransitionTo(stateManager.as_IdleState);
    }
}