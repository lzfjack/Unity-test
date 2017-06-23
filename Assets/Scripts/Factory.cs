using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    private List<GameObject> used = new List<GameObject>();//记录已使用的箭
    private List<GameObject> free = new List<GameObject>();//记录空余的箭
    
    //从空余列表中取箭，没有则创建
    public GameObject getArrow() {
        GameObject arrow = null;
        if (free.Count > 0) {
            arrow = free[free.Count - 1];
            //渲染箭
            arrow.SetActive(true);
            //重新设定箭的刚体等性质
            arrow.GetComponent<ArrowCollision>().Reset();
            arrow.AddComponent<Rigidbody>();
            used.Add(arrow);
            free.Remove(arrow);
            return arrow;
        }
        //没有空闲就从预制中创建
        arrow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/arrow"));
        arrow.GetComponent<ArrowCollision>().Reset();
        used.Add(arrow);
        return arrow;
    }

    //回收箭 
    public void setFree(GameObject arrow) {
        free.Add(arrow);
        arrow.SetActive(false);
        used.Remove(arrow);
        Destroy(arrow.GetComponent<Rigidbody>());
    }
}
