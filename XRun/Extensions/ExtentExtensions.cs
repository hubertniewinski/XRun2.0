namespace XRun.Extensions;

public static class ExtentExtensions
{
    public static void AddToExtent<T>(this List<T> extent, T value)
    {
        if (extent.Contains(value))
        {
            return;
        }
        
        extent.Add(value);
    }
    
    public static void RemoveFromExtent<T>(this List<T> extent, T value)
    {
        if (!extent.Contains(value))
        {
            return;
        }
        
        extent.Remove(value);
    }
}