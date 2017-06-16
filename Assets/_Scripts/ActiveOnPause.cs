using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnPause : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(UIManager.Instance.IsPause);
	}
	
}
