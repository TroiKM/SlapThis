using UnityEngine;

public static class ColorExtensions {

    public static Color NewColor(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static Color NewColor(this Color color)
    {
        return color.NewColor(color.a);
    }
}
