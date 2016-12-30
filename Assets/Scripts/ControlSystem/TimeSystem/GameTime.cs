using UnityEngine;
using UnityEngine.UI;



public class GameTime : MonoBehaviour {

    public Transform[] sun;
	public Transform[] moon;
    private Sun[] _sunScript;
	private Moon[] _moonScript;

    public float dayCycleInMinutes = 1.0f;

    private const float SECOND = 1.0f;
    private const float MINUTE = 60f * SECOND;
    private const float HOUR = 60f * MINUTE;
    private const float DAY = 24 * HOUR;
    private const float DEGREE_PER_SECOND = 360 / DAY;

    private float _degreeRotation;
    private float _timeOfDay;
    private float _dayCycleInSeconds;
	private float _sunBias = 0.0f;
	private float _moonBias = 180.0f;

	// Overwrite, register time pause event, time scale = 0 == pause
	private void OnEnable(){
		MessageManager.StartListening ("TimeScaleChange", TimeScale);
	}

	// Overwrite, unregister time pause event
	private void OnDisable(){
		MessageManager.StopListening ("TimeScaleChange", TimeScale);
	}

	// Convert the tiem scale
	public void TimeScale(){
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}

	// Initial the sun and moon list
    void Start () {
		_dayCycleInSeconds = dayCycleInMinutes * MINUTE;
        _sunScript = new Sun[sun.Length];
		_moonScript = new Moon[moon.Length];
        for (int count = 0; count < sun.Length; count++){
            Sun temp = sun[count].GetComponent<Sun>();
            if (temp == null)
            {
                Debug.LogWarning("Sun script not found, Adding it.");
                sun[count].gameObject.AddComponent<Sun>();
                temp = sun[count].GetComponent<Sun>();
            }
            _sunScript[count] = temp;
        }
		for (int count = 0; count < moon.Length; count++){
			Moon temp = moon[count].GetComponent<Moon>();
			if (temp == null){
				Debug.LogWarning("Moon script not found, Adding it.");
				moon[count].gameObject.AddComponent<Moon>();
				temp = moon[count].GetComponent<Moon>();
			}
			_moonScript[count] = temp;
		}
        _timeOfDay = 0;
		// Set moon rotation and sun rotation
        _degreeRotation = DEGREE_PER_SECOND * DAY / _dayCycleInSeconds;
		for(int count = 0; count < sun.Length; count++)
			sun[count].Rotate(new Vector3(_sunBias , 0, 0));
		for(int count = 0; count < moon.Length; count++)
			moon[count].Rotate(new Vector3(_moonBias , 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
        for(int count = 0; count < sun.Length; count++)
            sun[count].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
		for(int count = 0; count < moon.Length; count++)
			moon[count].Rotate(new Vector3(_degreeRotation , 0, 0) * Time.deltaTime);
        _timeOfDay += Time.deltaTime;
        //Debug.Log(_timeOfDay);
	}
}
