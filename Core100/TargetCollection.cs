namespace Core;
public record TargetCollection(TargetOptions Options)
{
    public readonly List<GLTarget> Targets = [];

    public static readonly TargetCollection
        Chest = new(TargetOptions.Chest),
        LargeChest = new (TargetOptions.LargeChest),
        Player = new(TargetOptions.Player),
        Sign = new(TargetOptions.Sign),
        Item = new(TargetOptions.Item),
        Other = new(TargetOptions.Other);

    public static readonly TargetCollection[] AsArray = [Chest, LargeChest, Player, Sign, Item, Other];
}