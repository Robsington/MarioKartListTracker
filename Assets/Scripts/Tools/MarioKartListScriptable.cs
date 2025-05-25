using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kart List", menuName = "Mario Kart List")]
public class MarioKartListScriptable : ScriptableObject
{
    public SO_MarioKartList so_MarioKartList = new SO_MarioKartList();
}
[System.Serializable]
public class SO_MarioKartList
{
    public List<SO_MarioKartCup> marioKartCups = new List<SO_MarioKartCup>();
}
[System.Serializable]
public class SO_MarioKartCup
{
    public string cupName;
    public Sprite cupEmblem = null;
    public List<string> trackList = new List<string>();
}