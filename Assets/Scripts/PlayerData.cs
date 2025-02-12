using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private List<Item> Inventory;
    [SerializeField] private int Coins;
    [SerializeField] private int Days;
    [SerializeField] private List<PastPlayerData> FinishedGames;

    public void Start()
    {
        LoadFromJson();
    }

    public List<Item> GetInventory()
    {
        return Inventory;
    }

    public int GetCoins()
    {
        return Coins;
    }

    public int GetDays()
    {
        return Days;
    }

    public List<PastPlayerData> GetFinishedGames()
    {
        return FinishedGames;
    }

    public void SetInventory(List<Item> inventory)
    {
        Debug.Log("Setting Inventory");
        Inventory = inventory;
    }

    public void SetCoins(int coins)
    {
        Coins = coins;
    }

    public void SetDays(int days)
    {
        Days = days;
    }

    public void SetFinishedGames(List<PastPlayerData> finishedGames)
    {
        FinishedGames = finishedGames;
    }

    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";

        if (!File.Exists(path))
        {
            Inventory = null;
            Coins = 0;
            Days = 0;
            FinishedGames = null;
            return;
        }

        string PDataLoaded = System.IO.File.ReadAllText(path);
        GameData player = JsonUtility.FromJson<GameData>(PDataLoaded);
        Coins = player.Coins;
        Days = player.Days;
        Inventory = player.Inventory;
        FinishedGames = player.FinishedGames;
    }
    
    public void SaveIntoJson(List<Item> inventory, int coins, int days, List<PastPlayerData> finishedGames)
    {
        GameData player = new GameData();
        player.Inventory = inventory;
        player.Coins = coins;
        player.Days = days;
        player.FinishedGames = finishedGames;

        string PDataSaved = JsonUtility.ToJson(player);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", PDataSaved);
    }



}

[System.Serializable]
public struct GameData
{
    public List<Item> Inventory;
    public int Coins;
    public int Days;
    public List<PastPlayerData> FinishedGames;

   
    public GameData(List<Item> inventory, int coins, int days, List<PastPlayerData> finishedGames)
    {
        Inventory = inventory;
        Coins = coins;
        Days = days;
        FinishedGames = finishedGames;
    }

}

[System.Serializable]

public struct PastPlayerData
{
    public List<Item> Inventory;
    public int Coins;
    public int Days;
    public PastPlayerData(List<Item> inventory, int coins, int days)
    {
        Inventory = inventory;
        Coins = coins;
        Days = days;
    }
}


