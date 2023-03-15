using ESP.Structs;
using ESP.Structs.Options;
using ESP.Utils;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using Render = ESP.Render;
using RH = ESP.RenderHook;
using RU = ESP.Utils.RenderUtils;

namespace ESP;
public unsafe class Render
{
    public Settings Settings = new Settings();

    public bool Enable(ref Cap cap)
    {
        try
        {
            if (cap == Cap.Lighting && Settings.NoLight) { }
            else if (cap == Cap.Fog && Settings.NoFog) { }
            else if (cap == Cap.CullFace && Settings.AntiCullFace) { }
            else if (cap == Cap.DepthTest && Settings.CaveViewer) { }
            else return true;

        }
        catch (Exception ex) { Interop.MessageBox(0, ex.ToString(), "C# Exception", 0); }

        return false;
    }

    public bool Disable(ref Cap cap)
    {
        try
        {
            if (cap == Cap.Blend && Settings.WorldChams) { }
            else if (cap == Cap.Texture2D && Settings.NoBackground) { }
            else return true;
        }
        catch (Exception ex) { Interop.MessageBox(0, ex.ToString(), "C# Exception", 0); }


        return false;
    }

    public bool Begin(ref Mode mode)
    {
        try
        {
            if (mode == Mode.TrianglesStript && Settings.RainbowText)
            {
                (float r, float g, float b) color = RGB.GetF();
                GL.Color3f(color.r, color.g, color.b);
            }

        }
        catch (Exception ex) { Interop.MessageBox(0, ex.ToString(), "C# Exception", 0); }

        return true;
    }

    public bool TranslateF(ref float x, ref float y, ref float z)
    {
        try
        {
            if (x == .5 && y == .4375 && z == .9375)
                Settings.Chest.Add(GetTarget(Settings.Chest, 0, .0625f, -.4375f));
            else if (x == 1 && y == 0.4375 && z == 0.9375)
                Settings.LargeChest.Add(GetTarget(Settings.LargeChest, 0, .0625f, -.4375f));

        }
        catch (Exception ex) { Interop.MessageBox(0, ex.ToString(), "C# Exception", 0); }


        return true;
    }

    public bool ScaleF(ref float x, ref float y, ref float z)
    {
        try
        {
            if (x == .9375 && y == .9375 && z == .9375)
                Settings.LargeChest.Add(GetTarget(Settings.Player, 0, -1, 0));
            else if (x == .25 && y == .25 && z == .25)
                Settings.Item.Add(GetTarget(Settings.Item));
            else if (x == .5 && y == .5 && z == .5)
                Settings.Item.Add(GetTarget(Settings.Item));
            else Settings.Other.Add(GetTarget(Settings.Other));
        }
        catch (Exception ex) { Interop.MessageBox(0, ex.ToString(), "C# Exception", 0); }

        

        return true;
    }


    public bool Ortho(ref double left, ref double right, ref double bottom, ref double top, ref double zNear, ref double zFar)
    {
        try
        {
            if (zNear != 1000 || zFar != 3000)
                return true;

            GL.PushAttrib(0x000fffff);
            GL.PushMatrix();

            RH.Disable(Cap.Texture2D);
            RH.Disable(Cap.CullFace);
            RH.Disable(Cap.Lighting);
            RH.Disable(Cap.DepthTest);

            RH.Enable(Cap.LineSmooth);

            RH.Enable(Cap.Blend);
            GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

            foreach (TargetOpt targetOpt in Settings.AsList)
            {
                foreach (GLTarget target in targetOpt.Targets)
                {
                    target.DrawOver(targetOpt);
                }
                targetOpt.Targets.Clear();
            }

            GL.PopAttrib();
            GL.PopMatrix();

        }
        catch (Exception ex) { Interop.MessageBox(0, ex.ToString(), "C# Exception", 0); }        

        return true;
    }

    
    private GLTarget GetTarget(TargetOpt options, float offsetX = 0, float offsetY = 0, float offsetZ = 0)
    {
        GLTarget target = new GLTarget();

        GL.GetFloatv(PName.ProjectionMatrix, target.Projection);
        GL.GetFloatv(PName.ModelviewMatrix, target.Modelview);

        float[] m3 = new float[4];
        for (int i = 0; i < 4; ++i)
            m3[i] = target.Modelview[i] * offsetX + target.Modelview[i + 4] * offsetY + target.Modelview[i + 8] * offsetZ + target.Modelview[i + 12];
        Buffer.BlockCopy(m3, 0, target.Modelview, 12, sizeof(float) * m3.Length);

        target.DrawDuring(options);

        return target;
    }
}