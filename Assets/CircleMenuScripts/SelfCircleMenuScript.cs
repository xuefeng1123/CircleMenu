using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SelfCircleMenuScript : MonoBehaviour
{
    static public float t = 1f;          //动画移动时间
    static public Vector3 velocity = Vector3.zero;  //动画移动速度
    static public string operationItemSampleName = "PressableButtonHoloLens2Circular"; //菜单item的实例名
    static public string menuSampleName = "Menu";
    static public string circleMenuBgName = "MenuBg";
    static public string menuOptionsName = "MenuOptions";
    static public string menuLayerName = "Menu";
    public GameObject menu;
    public GameObject currObject;
    private LayerMask originObjectLayer;

    //for test
    static public List<Operation> operations = new List<Operation>
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
        menu = GameObject.Find(menuSampleName);//空物体
        menu.transform.position = GameObject.Find(operationItemSampleName).transform.position;
        //for test
        createCircleMenu(operations);
    }

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
                    hideCircleMenu();
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
            if (menu.activeSelf) hideCircleMenu();
            else CancelInvoke();
        }

    }

    public void createCircleMenu(List<Operation> operations)
    {
        //按照操作列表初始化操作item
        operations.ForEach(operation =>
        {
            GameObject operationObject = GameObject.Instantiate(GameObject.Find(operationItemSampleName));
            operationObject.transform.position = menu.transform.position;
            operationObject.GetComponent<Operation>().setOperation(operation);
            operationObject.transform.parent = menu.transform.Find(menuOptionsName).transform;//为menu中的options子项添加menu item
        });
        createMenuBackground();
        adjustLayout(menu.transform.Find(menuOptionsName).gameObject);//调整选项展开布局
        menu.SetActive(false);//暂时隐藏menu
    }
    private void createMenuBackground()
    {
        GameObject menuBg = menu.transform.Find(circleMenuBgName).gameObject;
        menuBg.transform.localPosition = Vector3.zero;
        menuBg.transform.localScale = ((radius * 2 + 0.01f) / menuBg.GetComponent<Renderer>().bounds.size.x) * (new Vector3(1, 1, 0));
    }

    public void showCircleMenu(GameObject originObject)
    {
        currObject = originObject;
        Invoke("updateCircleMenu", 2.0f);
    }

    void updateCircleMenu()
    {
        foreach (Transform item in menu.transform.Find(menuOptionsName).transform)
        {
            //将物体从中间平滑展开
            item.position = Vector3.Lerp(item.position, item.gameObject.GetComponent<Operation>().localPosition + menu.transform.position, t);
        }
        menu.transform.position = currObject.transform.position;
        menu.transform.rotation = currObject.transform.rotation;
        originObjectLayer = currObject.layer;
        currObject.layer = LayerMask.NameToLayer(menuLayerName);

        menu.SetActive(true);

    }

    public void hideCircleMenu()
    {

        foreach (Transform item in menu.transform)
        {
            //将物体从中间平滑收回
            item.position = Vector3.Lerp(item.position, currObject.transform.position, t);
        }
        menu.SetActive(false);
        currObject = null;
    }

    static public float radius = 0.02f;

    private void adjustLayout(GameObject menuOptions)
    {
        int index = 0;
        float angleUnit = 360.0f / menuOptions.transform.childCount;
        foreach (Transform operationItem in menuOptions.transform)
        {
            operationItem.gameObject.GetComponent<Operation>().localPosition = new Vector3(
                radius * Mathf.Sin(radianAngle(index * angleUnit)),
                radius * Mathf.Cos(radianAngle((index++) * angleUnit)),
                0);
            operationItem.gameObject.transform.localPosition = Vector3.zero;//初始时聚集在一起

        }

    }

    public float radianAngle(double angle)
    {
        return (float)(Mathf.PI * angle / 180.0);
    }

}
