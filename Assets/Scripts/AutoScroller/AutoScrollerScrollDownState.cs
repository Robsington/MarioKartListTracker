using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScrollerScrollDownState : AutoScrollerBaseState
{
    private float scrollDownSpeed = 100.0f;
    public override void InitState(SettingsData settings)
    {
        scrollDownSpeed = settings.scrollDownSpeed;
    }

    public override void EnterState(AutoScrollerStateManager stateManager)
    {
        Debug.Log("[AutoScrollerScrollDownState.OnEnter]");
    }

    public override void UpdateState(AutoScrollerStateManager stateManager)
    {
        Debug.Log("[AutoScrollerScrollDownState.UpdateState]");
        stateManager.GetScrollRect().verticalScrollbar.value -= (scrollDownSpeed * .01f) * Time.deltaTime;
        if (stateManager.GetScrollRect().verticalScrollbar.value <= 0)
            stateManager.TransitionTo(stateManager.as_IdleState);
    }
}