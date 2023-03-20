using ESP.Hood;
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
        if (cap == Cap.Lighting && Settings.NoLight) { }
        else if (cap == Cap.Fog && Settings.NoFog) { }
        else if (cap == Cap.CullFace && Settings.AntiCullFace) { }
        else if (cap == Cap.DepthTest && Settings.CaveViewer) { }
        else return true;

        return false;
    }

    public bool Disable(ref Cap cap)
    {
        if (cap == Cap.Blend && Settings.WorldChams) { }
        else if (cap == Cap.Texture2D && Settings.NoBackground) { }
        else return true;


        return false;
    }

    public bool Begin(ref Mode mode)
    {
        if (mode == Mode.TrianglesStript && Settings.RainbowText)
        {
            (float r, float g, float b) color = RGB.GetF();
            GL.Color3f(color.r, color.g, color.b);
        }
        
        return true;
    }

    public bool TranslateF(ref float x, ref float y, ref float z)
    {
        if (x == .5 && y == .4375 && z == .9375)
            SetTarget(Settings.Chest, 0, .0625f, -.4375f);
        else if (x == 1 && y == 0.4375 && z == 0.9375)
            SetTarget(Settings.LargeChest, 0, .0625f, -.4375f);

        return true;
    }

    public bool ScaleF(ref float x, ref float y, ref float z)
    {
        if (x == .9375 && y == .9375 && z == .9375)
            SetTarget(Settings.Player, 0, -1, 0);
        else if (x == .25 && y == .25 && z == .25)
            SetTarget(Settings.Item);
        else if (x == .5 && y == .5 && z == .5)
            SetTarget(Settings.Item);
        else SetTarget(Settings.Other);

        return true;
    }


    public bool Ortho(ref double left, ref double right, ref double bottom, ref double top, ref double zNear, ref double zFar)
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

        /*
        foreach (GLTarget target in Settings.Chest.Targets)
        {
            GL.MatrixMode(Matrix.Projection);
            GL.LoadMatrixf(target.Projection);

            GL.MatrixMode(Matrix.Modelview);
            GL.LoadMatrixf(target.Modelview);

            GL.LoadIdentity();

            GL.Begin(Mode.Lines);
            GL.Vertex3f(0, 0, -.1f);
            GL.Vertex3f(target.Modelview[12], target.Modelview[13], target.Modelview[14]);
            GL.End();
        }
        */

        
        foreach (TargetOpt targetOpt in Settings.AsList)
            if (targetOpt.Enabled)
                foreach (GLTarget target in targetOpt.Targets)
                {
                    if (!target.IsValid)
                        break;

                    target.DrawOver(targetOpt);
                }

        /*
        foreach (GLTarget target in Settings.Chest.Targets)
            target.DrawOver(Settings.Chest);
        */

        GL.PopAttrib();
        GL.PopMatrix();      

        return true;
    }

    public void SwapBuffers(IntPtr hdc)
    {
        foreach (TargetOpt targetOpt in Settings.AsList)
        {
            for (int i = 0; i < targetOpt.Targets.Length; i++)
                targetOpt.Targets[i].IsValid = false;
            targetOpt.Index = 0;
        }
    }


    private void SetTarget(TargetOpt options, float offsetX = 0, float offsetY = 0, float offsetZ = 0)
    {
        try
        {
            int index = options.Index % 256;
            options.Targets[index].IsValid = true;
            GL.GetFloatv(PName.ProjectionMatrix, options.Targets[index].Projection);
            GL.GetFloatv(PName.ModelviewMatrix, options.Targets[index].Modelview);

            float[] m3 = new float[4];
            for (int i = 0; i < 4; ++i)
                m3[i] = options.Targets[index].Modelview[i] * offsetX + options.Targets[index].Modelview[i + 4] * offsetY + options.Targets[index].Modelview[i + 8] * offsetZ + options.Targets[index].Modelview[i + 12];

            Buffer.BlockCopy(m3, 0, options.Targets[index].Modelview, 12, sizeof(float) * m3.Length);

            options.Targets[index].DrawDuring(options);

            options.Index = index + 1;
        }
        catch (Exception ex) { Interop.MessageBox(0, ex.Message, "C#", 0); }
    }
}