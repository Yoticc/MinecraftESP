using ESP.Structs;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using Render = ESP.Render;
using RH = ESP.RenderHook;
using RU = ESP.RenderUtils;

namespace ESP;
public unsafe class Render
{
    public bool NoLight, NoBackground, NoFog, AntiCullFace, WorldChams, CaveViewer, RainbowText;
    public bool Enable(ref Cap cap)
    {
        if (cap == Cap.Lighting && NoLight) { }
        else if (cap == Cap.Fog && NoFog) { }
        else if (cap == Cap.CullFace && AntiCullFace) { }
        else if (cap == Cap.DepthTest && CaveViewer) { }
        else return true;
        
        return false;
    }

    public bool Disable(ref Cap cap)
    {
        if (cap == Cap.Blend && WorldChams) { }
        else if (cap == Cap.Texture2D && NoBackground) { }
        else return true;

        return false;
    }

    public bool Begin(ref Mode mode)
    {
        if (mode == Mode.TrianglesStript && RainbowText)
        {
            (float r, float g, float b) color = RGB.GetF();
            GL.Color3f(color.r, color.g, color.b);
        }

        return true;
    }

    public bool TranslateF(ref float x, ref float y, ref float z)
    {
        if (x == 0.5f && y == 0.4375f && z == 0.9375f)
        {
            glTargets.Add(GetTarget(0, 0.0625f, -0.4375f));
        }

        return true;
    }


    public bool ScaleF(ref float x, ref float y, ref float z)
    {


        return true;
    }


    private List<GLTarget> glTargets = new List<GLTarget>();
    private AABB localPlayerAABB = new AABB(-0.3, 0, -0.3, 0.3, 1.8, 0.3);
    public bool Ortho(ref double left, ref double right, ref double bottom, ref double top, ref double zNear, ref double zFar)
    {
        GL.PushAttrib(0x000fffff);
        GL.PushMatrix();

        RH.Disable(Cap.Texture2D);
        RH.Disable(Cap.CullFace);
        RH.Disable(Cap.Lighting);
        RH.Disable(Cap.DepthTest);

        RH.Enable(Cap.LineSmooth);

        RH.Enable(Cap.Blend);
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);


        GL.Color3f(0.5f, 0, 0.5f);
        GL.LineWidth(1f);

        /*
        foreach (GLTarget target in glTargets)
        {
            target.Draw();
        }
        */


        GL.PopAttrib();
        GL.PopMatrix();

        glTargets.Clear();

        return true;
    }

    private GLTarget GetTarget(float offsetX = 0, float offsetY = 0, float offsetZ = 0)
    {
        GLTarget target = new GLTarget()
        {
            Projection = new float[16],
            Modelview = new float[16]
        };

        GL.GetFloatv(PName.ProjectionMatrix, target.Projection);

        GL.GetFloatv(PName.ModelviewMatrix, target.Modelview);

        float[] m3 = new float[3];
        for (int i = 0; i < 4; ++i)
            m3[i] = target.Modelview[i] * offsetX + target.Modelview[i + 4] * offsetY + target.Modelview[i + 8] * offsetZ + target.Modelview[i + 12];

        Buffer.BlockCopy(m3, 0, target.Modelview, 12, sizeof(float) * m3.Length);

        return target;
    }
}