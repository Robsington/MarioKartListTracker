using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoScrollerBaseState
{
    public abstract void InitState(SettingsData settingsData);
    public abstract void EnterState(AutoScrollerStateManager stateManager);
    public abstract void UpdateState(AutoScrollerStateManager stateManager);
}