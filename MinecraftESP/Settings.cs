using ESP.Structs;
using ESP.Structs.Options;

namespace ESP;
public class Settings
{
    public Settings()
    {
        AsList = new List<TargetOpt>()
        {
            Chest,
            LargeChest,
            Player,
            Sign,
            Item,
            Other
        };
    }

    public bool NoLight, NoBackground, NoFog, AntiCullFace, WorldChams, CaveViewer, RainbowText, ESP = true;

    private static AABB chestBox = new AABB(.0625, .0625, .5, .9375, .9375, .9375 + .5 - .0625);
    private static AABB largeChestBox = new AABB(.0625, .0625, .5, .9375 + 1, .9375, .9375 + .5 - .0625);
    private static AABB playerBox = new AABB(-.3, 1, -.3, .3, -.8, .3);
    private static AABB signBox = new AABB(-.5, .0845, -.043, .5, .585, .041);
    private static AABB itemBox = new AABB(-.125, -.125, -.125, .125, .125, .125);
    private static AABB otherBox = new AABB(-.125, -.125, -.125, .125, .125, .125);

    public List<TargetOpt> AsList { get; init; }

    public TargetOpt Chest = new TargetOpt(
        true, 
        new Box(
            new LBox(true, new CAABB(new Color(.8, .5, 0, .5), chestBox), 1.3f),
            new PBox(true, new CAABB(new Color(.8, .5, 0, .09), chestBox))
        ));

    public TargetOpt LargeChest = new TargetOpt(
        true,
        new Box(
            new LBox(true, new CAABB(new Color(.8, .6, .1, .5), largeChestBox), 1.3f),
            new PBox(true, new CAABB(new Color(.6, .5, 0, .1), largeChestBox))
        ));

    public TargetOpt Player = new TargetOpt(
        true,
        new Box(
            new LBox(true, new CAABB(new Color(.1, .8, .7, .75), playerBox), 1),
            new PBox(true, new CAABB(new Color(.1, .7, .8, .05), playerBox))
        ),
        new Tracer(true, Color.DistanceColor, 1, 0, .6f, 0),
        new Chams(true, false, new Color(), true)
        );

    public TargetOpt Sign = new TargetOpt(
        true,
        new Box(
            new LBox(true, new CAABB(new Color(.8, .6, .1, .5), signBox), .7f),
            new PBox(true, new CAABB(new Color(.6, .5, 0, .1), signBox))
        ));

    public TargetOpt Item = new TargetOpt(
        true,
        new Box(
            new LBox(true, new CAABB(new Color(.8, .6, .1, .75), itemBox), .7f),
            new PBox(false, new CAABB(new Color(.6, .5, 0, .2), itemBox))
        ));

    public TargetOpt Other = new TargetOpt(
        false,
        new Box(
            new LBox(true, new CAABB(new Color(1, 1, 1, .75), otherBox), .7f)
        ));
}