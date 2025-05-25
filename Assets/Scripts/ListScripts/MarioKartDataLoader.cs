using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MarioKartDataLoader
{
    public static readonly string mk_DataFileName = Application.streamingAssetsPath + "/mk_data.json";
    public static MarioKartList TryLoadMKData()
    {
        Debug.Log("<color=cyan>[MarioKartDataLoader.TryLoadData]</color>");
        if (File.Exists(mk_DataFileName))
        {
            Debug.Log("<color=cyan>[MarioKartDataLoader.TryLoadData]</color> file Exists");
            string loadedFileData = File.ReadAllText(mk_DataFileName);
            MarioKartList marioKartList = JsonUtility.FromJson<MarioKartList>(loadedFileData);
            return marioKartList;
        }
        return null;
    }

    public static void SaveMKData(MarioKartList marioKartList)
    {
        Debug.Log("<color=cyan>[MarioKartDataLoader.SaveMKData]</color>");
        string mkListSaveData = JsonUtility.ToJson(marioKartList);
        File.WriteAllText(mk_DataFileName, mkListSaveData);
    }
}