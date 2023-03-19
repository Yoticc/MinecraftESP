using ESP.Structs.Options;
using ESP.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    private static AABB playerBox = new AABB(-.3, 1, -.3, .3, 2.8, .3);
    private static AABB signBox = new AABB(-.475, .11, .02, .475, .275, .04);
    private static AABB itemBox = new AABB(-.125, -.125, -.125, .125, .125, .125);
    private static AABB otherBox = new AABB(-.125, -.125, -.125, .125, .125, .125);

    public List<TargetOpt> AsList { get; init; }

    public TargetOpt Chest = new TargetOpt(
        true, 
        new Box(
            new LBox(true, new CAABB(new Color(.8, .5, 0, .75), chestBox), 1.3f), 
            new PBox(true, new CAABB(new Color(.8, .5, 0, .2), chestBox))
        ),
        new Tracer(true, new Color(100, 50, 120, 200), 2, 0, .0625f, -.4375f)
        );

    public TargetOpt LargeChest = new TargetOpt(
        false,
        new Box(
            new LBox(true, new CAABB(new Color(.8, .6, .1, .75), largeChestBox), 1.3f),
            new PBox(true, new CAABB(new Color(.6, .5, 0, .2), largeChestBox))
        ));

    public TargetOpt Player = new TargetOpt(
        false,
        new Box(
            new LBox(true, new CAABB(new Color(.8, .6, .1, .75), playerBox), 1),
            new PBox(true, new CAABB(new Color(.6, .5, 0, .2), playerBox))
        ),
        new Tracer(true, new Color(1, 1, 1, .9), 1, 0, 2.6f, 0),
        new Chams(true, false, new Color(), true)
        );

    public TargetOpt Sign = new TargetOpt(
        false,
        new Box(
            new LBox(true, new CAABB(new Color(.8, .6, .1, .75), signBox), .7f),
            new PBox(true, new CAABB(new Color(.6, .5, 0, .2), signBox))
        ));

    public TargetOpt Item = new TargetOpt(
        false,
        new Box(
            new LBox(true, new CAABB(new Color(.8, .6, .1, .75), itemBox), .7f),
            new PBox(true, new CAABB(new Color(.6, .5, 0, .2), itemBox))
        ));

    public TargetOpt Other = new TargetOpt(
        false,
        new Box(
            new LBox(true, new CAABB(new Color(1, 1, 1, .75), otherBox), .7f)
        ));
}