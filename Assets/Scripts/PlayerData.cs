using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private List<Item> Inventory;
    [SerializeField] private int Coins;
    [SerializeField] private int Days;
    [SerializeField] private List<PastPlayerData> FinishedGames;

    private List<string> itemNames = new List<string>
    {
        "Parrot Fish", "Mackerel", "Clown Fish", "Plaice", "Silver Eel", "Sea Horse", "Lionfish", "Cowfish", "Tuna",
        "Banded Butterflyfish", "Atlantic Bass", "Blue Tang", "Pollock", "Ballan Wrasse", "Weaver Fish", "Bream",
        "Pufferfish", "Cod", "Dab", "Flounder", "Whiting", "Halibut", "Herring", "Stingray", "Wolfish", "Bonefish",
        "Cobia", "Black Drum", "Blobfish", "Pompano", "Sardine", "Angelfish", "Red Snapper", "Salmon", "Anglerfish",

        "Shrimp", "Squid", "Dumbo Octopus", "Crab", "Lobster", "Sea Angel", "Turtle", "Octopus", "Pink Fantasia",
        "Sea Spider", "Jellyfish", "Sea Cucumber", "Christmas Tree Worm", "Sea Pen", "Sea Urchin", "Blue Lobster",
        "Saltwater Snail",

        "Crucian Carp", "Bluegill", "Tilapia", "Smelt", "Trout", "Betta", "Rainbow Trout", "Yellow Perch", "Char",
        "Guppy", "King Salmon", "Neon Tetra", "Piranha", "Bitterling", "Black Bass", "Eel", "Chub", "Perch",
        "Crappie", "Catfish", "Walleye", "Dace", "Loach", "Largemouth Bass",

        "Water Beetle", "Crayfish", "Snake", "Freshwater Snail",

        "Goldfish", "Koi", "Grass Carp", "Fathead Minnow", "Green Sunfish", "Plecostomus", "Red Shiner",
        "Pumpkin Seed Fish", "Goby", "Shubukin", "Fancy Goldfish", "High Fin Banded Shark", "Paradise Fish",
        "Gizzard Shad", "Rosette", "Golden Tench", "Molly",

        "Frog", "Tadpole", "Axolotl"
    };

    public int GetRandomItemIndex()
    {
        int index = Random.Range(0, itemNames.Count);
        return index;
    }

    public string GetNameFromIndex(int index)
    {
        return itemNames[index];
    }

    public void Start()
    {
        LoadFromJson();
    }

    public List<Item> GetInventory()
    {
        return this.Inventory;
    }

    public int GetCoins()
    {
        return this.Coins;
    }

    public int GetDays()
    {
        return this.Days;
    }

    public List<PastPlayerData> GetFinishedGames()
    {
        return this.FinishedGames;
    }

    public void SetInventory(List<Item> inventory)
    {
        Debug.Log("Setting Inventory");
        this.Inventory = inventory;
    }

    public void SetCoins(int coins)
    {
        this.Coins = coins;
    }

    public void SetDays(int days)
    {
        this.Days = days;
    }

    public void SetFinishedGames(List<PastPlayerData> finishedGames)
    {
        this.FinishedGames = finishedGames;
    }

    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";

        if (!File.Exists(path))
        {
            Inventory = new List<Item>();
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


