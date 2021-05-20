using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMenuScript : MonoBehaviour
{
    static public float itemSize = (float)(0.032 / 5);//每个格子的大小
    static public string listMenuItemName = "ListMenuItemSample";
    static public string listMenuName = "listMenu";
    static public string squarName = "32x32mm_Square";
    static public string quadName = "Quad";
    static public string buttonCollectionName = "ButtonCollection";
    static public string menuLayerName = "Menu";
    LayerMask originObjectLayer;
    public GameObject menu;
    public GameObject currObject;
    public List<Operation> operations;
    //for test
    static public List<Operation> operationsSample = new List<Operation>
    {
        new Operation("operation1", "icon1"),
        new Operation("operation2", "icon2"),
        new Operation("operation3", "icon3"),
        new Operation("operation4", "icon4"),
        new Operation("operation5", "icon5"),
        new Operation("operation6", "icon6"),
    };


    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find(listMenuName);
        createListMenu(operationsSample);
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //验证1s后是否仍然注视物体
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.transform.forward, out hit, 70.0f))
        {
            GameObject root = hit.collider.gameObject.transform.root.gameObject;//找到最顶层
            print(root.name);
            if (menu.activeSelf)//当菜单正在显示时，判断什么时候收回
            {
                if (root.layer != LayerMask.NameToLayer(ListMenuScript.menuLayerName))
                {
                    hideListMenu();
                }
            }
            else//当菜单已经隐藏时，判断什么时候出现
            {
                if (root != currObject)//当视线从原物体移开时
                {
                    CancelInvoke();
                }
            }

        }
        else
        {
            if (menu.activeSelf) hideListMenu();
            else CancelInvoke();
        }

    }

    void createListMenu(List<Operation> operations)
    {
        this.operations = operations;
        calculateLayout(operations);
        menu.SetActive(false);//先隐藏list
    }

    void calculateLayout(List<Operation> operations)
    {
        GameObject background = menu.transform.Find(squarName).Find(quadName).gameObject;//list背景
        background.transform.localScale = new Vector3(operations.Count * itemSize, background.transform.localScale.y, background.transform.localScale.z);//根据item数量调整背景的大小
        background.transform.position = menu.transform.Find(buttonCollectionName).position;//使得背景与按钮集合对齐
        Transform collection = menu.transform.Find(buttonCollectionName);
        //添加item
        operations.ForEach(operation =>
        {
            GameObject item = GameObject.Instantiate(GameObject.Find(listMenuItemName));
            item.transform.parent = collection;
        });
        collection.gameObject.GetComponent<GridObjectCollection>().UpdateCollection();//添加完成后更新
    }

    void removeOperations()
    {
        Transform collection = menu.transform.Find(buttonCollectionName);
        foreach (Transform child in collection)
        {
            child.parent = null;
            Destroy(child);
        }
        collection.gameObject.GetComponent<GridObjectCollection>().UpdateCollection();//清除完成后更新
    }

    public void updateListMenu()
    {
        //更改scale 适配各种item
        float objectWidth = currObject.GetComponent<MeshRenderer>().bounds.size.x;
        float sampleWidth = GameObject.Find(listMenuItemName).transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().bounds.size.x;
        menu.transform.localScale = (objectWidth / sampleWidth) * Vector3.one;

        //右移半个listItem和菜单本身的的宽度，保证不影响list本身的显示
        //menu.transform.position = currObject.transform.position + new Vector3(objectWidth / 2, 0, 0) + new Vector3(operations.Count * itemSize * menu.transform.localScale.x / 2, 0, 0);
        menu.transform.position = currObject.transform.position + new Vector3(objectWidth / 2 + menu.transform.Find(squarName).Find(quadName).gameObject.GetComponent<MeshRenderer>().bounds.size.x / 2, 0, 0);
        originObjectLayer = currObject.layer;//先记录下原本的layer
        currObject.layer = LayerMask.NameToLayer(menuLayerName);//更改layer

        menu.SetActive(true);
    }

    public void showListMenu(GameObject originObject)
    {
        currObject = originObject;

        Invoke("updateListMenu", 2.0f);//凝视1s后显示
    }

    public void hideListMenu()
    {
        menu.SetActive(false);
        currObject.layer = originObjectLayer;//改回原来的layer
    }
}
