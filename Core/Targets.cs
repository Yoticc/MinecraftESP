namespace Core;
public class Targets
{
    public Targets()
    {
        AsArray = new[]
        {
            Chest,
            LargeChest,
            Player,
            Sign,
            Item,
            Other
        };
    }

    static AABB 
        chestBox = new(.0625, .0625, .5, .9375, .9375, 1.375),
        largeChestBox = new(.0625, .0625, .5, .9375 + 1, .9375, 1.375),
        playerBox = new(-.3, 1, -.3, .3, -.8, .3),
        signBox = new(-.5, .0845, -.043, .5, .585, .041),
        itemBox = new(-.125, -.125, -.125, .125, .125, .125),
        otherBox = new(-.125, -.125, -.125, .125, .125, .125);

    public TargetOpt[] AsArray { get; init; }

    public TargetOpt
    Chest = new(true,
        new Box(
            new LBox(true, new(new(.8, .5, 0, .5), chestBox), 1.3f),
            new PBox(true, new(new(.8, .5, 0, .09), chestBox))
        )),

    LargeChest = new(true,
        new Box(
            new LBox(true, new(new(.8, .6, .1, .5), largeChestBox), 1.3f),
            new PBox(true, new(new(.6, .5, 0, .1), largeChestBox))
        )),

    Player = new(true,
        new Box(
            new LBox(true, new(new(.1, .8, .7, .75), playerBox), 1),
            new PBox(true, new(new(.1, .7, .8, .05), playerBox))
        ),
        new Tracer(true, Color.DistanceColor, 1, 0, .6f, 0)
        ),

    Sign = new(true,
        new Box(
            new LBox(true, new(new(.8, .6, .1, .5), signBox), .7f),
            new PBox(true, new(new(.6, .5, 0, .1), signBox))
        )),

    Item = new(true,
        new Box(
            new LBox(true, new(new(.8, .6, .1, .75), itemBox), .7f),
            new PBox(false, new(new(.6, .5, 0, .2), itemBox))
        )),

    Other = new(true,
        new Box(
            new LBox(true, new(new(1, 1, 1, .75), otherBox), .7f)
        ));
}