using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollerIdleState : AutoScrollerBaseState
{
    [SerializeField] private float idleTime = 5.0f;
    private float idleTimer = 0.0f;
    
    public override void InitState(SettingsData settings)
    {
        
    }

    public override void EnterState(AutoScrollerStateManager stateManager)
    {
        Debug.Log("[AutoScrollerIdleState.EnterState]");
        idleTimer = 0.0f;
    }

    public override void UpdateState(AutoScrollerStateManager stateManager)
    {
        idleTimer += Time.deltaTime;

        if(idleTimer >= idleTime)
        {
            Debug.Log("[AutoScrollerIdleState.UpdateState]");
            if (stateManager.GetScrollRect().verticalScrollbar.value <= 0)
                stateManager.TransitionTo(stateManager.as_ScrollUpState);
            else
                stateManager.TransitionTo(stateManager.as_ScrollDownState);
        }
    }
}