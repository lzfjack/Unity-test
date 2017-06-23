using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour {

    public float timer { get; set; }//用于定时回收
    public ScoreRecorder scoreRecorder;

    public GameObject head;//箭头
    public GameObject body;//箭身 

    private int collisionNum;//用于限制碰撞检测的次数，初始化为1，只处理最先的碰撞

    void OnCollisionEnter(Collision collision) {
        if (collisionNum <= 0) return;//已碰撞过一次
        if (!collision.collider.gameObject.name.Contains("ring")) return;//撞到其他箭不处理
        //确认撞到的是哪一环
        Debug.Log(collision.collider.gameObject.name + " is hitted by " + collision.gameObject.name);
        //记录分数
        scoreRecorder.Record(collision);
        //撞到之后箭头隐藏
        head.SetActive(false);
        // 取消箭身的刚体，让箭留在靶上
        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        Destroy(rigidbody);
        //Destroy(body.GetComponent<Collider>());

        //最先碰到的靶，减一
        collisionNum--;
    }

    // Use this for initialization
    void Start () {
        timer = 4;//假定射出五秒后回收
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        collisionNum = 1;

    }
	
	// Update is called once per frame
	void Update () {
        timer -= 1 * Time.deltaTime;
	}

    //重设箭的状态
    public void Reset() {
        timer = 4;
        head.SetActive(true);
        //body.AddComponent<>();
        collisionNum = 1;
    }
}
