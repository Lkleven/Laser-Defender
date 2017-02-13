using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip[] backGroundMusic;
	private AudioClip selectedAudio;

	private AudioSource source;

	private Slider musicSlider;
	//private Toggle musicToggle;
	private bool mute = false;
	static MusicPlayer instance = null;
	private float prevVolume = 0.0f;


	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		source = gameObject.GetComponent<AudioSource> ();
		if (backGroundMusic != null) {
			source.clip = backGroundMusic[0];
			PlayMusic ();
		}
		musicSlider = GameObject.Find ("VolumeSlider").GetComponent<Slider>();
		float volume = gameObject.GetComponent<AudioSource> ().volume;
		musicSlider.value = volume;

	}

	void PlayMusic (){
		source.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (!mute) {
			gameObject.GetComponent<AudioSource> ().volume = musicSlider.value;
		}
	}

	public void ToggleMusic(){
		if (mute) {
			gameObject.GetComponent<AudioSource> ().volume = prevVolume;
			mute = false;
		} else if (!mute) {
			prevVolume = gameObject.GetComponent<AudioSource>().volume;
			gameObject.GetComponent<AudioSource> ().volume = 0.0f;
			mute = true;
		}
	}

	public void SelectTrack(int trackNo){
		source.Stop ();
		source.clip = backGroundMusic [trackNo];
		PlayMusic ();
	}
}
