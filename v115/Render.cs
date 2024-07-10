namespace v115;
public unsafe class Render : DefaultRender
{
    List<float[]> arrays = [];
    public bool DrawArrays(Mode mode, int first, int count)
    {
        if (mode == Mode.Quads)
        {
            for (var i = 0; i < arrays.Count; i++)
            {
                var values = arrays[i];

                GL.PushAttrib(0x000fffff);

                GL.Disable(Cap.Texture2D);
                GL.Disable(Cap.CullFace);
                GL.Disable(Cap.Lighting);
                GL.Disable(Cap.DepthTest);

                GL.Enable(Cap.LineSmooth);

                GL.Enable(Cap.Blend);
                GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

                GL.LineWidth(3);
                GL.Color3f(1, 0, 0);

                GL.Begin(Mode.Lines);
                for (var o = 0; o < values.Length; o += 3)
                    GL.Vertex3f(values[o], values[o + 1], values[o + 2]);
                GL.End();

                GL.PopAttrib();
            }

            arrays.Clear();
        }

        return true;
    }

    public bool VertexPointer(int size, TexType type, int stride, pointer pointer)
    {
        if (type == TexType.Float)
        {
            if (pointer > 0x10000)
            {
                float[] values = new float[size * 3];

                for (var i = 0; i < values.Length; i += 3)
                {
                    var start = (float*)(pointer + stride * i);
                    values[i] = start[0];
                    values[i + 1] = start[1];
                    values[i + 2] = start[2];
                }
                
                arrays.Add(values);
            }
        }

        return true;
    }

    public new bool Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return true;

        nowInInventory = true;

        GL.PushAttrib(0x000fffff);
        GL.PushMatrix();

        GL.Disable(Cap.Texture2D);
        GL.Disable(Cap.CullFace);
        GL.Disable(Cap.Lighting);
        GL.Disable(Cap.DepthTest);

        GL.Enable(Cap.LineSmooth);

        GL.Enable(Cap.Blend);
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

        Draw(Config->PlayerESPEnabled, Targets.Player);
        Draw(Config->ChestESPEnabled, Targets.Chest, Targets.LargeChest);
        Draw(Config->ItemESPEnabled, Targets.Item);
        Draw(Config->SignESPEnabled, Targets.Sign);
        Draw(Targets.Other.Enabled, Targets.Other);

        GL.PopAttrib();
        GL.PopMatrix();

        return true;

        void Draw(bool enabled, params TargetOpt[] targetOpts)
        {
            if (enabled)
                foreach (var targetOpt in targetOpts)
                    foreach (var target in targetOpt.Targets)
                        target.DrawOver(targetOpt);
        }
    }
}