using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MarioKartList
{
    public int totalTracksPlayed = 0;
    public bool allCupsCompleted = false;
    public List<MarioKartCup> cupList = new List<MarioKartCup>();
    public void ClearData()
    {
        totalTracksPlayed = 0;
        allCupsCompleted = false;
        for(int i = 0; i < cupList.Count; i++)
            cupList[i].ClearData();
    }
}

[System.Serializable]
public class MarioKartCup
{
    public string cupName = string.Empty;
    public bool cupCompleted = false;
    public int totalTracksPlayedCounter = 0;
    public List<MarioKartTrack> cupTracks = new List<MarioKartTrack>();
    public void ClearData()
    {
        cupCompleted = false;
        totalTracksPlayedCounter = 0;
        for (int i = 0; i < cupTracks.Count; i++)
            cupTracks[i].ClearData();
    }
}

[System.Serializable]
public class MarioKartTrack
{
    public string trackName = string.Empty;
    public bool trackCompleted = false;
    public int trackCounter = 0;
    public void ClearData()
    {
        trackCompleted = false;
        trackCounter = 0;
    }
}