namespace Core;
public class Targets
{
    public Targets()
    {
        AsArray = [Chest, LargeChest, Player, Sign, Item, Other];
    }

    static AABB
        chestBox = (.0625, .0625, .5, .9375, .9375, 1.375),
        largeChestBox = (.0625, .0625, .5, .9375 + 1, .9375, 1.375),
        playerBox = (-.3, 1, -.3, .3, -.8, .3),
        signBox = (-.5, .0845, -.043, .5, .585, .041),
        itemBox = (-.125, -.125, -.125, .125, .125, .125),
        otherBox = (-.125, -.125, -.125, .125, .125, .125);

    public readonly TargetOpt[] AsArray;

    // If God exists, then why didn't he kill me at birth? 😈
    // Most cursed shit. pt 5
    public TargetOpt
        Chest=(true,((true,((.8,.5,0,.5),chestBox),1.3f),(true,((.8,.5,0,.09),chestBox)))),
        LargeChest=(true,((true,((.8,.6,.1,.5),largeChestBox),1.3f),(true,((.6,.5,0,.1),largeChestBox)))),
        Player=(true,((true,((.1,.8,.7,.75),playerBox),1),(true,((.1,.7,.8,.05),playerBox))),(true,Color.DistanceColor,1,0,.6f,0)),
        Sign=(true,((true,((.8,.6,.1,.5),signBox),.7f),(true,((.6,.5,0,.1),signBox)))),
        Item=(true,((true,((.8,.6,.1,.75),itemBox),.7f),(false,((.6,.5,0,.2),itemBox)))),
        Other=(false,new((true,((1,1,1,.75),otherBox),.7f)));
}