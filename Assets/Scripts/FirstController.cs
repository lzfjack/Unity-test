using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, IScenceController, IUserAction {

    public Factory arrowFactory;//箭工厂
    public ScoreRecorder scoreRecorder;// 记分员
    public PhysisActionManager physisManager;//物理运动管理器

    private GameObject target;//靶子
    private List<GameObject> arrows = new List<GameObject>();//记录当前场景有多少渲染的箭

    public enum WindDeirection { left, right };//风向
    private WindDeirection windDirection;//当前风向
    private Vector3 forceDirection;//记录了风力和风方向的信息
    private int force;

    void Awake() {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentScenceController = this;
        director.currentScenceController.LoadResources();
    }

    public void LoadResources() {
        Debug.Log("Loading Resources!!!");
        arrowFactory = Singleton<Factory>.Instance;;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        physisManager = GetComponent<PhysisActionManager>();
        target = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/target"));
    }

    // Use this for initialization
    void Start () {
        forceDirection = getForce();
    }
	
	// Update is called once per frame
	void Update () {
        checkAndSetFree();
    }

    public void Pause() {
        throw new NotImplementedException();
    }

    public void Resume() {
        throw new NotImplementedException();
    }

    //根据点击的地方射出箭
    public void shootArrow() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject arrow = arrowFactory.getArrow();
            arrow.transform.position = new Vector3(0, 4, -9);//初始出发位置
            arrow.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));//计算箭头朝向
            arrows.Add(arrow);
            physisManager.arrowfly(arrow, ray.direction, forceDirection);
            forceDirection = getForce();//下次射箭的风向 
        }
    }

    /*
        产生风力和分的方向
    */
    private Vector3 getForce() {
        System.Random rand = new System.Random((int)DateTime.Now.Ticks);
        windDirection = (WindDeirection)rand.Next(0, 2);
        force = rand.Next(0, 100);
        if (windDirection == WindDeirection.left) {
            forceDirection = new Vector3(-force, 0, 0);
        } else if (windDirection == WindDeirection.right) {
            forceDirection = new Vector3(force, 0, 0);
        }
        return forceDirection;
    }



    //检查当前哪些箭要回收，根据计时器
    private void checkAndSetFree() {
        GameObject arrow = null;
        foreach (GameObject a in arrows) {
            ArrowCollision ac = a.GetComponent<ArrowCollision>();
            if (ac.timer <= 0) {//超时
                arrow = a;
                break;
            }
        }
        if (arrow != null) {
            //找到超时的箭就回收
            arrowFactory.setFree(arrow);
            arrows.Remove(arrow);
            Debug.Log("Set Free in controller");
        }
    }

    //IUserAction
    #region

    public void getScoreAndDisplay() {
        GUIStyle frontStyle = new GUIStyle();
        frontStyle.normal.background = null;
        frontStyle.normal.textColor = Color.white;
        frontStyle.fontSize = 40;
        GUI.Label(new Rect(0, 0, 40, 40), "分数: " + scoreRecorder.score, frontStyle);
    }

    public void getWindInfo() {
        GUIStyle frontStyle = new GUIStyle();
        frontStyle.normal.background = null;
        frontStyle.normal.textColor = Color.white;
        frontStyle.fontSize = 40;
        GUI.Label(new Rect(200, 0, 40, 40), "风力: " + force, frontStyle);
        if (windDirection == WindDeirection.left)
            GUI.Label(new Rect(400, 0, 40, 40), "风向: 左" , frontStyle);
        else if (windDirection == WindDeirection.right)
            GUI.Label(new Rect(400, 0, 40, 40), "风向: 右", frontStyle);
    }

    #endregion
}
