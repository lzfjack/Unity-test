using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;

    private int winCheck = 0;

	// Use this for initialization
	void Start () {
        action = SSDirector.getInstance().currentScenceController as IUserAction;
	}
	
	// Update is called once per frame
	void Update () {
        action.shootArrow();
	}

    void OnGUI() {
        float width = Screen.width / 6;
        float height = Screen.height / 12;

        action.getScoreAndDisplay();
        action.getWindInfo();
    }
}
