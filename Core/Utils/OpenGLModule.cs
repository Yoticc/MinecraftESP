namespace Core;
public static class OpenGLModule
{
    static OpenGLModule()
    {
        Handle = kernel32.GetModuleHandle("opengl32");
        if (Handle == 0)
            MessageBox($"Not found opengl32.dll module");

        wglSwapBuffers = kernel32.GetProcAddress(Handle, "wglSwapBuffers");
    }

    public static nint Handle;
    public static pointer wglSwapBuffers;
}