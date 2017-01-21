using UnityEngine;
using System.Collections;

public static class StringExtensions {

	public static string AppendPath(this string s, string component)
    {
        return s + "/" + component;
    }
}
