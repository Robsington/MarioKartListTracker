using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTimer : MonoBehaviour
{
    [SerializeField] private AutoScrollerStateManager autoScroller = null;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float maxIdleTime = 20.0f;
    public void SetIdleTime(SettingsData settingsData) { maxIdleTime = settingsData.autoScrollTimeout; }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || 
            Input.anyKeyDown ||
            Input.GetAxis("Mouse ScrollWheel") > 0 ||
            Input.GetAxis("Mouse ScrollWheel") < 0 || 
            Input.GetMouseButton(0))
        {
            autoScroller.Deactivate();
            RestartTimer();
            return;
        }
        CountDown();
    }

    private void CountDown()
    {
        timer += Time.deltaTime;
        if(timer >= maxIdleTime)
        {
            autoScroller.Activate();
            RestartTimer();
        }
    }

    private void RestartTimer()
    {
        timer = 0.0f;
    }
}
