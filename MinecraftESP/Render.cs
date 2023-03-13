using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using Render = ESP.Render;

namespace ESP;
public unsafe class Render
{
    public List<Cap> blockedEnableCaps = new List<Cap>();
    public List<Cap> blockedDisableCaps = new List<Cap>();
    public List<Cap> extraEnableCaps = new List<Cap>();
    public List<Cap> extraDisableCaps = new List<Cap>();

    public bool Enable(ref Cap cap)
    {
        if (blockedEnableCaps.Contains(cap))
            return false;

        foreach (Cap exCap in extraEnableCaps)
            RenderHook.Enable(exCap);
        
        return true;
    }

    public bool Disable(ref Cap cap)
    {
        if (blockedDisableCaps.Contains(cap))
            return false;

        foreach (Cap exCap in extraDisableCaps)
            RenderHook.Disable(exCap);

        return true;
    }

    public bool Begin(ref Mode mode)
    {
        /*
        if (mode == Mode.TrianglesStript)
        {
            GL.Color4f(1, 0, 0, 0.7f);
        }
        */

        return true;
    }
}