using System.Linq;

namespace SafeAreaRect.Screens
{
    public static class iOSScreenTypeResolver
    {
        public static iOSScreen Resolve()
        {
            return iOSScreenType.Values.FirstOrDefault(t => t.IsCurrentScreen()) ?? iOSScreenType.Unknown();
        }
    }
}