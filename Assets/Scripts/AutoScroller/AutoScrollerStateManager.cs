using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScrollerStateManager : MonoBehaviour
{
    public AutoScrollerBaseState as_currentState;
    public AutoScrollerIdleState as_IdleState = new AutoScrollerIdleState();
    public AutoScrollerScrollDownState as_ScrollDownState = new AutoScrollerScrollDownState();
    public AutoScrollerScrollUpState as_ScrollUpState = new AutoScrollerScrollUpState();
    public AutoScrollerDeactivatedState as_DeactivatedState = new AutoScrollerDeactivatedState();
    [SerializeField] private ScrollRect scrollRect = null;
    private bool isActive = false;
    public ScrollRect GetScrollRect() { return scrollRect; }
    public void InitializeAutoScroller(SettingsData settings)
    {
        //Debug.Log("[AutoScrollerStateManager.InitializeAutoScroller]");
        as_IdleState.InitState(settings);
        as_ScrollDownState.InitState(settings);
        as_ScrollUpState.InitState(settings);
        as_currentState = as_IdleState;
        as_currentState.EnterState(this);
    }

    private void Update()
    {
        //Debug.Log("[AutoScrollerStateManager.Update]");
        as_currentState.UpdateState(this);
    }

    public void TransitionTo(AutoScrollerBaseState newState)
    {
        as_currentState = newState;
        as_currentState.EnterState(this);
    }

    public void Deactivate()
    {
        isActive = false;
        as_currentState = as_DeactivatedState;
        as_currentState.EnterState(this);
    }

    public void Activate()
    {
        if (isActive)
            return;
        isActive = true;
        as_currentState = as_IdleState;
        as_currentState.EnterState(this);
    }
}
