using UnityEngine;

public class InputController : MonoBehaviour {

	public static InputController Instance
	{
		get
		{
			if (instance == null) instance = GameObject.FindObjectOfType<InputController>();
			return instance;
		}
		set
		{
			instance = value;
		}
	}

	static InputController instance;

	public delegate void InputEvent(Vector3 position);
	public event InputEvent OnTouchDown;
	public event InputEvent OnTouchUp;
	public event InputEvent OnTouchMoved;

    [Tooltip("Minimun drag movement in world space")]
    public float m_MinMoveDelta = 0.35f;

    Vector2 m_DownPosition;

	void Awake()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update ()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		CheckClicks();
		#else
		CheckTouches();
		#endif
	}

	void CheckClicks()
	{
		if(Input.GetMouseButtonDown(0))
		{
            var worldPos = Input.mousePosition.ToWorldPos();//ToWorldPos(Input.mousePosition);
            m_DownPosition = worldPos;
            if (OnTouchDown != null) OnTouchDown(worldPos);
		}
		else if(Input.GetMouseButton(0))
		{
            var worldPos = Input.mousePosition.ToWorldPos(); // ToWorldPos(Input.mousePosition);
            if(Vector2.Distance(m_DownPosition, worldPos) >= m_MinMoveDelta)
            {
                if (OnTouchMoved != null) OnTouchMoved(worldPos);
            }
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if (OnTouchUp != null) OnTouchUp(Input.mousePosition.ToWorldPos() /*ToWorldPos(Input.mousePosition)*/);
		}
	}

	void CheckTouches()
	{
		if (Input.touchCount == 0) return;

//		var touch = Input.GetTouch(0);

		foreach (var touch in Input.touches) {
		
			if (TouchPhase.Began == touch.phase)
			{
				var worldPos = touch.position.ToWorldPos(); //ToWorldPos(touch.position);
				m_DownPosition = worldPos;
				if (OnTouchDown != null) OnTouchDown(worldPos);
			}
			else if (TouchPhase.Moved == touch.phase)
			{
				var worldPos = touch.position.ToWorldPos();// ToWorldPos(touch.position);

				if (OnTouchMoved != null) OnTouchMoved(worldPos);
			}
			else if (TouchPhase.Stationary == touch.phase)
			{

			}
			else if(TouchPhase.Ended == touch.phase || TouchPhase.Canceled == touch.phase)
			{
				if (OnTouchUp != null) OnTouchUp(touch.position.ToWorldPos()/*ToWorldPos(touch.position)*/);
			}
		}


	}
}
