using UnityEngine;
using System.Collections;
using System;

public static class MonoBehaviourExtensions {

	public static T GetComponentInSibling<T> (this MonoBehaviour mb)
	{
		return mb.gameObject.transform.parent.GetComponentInChildren<T>();
	}

    public static void InvokeAfterSeconds(this MonoBehaviour mb, float seconds, Action action)
    {
        mb.StartCoroutine(YieldCor(seconds, action));
    }

    static IEnumerator YieldCor(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);

        action.Invoke();
    }

	/// <summary>
	/// Controls fading in or out, sending /float/ param 'alpha' with range [0,1].
	/// </summary>
	/// <param name="fadeIn">If set to <c>true</c> fade in.</param>
	/// <param name="duration">Duration of the fade</param>
	/// <param name="action">Action.</param>
	public static void Fade(this MonoBehaviour mb, bool fadeIn, float duration, Action<float> alpha)
	{
		mb.StartCoroutine(FadeCor(fadeIn, duration, alpha));
	}

	public static void FadePingPong(this MonoBehaviour mb,
		float initialAlpha, 
		float minAlpha,
		float maxAlpha, 
		float duration,
		float scale,
		Action<float> action,
		Action finish)
	{
		mb.StartCoroutine(PingPongAlpha(initialAlpha, minAlpha, maxAlpha, duration, scale, action, finish));
	}

	public static void FadePingPongProgressive(this MonoBehaviour mb,
		float initialAlpha, 
		float minAlpha,
		float maxAlpha, 
		float duration,
		Action<float> action,
		Action finish)
	{
		mb.StartCoroutine(PingPongAlphaProgressive(initialAlpha, minAlpha, maxAlpha, duration, action, finish));
	}

	static IEnumerator FadeCor(bool fadeIn, float duration, Action<float> action)
	{
		float alpha = 0;

		while(alpha <= 1)
		{
			alpha += Time.deltaTime/duration;
			float func = fadeIn ? alpha : 1 - alpha;
			action.Invoke(func);
			yield return null;
		}
	}

	static IEnumerator PingPongAlpha(float initialAlpha, 
		float minAlpha, 
		float maxAlpha,
		float duration,
		float scale, 
		Action<float> action, 
		Action finish)
	{
		for(float i=0; i< duration; i+= Time.deltaTime)
		{
			float alpha = Mathf.PingPong(initialAlpha, maxAlpha - minAlpha) + minAlpha;
			action.Invoke(alpha);

			initialAlpha += Time.deltaTime * scale;
			yield return null;
		}

		finish.Invoke();
	}

	static IEnumerator PingPongAlphaProgressive(float initialAlpha, 
		float minAlpha, 
		float maxAlpha,
		float duration, 
		Action<float> action, 
		Action finish)
	{
		float scale = 1f;
		var scaleStep = 3f/duration;
		for(float i=0; i< duration; i+= Time.deltaTime)
		{
			float alpha = Mathf.PingPong(initialAlpha, maxAlpha - minAlpha) + minAlpha;
			action.Invoke(alpha);

			initialAlpha += Time.deltaTime * scale;

			scale += Time.deltaTime * scaleStep;
			yield return null;
		}

		finish.Invoke();
	}

}
