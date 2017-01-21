using UnityEngine;
using System;
using System.Collections;

public static class VectorExtensions {

    public static Vector3 ToWorldPos(this Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(position);
    }

    public static Vector2 ToWorldPos(this Vector2 position)
    {
        return Camera.main.ScreenToWorldPoint(position).ToVector2();
    }

    public static Vector3 ToViewportPos(this Vector3 position)
    {
        return Camera.main.ScreenToViewportPoint(position);
    }

    public static Vector2 ToViewportPos(this Vector2 position)
    {
        return Camera.main.ScreenToViewportPoint(position).ToVector2();
    }

    public static Vector2 ToVector2(this Vector3 v)
	{
		return new Vector2(v.x, v.y);
	}

	public static Vector3 ToVector3(this Vector2 v, float z = 0)
	{
		return new Vector3(v.x, v.y, z);
	}

	public static float RectDistance(this Vector2 v, Vector2 v2)
	{
		float xDist = Mathf.Abs(v.x - v2.x);
		float yDist = Mathf.Abs(v.y - v2.y);
		return xDist + yDist;
	}

	public static Vector3 SetAllToInt(this Vector3 v)
	{
		v.Set(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		return v;
	}

	public static Vector2 SetAllToInt(this Vector2 v)
	{
		v.Set(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		return v;
	}

	public static Quaternion ToRotation(this Vector2 v)
	{
		float rotationAngle = Vector2.Angle(v, Vector3.right);
		rotationAngle = v.y < 0 ? rotationAngle * -1 : rotationAngle;
		return Quaternion.AngleAxis(rotationAngle, new Vector3(0, 0, 1));
	}

    public static Quaternion ToRotation(this Vector3 v)
    {
        float rotationAngle = Vector2.Angle(v, Vector3.right);
        rotationAngle = v.y < 0 ? rotationAngle * -1 : rotationAngle;
        return Quaternion.AngleAxis(rotationAngle, new Vector3(0, 0, 1));
    }

    public static Vector3 Rotate(this Vector2 v, float angle)
	{
		Quaternion quat = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

		return quat * v;
	}

    public static Vector3 Rotate(this Vector3 v, Vector3 rotationAngle, float angle)
    {
        Quaternion quat = Quaternion.AngleAxis(angle, rotationAngle);

        return quat * v;
    }

	public static float ClockwiseAngle(this Vector2 from, Vector2 to)
	{
		float ang = Vector2.Angle(from, to);
		Vector3 cross = Vector3.Cross(from, to);

		if (cross.z > 0)
			ang = 360 - ang;

		return ang;
	}

	public static float ClockwiseAngle(this Vector2 from)
	{
		float ang = Vector2.Angle(from, Vector2.right);
		Vector3 cross = Vector3.Cross(from, Vector2.right);

		if (cross.z > 0)
			ang = 360 - ang;

		return ang;
	}

	public static Vector2[] Decompose(this Vector2 v)
	{
		var vX = Vector2.right * v.x;
		var vY = Vector2.up * v.y;

		return new Vector2[] { vX, vY };
	}

	public static Vector2 DominantComponent(this Vector2 v)
	{
		if(Mathf.Abs(v.x) > Mathf.Abs(v.y))
		{
			return v.x > 0 ? Vector2.right : Vector2.left;
		}
		if (Mathf.Abs(v.x) < Mathf.Abs(v.y))
		{
			return v.y > 0 ? Vector2.up : Vector2.down;
		}

		return Vector2.zero;
	}

	public static Vector2 AbsoluteValue(this Vector2 v)
	{
		v.x = Mathf.Abs(v.x);
		v.y = Mathf.Abs(v.y);

		return v;
	}

	public static Vector2 Flip(this Vector2 v)
	{
		v.x = -v.x;
		v.y = -v.y;
		return v;
	}

	public static Vector2 FlipY(this Vector2 v)
	{
		v.x = -v.x;
		return v;
	}

	public static Vector2 Multiply(this Vector2 v, Vector2 v2)
	{
		v.x = v.x * v2.x;
		v.y = v.y * v2.y;

		return v;
	}

	public static Vector2 ClampWithVector(this Vector2 v, Vector2 clampVector)
	{
		v.x = Mathf.Clamp(v.x, -clampVector.x, clampVector.x);
		v.y = Mathf.Clamp(v.y, -clampVector.y, clampVector.y);

		return v;
	}
}