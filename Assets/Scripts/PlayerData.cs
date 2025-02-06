using System.Collections.Generic;
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

    public void LoadFromJson()
    {

        string PDataLoaded = System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        if (PDataLoaded == null)
        {
            Inventory = null;
            Coins = 0;
            Days = 0;
            FinishedGames = null;
            return;
        }
        GameData player = JsonUtility.FromJson<GameData>(PDataLoaded);
        Coins = player.Coins;
        Days = player.Days;
        Inventory = player.Inventory;
        FinishedGames = player.FinishedGames;
    }
    
    public void SaveIntoJson(List<Item> inventory, int coins, int days, List<PastPlayerData> finishedGames){
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


