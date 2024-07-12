namespace Core.Utils;
public class LwjglModule
{
    static LwjglModule()
    {
        Handle = kernel32.GetModuleHandle("lwjgl_opengl");
        if (Handle == 0)
            MessageBox($"Not found lwjgl_opengl.dll module. Looks like you choose the wrong version of Minecraft");

        glDrawArrays = kernel32.GetProcAddress(Handle, "Java_org_lwjgl_opengl_GL11C_glDrawArrays");
        glVertexPointer = kernel32.GetProcAddress(Handle, "Java_org_lwjgl_opengl_GL11_nglVertexPointer");
    }

    public static nint Handle;
    public static pointer glDrawArrays;
    public static pointer glVertexPointer;
}