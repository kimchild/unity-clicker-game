using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text TextGold;
	public Camera MainCamera;
	public GameObject EffectSpark;
	public AudioClip SFXClick;

	// Use this for initialization
	void Start () {
		TextGold.text = DataController.Instance.Gold.ToString();
		StartCoroutine (StartCollectGold ());
	}

	//coroutine
	IEnumerator StartCollectGold(){
	    
		while (true) {
			
			//1seconds sleep
			yield return new WaitForSecondsRealtime (1f);
			DataController.Instance.Gold += DataController.Instance.GoldPerSec;
			TextGold.text = DataController.Instance.Gold.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		//left click
		if (Input.GetMouseButtonDown (0)) {

			//마우스 클릭시 골드증가
			DataController.Instance.Gold += DataController.Instance.GoldPerSec;
			TextGold.text = DataController.Instance.Gold.ToString ();

			//클릭한 위치에 카메라 이펙트 효과
			Ray ray = MainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100f)) {
				Debug.Log (hit.point);
				Debug.DrawLine (ray.origin, hit.point, Color.red);
				Instantiate (EffectSpark, hit.point, EffectSpark.transform.rotation);

				//클릭 사운드
				MainCamera.gameObject.GetComponent<AudioSource> ().PlayOneShot(SFXClick);
			}
		}

	}


	public void UpgradeCollectGold(){
	    
	}

}
