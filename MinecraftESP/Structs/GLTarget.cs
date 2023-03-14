using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using RH = ESP.RenderHook;

namespace ESP.Structs;
public unsafe class GLTarget
{
    public float[] Projection;
    public float[] Modelview;

    public void Draw()
    {
        GL.MatrixMode(Matrix.Projection);
        GL.LoadMatrixf(Projection);

        GL.MatrixMode(Matrix.Modelview);
        GL.LoadMatrixf(Modelview);

        GL.LoadIdentity();

        RH.Begin(Mode.Lines);
        GL.Vertex3f(0, 0, -0.1f);
        GL.Vertex3f(Modelview[12], Modelview[13], Modelview[14]);
        GL.End();
    }
}