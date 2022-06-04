namespace Dragon.Game.Configurations.Data;

public class Player {
    public bool PersonalShop { get; set; }
    public int MaximumNameLength { get; set; }
    public int MaximumAuras { get; set; }
    public int MaximumHeraldries { get; set; }
    public int MaximumRecipes { get; set; }
    public int MaximumSkills { get; set; }
    public int MaximumTitles { get; set; }
    public int MaximumLevel { get; set; }
    public int MaximumWarehouse { get; set; }
    public int MaximumInventory { get; set; }
    public int MaximumEffects { get; set; }
    public int MaximumMails { get; set; }
    public int InitialInventorySize { get; set; }
    public int InitialWarehouseSize { get; set; }
    public bool AutomaticSave { get; set; }
    public int SaveTimeInMinutes { get; set; }

    public Player() {
        PersonalShop = true;
        MaximumAuras = 10;
        MaximumLevel = 20;
        MaximumHeraldries = 17;
        MaximumRecipes = 25;
        MaximumSkills = 35;
        MaximumTitles = 30;
        MaximumEffects = 30;
        MaximumWarehouse = 50;
        MaximumInventory = 175;
        MaximumMails = 30;
        InitialInventorySize = 35;
        InitialWarehouseSize = 20;
        MaximumNameLength = 32;

        AutomaticSave = true;
        SaveTimeInMinutes = 15;
    }
}