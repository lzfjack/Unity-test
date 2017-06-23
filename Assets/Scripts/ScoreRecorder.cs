using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreRecorder : MonoBehaviour{

    public int score = 0;

    public void Record(Collision collision) {
        int ring = getRing(collision.collider.gameObject.name);
        score += ring;
    }

    private int getRing(string name) {
        string ringstr = name[4] + "";
        int ring = int.Parse(ringstr);//靶环的命名方式为ringX（x为第几环 ）
        return ring;
    }

    public void Reset() {
        score = 0;
    }
}
