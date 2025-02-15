using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Model
{
    public const int excangeRateFPtoTime = 2;
    
    private static string plPrefData = "plPref_data";
    private static UserData _user;
    private const int defGameTime = 10;
    private const int defFishAmount = 10;
    public static UserData User
    {
        get
        {
            if (_user == null || _user is {isInitialized: false})
            {
                if(PlayerPrefs.HasKey(plPrefData))
                {
                    var userData= PlayerPrefs.GetString(plPrefData, "");
                    _user = JsonUtility.FromJson<UserData>(userData);
                    _user.isInitialized = true;
                }
                else
                {
                    _user = new UserData
                    {
                        isInitialized = true
                    };
                }
            }
            return _user;
        }
    }
    public static void SaveData()
    {
        var data = Json.Serialize(User);
        Debug.Log("[SAVE] Data Saved:" + data);
        PlayerPrefs.SetString(plPrefData, data);
    }
    // public enum TreasureType
    // {
    //     treasure1,
    //     treasure2,
    //     treasure3,
    //     treasure4,
    //     treasure5,
    //     treasure6,
    //     treasure7,
    //     treasure8,
    //     treasure9,
    //     treasure10,
    //     treasure11,
    //     treasure12,
    //     treasure13,
    //     treasure14,
    //     treasure15,
    //     treasure16,
    //     treasure17,
    //     treasure18,
    //     treasure19,
    //     treasure20,
    // }
    public static bool MusicOn
    {
        set => User.musicOn = value;
        get => User.musicOn;
    }

    public static void ResetGameTime()
    { 
        GameTime = defGameTime;
    }public static void ResetFishAmount()
    { 
        FishAmount = defFishAmount;
    }
    public static int FishAmount
    {
        set => User.fishAmount = value;
        get => User.fishAmount;
    }
    public static int GameTime
    {
        set => User.gameTime = value;
        get => User.gameTime;
    }
    public static int addFishMultiplicator
    {
        set => User.addFishMultiplicator = value;
        get => User.addFishMultiplicator;
    }

    public static int addTimeMultiplicator
    {
        set => User.addTimeMultiplicator = value;
        get => User.addTimeMultiplicator;
    }
    public static int FishPoints
    {
        set => User.fishPts = value;
        get => User.fishPts;
    }

    public static void SetCollectionItem(string id, int amount)
    {
        var collected = TreasureCollection;
        if (collected.ContainsKey(id))
        {
            collected[id] = amount;
            User.collection = Json.Serialize(collected);
        }
        else
        {
            collected.Add(id, amount);
            User.collection = Json.Serialize(collected);
        }
    }
    public static void AddCollectionItem(string id)
    {
        var collected = TreasureCollection;
        if (collected.ContainsKey(id))
        {
            long cur = (long)collected[id];
            cur++;
            collected[id] = cur;
            User.collection = Json.Serialize(collected);
        }
        else
        {
            collected.Add(id,1);
            User.collection = Json.Serialize(collected);
        }
    }
    public static void AddFish(string id)
    {
        var items = AvailableFishes;
        if (!items.Contains(id))
        {
            items.Add(id);
            User.fishes = Json.Serialize(items);
        }
    }
    public static Dictionary<string, object> TreasureCollection
    {
        get
        {
            Dictionary<string, object> dict= new Dictionary<string, object>();
            if(User.collection != "")
                dict = Json.Deserialize(User.collection) as Dictionary<string, object>;
            return dict ;
            return null;
        }   
    }

    public static List<string> AvailableFishes
    {
        get
        {
            List<string> dict= new List<string>();
            if(User.collection != "")
                dict = Json.Deserialize(User.collection) as List<string>;
            return dict ;
            return null;
        }   
    }

    public static bool IsDecorPurchased(string typeStr)
    {
        var isOwned = GetCustomData("decor_owned_" + typeStr) != "";
        return isOwned;
    }

    public static void PurchaseDecor(string typeStr)
    {
        SetCustomData("decor_owned_" + typeStr, "true");
    }
    // public static List<string> AvailableDecor
    // {
    //     get
    //     {
    //         List<string> dict= new List<string>();
    //         if(User.collection != "")
    //             dict = Json.Deserialize(User.openDecor) as List<string>;
    //         return dict ;
    //     }   
    // }
    public static string GetCustomData(string key)
    {
        Dictionary<string, object> dataDict = AllCustomData;
        if (dataDict.ContainsKey(key)) 
            return dataDict[key].ToString();
        return "";
    }

    private static Dictionary<string, object> AllCustomData
    {
        get
        {
            if (string.IsNullOrEmpty(User.customUserData)) return new Dictionary<string, object>();
            Dictionary<string, object> dataDictFromJson = Json.Deserialize(User.customUserData) as  Dictionary<string, object>;
            return dataDictFromJson;
        }
    }
    public static void SetCustomData(string key, string value)
    {
        Dictionary<string, object> dataDict = AllCustomData;

        if (dataDict.ContainsKey(key)) 
            dataDict[key] = value;
        else
            dataDict.Add(key, value);
        User.customUserData = Json.Serialize(dataDict);
            
        return;
    }
    public class UserData
    {
        public string customUserData= ""; //Dict<string,long>
        // public string openDecor= ""; //Dict<string,long>
        public string collection= ""; //Dict<string,long>
        public string fishes= ""; //Dict<string,long>
        public bool isInitialized = false;
        public bool musicOn = true;
        public int fishPts = 0;
        public int gameTime = defGameTime;
        public int fishAmount = defFishAmount;
        public int addFishMultiplicator = 1;
        public int addTimeMultiplicator = 1;
    }
    
}
