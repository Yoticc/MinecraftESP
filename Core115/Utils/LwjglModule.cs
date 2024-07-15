namespace Core;
public class LwjglModule
{
    static LwjglModule()
    {
        Handle = kernel32.GetModuleHandle("lwjgl_opengl");
        if (Handle == 0)
            MessageBox($"Not found lwjgl_opengl.dll module. Looks like you choose the wrong version of Minecraft");

        glDrawArrays = kernel32.GetProcAddress(Handle, "Java_org_lwjgl_opengl_GL11C_glDrawArrays");
        glDrawElements = kernel32.GetProcAddress(Handle, "Java_org_lwjgl_opengl_GL11C_nglDrawElements");
        glVertexPointer = kernel32.GetProcAddress(Handle, "Java_org_lwjgl_opengl_GL11_nglVertexPointer");
        glBufferData = kernel32.GetProcAddress(Handle, "Java_org_lwjgl_opengl_GL15C_nglBufferData__IJJI");
    }

    public static nint Handle;
    public static pointer glDrawArrays;
    public static pointer glDrawElements;
    public static pointer glVertexPointer;
    public static pointer glBufferData;
}