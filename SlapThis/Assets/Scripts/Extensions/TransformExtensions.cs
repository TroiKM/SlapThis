using UnityEngine;
using System.Collections;

public static class TransformExtensions {

	public static Transform FindDeepChild(this Transform t, string name)
	{
		int childs = t.childCount;
		for(int i=0; i<childs; i++)
		{
			var c = t.GetChild(i);
			if(c.name == name) return c;

			var dc = c.FindDeepChild(name);
			if(dc != null) return dc;
		}

		return null;
	}

	public static T FindDeepChildComponent<T>(this Transform t, string name)
	{
		var child = t.FindDeepChild(name);
		if(child != null)
		{
			return child.GetComponent<T>();
		}
		return default(T);
	}
}
