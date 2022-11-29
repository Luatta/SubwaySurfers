using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Control_GameManager : MonoBehaviour
{
    // 道路列表
    public List<Transform> roadList = new List<Transform>();
    // 抵达点列表
    public List<Transform> arrivePosList = new List<Transform>();
    // 障碍物列表
    public List<GameObject> objPrefabList = new List<GameObject>();
    // 目前的障碍物
    Dictionary<string, List<GameObject>> objDict = new Dictionary<string, List<GameObject>>();
    // 道路间隔距离
    public int roadDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform road in roadList)
        {
            List<GameObject> objList = new List<GameObject>();
            objDict.Add(road.name, objList);
        }
        initRoad(0);
        initRoad(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 切出新的道路
    public void changeRoad(Transform arrivePos)
    {
        int index = arrivePosList.IndexOf(arrivePos);
        if(index >= 0)
        {
            int lastIndex = index - 1;
            if (lastIndex < 0)
                lastIndex = roadList.Count - 1;
            // 移动道路
            roadList[index].position = roadList[lastIndex].position + new Vector3(0, 0, roadDistance);
 
            initRoad(index);
        }
        else
        {
            Debug.LogError("arrivePos index is error");
            return;
        }
    }
 
    void initRoad(int index)
    {
        string roadName = roadList[index].name;
        // 清空已有障碍物
        foreach(GameObject obj in objDict[roadName])
        {
            Destroy(obj);
        }
        
        objDict[roadName].Clear();

        List<GameObject> prefabList = new List<GameObject>();
        List<Vector3> posList = new List<Vector3>();
        prefabList.Clear();
        posList.Clear();
        bool flag = true;

        GameObject prefab1 = objPrefabList[Random.Range(0, objPrefabList.Count)];
        Vector3 pos1 = RandomBarrierPosition(index);
        prefabList.Add(prefab1);
        posList.Add(pos1);

        // 添加障碍物
        for(int i = 1; i < 13; i++)
        {
            GameObject prefab = objPrefabList[Random.Range(0, objPrefabList.Count)];
            Vector3 pos = RandomBarrierPosition(index);

            for (int j = 0; j < posList.Count; j++)
            {
                if (prefab.name.StartsWith("Roadblock") && prefabList[j].name.StartsWith("Roadblock") &&
                    System.Math.Abs(pos.z - posList[j].z) < 10.0f)
                {
                    flag = false;
                    break;
                }

                if (prefab.name.StartsWith("Train") && prefabList[j].name.StartsWith("Train"))
                {
                    if (prefab.name.Length == 6)
                    {
                        if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 7.0f)
                        {
                            flag = false;
                            break;
                        }
                        else if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 10.0f)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (prefab.name.Length == 7)
                    {
                        if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 10.0f)
                        {
                            flag = false;
                            break;
                        }
                        else if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 13.0f)
                        {
                            flag = false;
                            break;
                        }
                    } 
                }

                if ((prefab.name.StartsWith("Roadblock") && prefabList[j].name.StartsWith("Train")) ||
                    (prefab.name.StartsWith("Train") && prefabList[j].name.StartsWith("Roadblock")))
                {
                    if ((prefab.name.Length == 6 || prefabList[j].name.Length == 6) &&
                        System.Math.Abs(pos.z - posList[j].z) < 3.0f)
                    {
                        flag = false;
                        break;
                    }
                    else if ((prefab.name.Length == 7 || prefabList[j].name.Length == 7) &&
                             System.Math.Abs(pos.z - posList[j].z) < 6.0f)
                    {
                        flag = false;
                        break;
                    }
                }
            }

            if (flag)
            {
                prefabList.Add(prefab);
                posList.Add(pos);
            }
            else
            {
                i--;
                flag = true;
            }
        }

        for (int i = 0; i < posList.Count; i++)
        {
            GameObject obj = Instantiate(prefabList[i], posList[i], Quaternion.identity);
            obj.tag = "Obstacle";
            objDict[roadName].Add(obj);
        }
    }
    
    //障碍物随机位置
    public Vector3 RandomBarrierPosition(int index)
    {
        float[] xChoice = {-2.4f, 0, 2.4f};
        int i = Random.Range(0,3);
        int m = (int)(roadList[index].position.z);
        int j = Random.Range(5 + m, 95 + m);
        Vector3 targetPos = new Vector3(xChoice[i],0,j);
        // Debug.Log(targetPos);
        return targetPos;
    }
}
