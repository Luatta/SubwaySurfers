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
    // 目前的金币
    Dictionary<string, List<GameObject>> coinDict = new Dictionary<string, List<GameObject>>();
    // 金币
    public GameObject coin; 
    // 道路间隔距离
    public int roadDistance;
    
    // 按照顺序的障碍列表
    List<GameObject> newObjList1 = new List<GameObject>();
    List<GameObject> newObjList2 = new List<GameObject>();
    List<GameObject> newObjList3 = new List<GameObject>();
    // 金币列表
    List<GameObject> coinList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform road in roadList)
        {
            List<GameObject> objList = new List<GameObject>();
            List<GameObject> coinListt = new List<GameObject>();
            objDict.Add(road.name, objList);
            coinDict.Add(road.name, coinListt);
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
        foreach (GameObject item in coinDict[roadName])
        {
            Destroy(item);
        }
        coinDict[roadName].Clear();
        newObjList1.Clear();
        newObjList2.Clear();
        newObjList3.Clear();
        coinList.Clear();

        List<GameObject> prefabList = new List<GameObject>();
        List<Vector3> posList = new List<Vector3>();
        prefabList.Clear();
        posList.Clear();

        GameObject prefab1 = objPrefabList[Random.Range(0, objPrefabList.Count)];
        Vector3 pos1 = RandomBarrierPosition(index);
        prefabList.Add(prefab1);
        posList.Add(pos1);

        // 添加障碍物
        for(int i = 1; i < 12; i++)
        {
            GameObject prefab = objPrefabList[Random.Range(0, objPrefabList.Count)];
            Vector3 pos = RandomBarrierPosition(index);

            int j = 0;
            for (j = 0; j < posList.Count; j++)
            {
                if (prefab.name.StartsWith("Roadblock") && prefabList[j].name.StartsWith("Roadblock") &&
                    System.Math.Abs(pos.z - posList[j].z) < 10.0f) break;
                if (prefab.name.Length == 6)
                {
                    if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 7.0f) break;
                    if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 12.0f) break;
                    if (prefabList[j].name.Length == 10 && System.Math.Abs(pos.z - posList[j].z) < 3.0f) break;
                }
                if (prefab.name.Length == 7)
                {
                    if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 12.0f) break;
                    if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 15.0f) break;
                    if (prefabList[j].name.Length == 10 && System.Math.Abs(pos.z - posList[j].z) < 9.0f) break;
                }
                if (prefab.name.StartsWith("Roadblock") && prefabList[j].name.StartsWith("Train"))
                {
                    if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 3.0f) break;
                    if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 9.0f) break;
                }
            }

            if (j == posList.Count)
            {
                prefabList.Add(prefab);
                posList.Add(pos);
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < posList.Count; i++)
        {
            GameObject obj = Instantiate(prefabList[i], posList[i], Quaternion.identity);
            obj.tag = "Obstacle";
            if (obj.transform.position.x == -2.4f)
            {
                newObjList1.Add(obj);
            }
            else if (obj.transform.position.x == 0)
            {
                newObjList2.Add(obj);
            }
            else
            {
                newObjList3.Add(obj);
            }
        }
        SortDict(newObjList1);
        SortDict(newObjList2);
        SortDict(newObjList3);
        newObjList1.AddRange(newObjList2);
        newObjList1.AddRange(newObjList3);
        foreach (GameObject item in newObjList1)
        {
            objDict[roadName].Add(item);
        }
        
        // 添加金币
        for (int i = 1; i < objDict[roadName].Count; i++)
        {
            if (System.Math.Abs(objDict[roadName][i - 1].transform.position.x - objDict[roadName][i].transform.position.x) < 0.001)
            {
                if (objDict[roadName][i].transform.position.z - objDict[roadName][i - 1].transform.position.z > 15.0f)
                {
                    GameObject obj;
                    int n = 1;
                    do
                    {
                        obj = Instantiate(coin, new Vector3(objDict[roadName][i].transform.position.x, 0,
                            objDict[roadName][i - 1].transform.position.z + 3.0f * n), Quaternion.identity);
                        coinList.Add(obj);
                        n++;
                    } while (objDict[roadName][i].transform.position.z - obj.transform.position.z > 10.0f && n < 5);
                }
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    GameObject obj = Instantiate(coin,
                        new Vector3(objDict[roadName][i - 1].transform.position.x, 0,
                            objDict[roadName][i - 1].transform.position.z + 3.0f * (j + 1)), Quaternion.identity);
                    coinList.Add(obj); 
                }
            }
        }
        Debug.Log("1111");
        for (int i = 0; i < 4; i++)
        {
            int n = Random.Range(0, objDict[roadName].Count);
            if (objDict[roadName][n].name.StartsWith("Train"))
            {
                Debug.Log(n);
                for (int j = 0; j < 3; j++)
                {
                    // GameObject obj = Instantiate(coin,
                        // new Vector3(objDict[roadName][n].transform.position.x, 3,
                            // objDict[roadName][n].transform.position.z + 3.0f), Quaternion.identity);
                    // coinList.Add(obj);
                }
            }
            else
            {
                i--;
            }
        }
        
        foreach (GameObject coinItem in coinList)
        {
            coinDict[roadName].Add(coinItem);
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
        return targetPos;
    }

    // 障碍物排序
    public void SortDict(List<GameObject> newObjList)
    {
        for (int i = 0; i < newObjList.Count; i++)
        {
            for (int j = i; j < newObjList.Count; j++){
                if ( newObjList[i].transform.position.z > newObjList[j].transform.position.z )
                { 
                    GameObject temp = newObjList[i];
                    newObjList[i] = newObjList[j];
                    newObjList[j] = temp;
                }
            }  
        }
        
    }
}
