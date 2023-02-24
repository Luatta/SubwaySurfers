using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Control_GameManager : MonoBehaviour
{
    // 道路列表  用于道路切换
    public List<Transform> roadList = new List<Transform>();
    // 抵达点列表  用于判断玩家到达指定位置触发道路切换
    public List<Transform> arrivePosList = new List<Transform>();
    // 障碍物所有模型的列表  
    public List<GameObject> objPrefabList = new List<GameObject>();
    // 目前所有生成的障碍物
    Dictionary<string, List<GameObject>> objDict = new Dictionary<string, List<GameObject>>();
    // 目前所有生成的金币
    Dictionary<string, List<GameObject>> coinDict = new Dictionary<string, List<GameObject>>();
    // 金币模型
    public GameObject coin; 
    // 双倍金币模型
    public GameObject doublecoin; 
    // 道路间隔距离
    public int roadDistance;
    
    // 障碍列表  用于辅助生成场景中的障碍物
    List<GameObject> prefabList = new List<GameObject>();
    // 随机的障碍物坐标
    List<Vector3> posList = new List<Vector3>();
    // 金币列表  用于辅助生成场景中的金币
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
        InitRoad(0);
        InitRoad(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 切出新的道路
    public void ChangeRoad(Transform arrivePos)
    {
        int index = arrivePosList.IndexOf(arrivePos);
        if(index >= 0)
        {
            int lastIndex = index - 1;
            if (lastIndex < 0)
                lastIndex = roadList.Count - 1;
            // 移动道路
            roadList[index].position = roadList[lastIndex].position + new Vector3(0, 0, roadDistance);
 
            InitRoad(index);
        }
        else
        {
            Debug.LogError("arrivePos index is error");
            return;
        }
    }
 
    public void InitRoad(int index)
    {
        string roadName = roadList[index].name;
        // 清空已有障碍物和金币
        InitEmpty(roadName);
        
        // 添加障碍物
        CreateObstacle(index, roadName);
        
        // 添加金币
        CreateCoin(roadName);
    }

    // 清空所有gameobject
    private void InitEmpty(string roadName)
    {
        // 清空现有障碍物和金币
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
        // 清空辅助列表
        prefabList.Clear();
        posList.Clear();
        coinList.Clear();
    }
    
     // 障碍物创建
    private void CreateObstacle(int index, string roadName)
    {
        GameObject prefab;
        GameObject obj;
        Vector3 pos;
        int j;
        
        prefab = objPrefabList[Random.Range(0, objPrefabList.Count)];
        pos = RandomBarrierPosition(index);
        prefabList.Add(prefab);
        posList.Add(pos);
        
        for(int i = 1; i < 9; i++)
        {
            prefab = objPrefabList[Random.Range(0, objPrefabList.Count)];
            pos = RandomBarrierPosition(index);
        
            for (j = 0; j < posList.Count; j++)
            {
                if (prefab.name.StartsWith("Roadblock") && prefabList[j].name.StartsWith("Roadblock") &&
                    System.Math.Abs(pos.z - posList[j].z) < 12.0f) break;
                if (prefab.name.Length == 6)
                {
                    if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 8.0f) break;
                    if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 13.0f) break;
                    if (prefabList[j].name.Length == 8 && System.Math.Abs(pos.z - posList[j].z ) < 8.5f) break;
                    if (prefabList[j].name.Length == 10 && System.Math.Abs(pos.z - posList[j].z) < 3.0f) break;
                }
                if (prefab.name.Length == 7)
                {
                    if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 13.0f) break;
                    if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 15.0f) break;
                    if (prefabList[j].name.Length == 8 && System.Math.Abs(pos.z - posList[j].z) < 14.0f) break;
                    if (prefabList[j].name.Length == 10 && System.Math.Abs(pos.z - posList[j].z) < 9.0f) break;
                }
                if (prefab.name.Length == 8)
                {
                    if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 9.0f) break;
                    if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 14.0f) break;
                    if (prefabList[j].name.Length == 8 && System.Math.Abs(pos.z - posList[j].z) < 10.0f) break;
                    if (prefabList[j].name.Length == 10 && System.Math.Abs(pos.z - posList[j].z) < 8.0f) break;
                }
                if (prefab.name.StartsWith("Roadblock") && prefabList[j].name.StartsWith("Train"))
                {
                    if (prefabList[j].name.Length == 6 && System.Math.Abs(pos.z - posList[j].z) < 3.0f) break;
                    if (prefabList[j].name.Length == 7 && System.Math.Abs(pos.z - posList[j].z) < 9.0f) break;
                    if (prefabList[j].name.Length == 8 && System.Math.Abs(pos.z - posList[j].z) < 8.0f) break;
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
            obj = Instantiate(prefabList[i], posList[i], Quaternion.identity);
            objDict[roadName].Add(obj);
        }
        
        SortDict(objDict[roadName]);
    }
    
    //障碍物随机位置
    private Vector3 RandomBarrierPosition(int index)
    {
        float[] xChoice = {-2.4f, 0, 2.4f};
        int i = Random.Range(0,3);
        int m = (int)(roadList[index].position.z);
        int j = Random.Range(5 + m, 95 + m);
        Vector3 targetPos = new Vector3(xChoice[i],0,j);
        return targetPos;
    }

    // 障碍物排序
    private void SortDict(List<GameObject> objList)
    {
        float flag;
        for (int i = 0; i < objList.Count; i++)
        {
            for (int j = i; j < objList.Count; j++){
                if ( objList[i].transform.position.x > objList[j].transform.position.x )
                { 
                    (objList[i], objList[j]) = (objList[j], objList[i]);
                }
            }
        }
        for (int i = 0; i < objList.Count; i++)
        {
            flag = objList[i].transform.position.x;
            for (int j = i; j < objList.Count; j++){
                if (objList[i].transform.position.z > objList[j].transform.position.z &&
                    System.Math.Abs(objList[j].transform.position.x - flag) < 0.001f)
                {
                    (objList[i], objList[j]) = (objList[j], objList[i]);
                }
            }
        }
    }
    
    // 金币创建
    private void CreateCoin(string roadName)
    {
        GameObject obj;
        Vector3 pos;
        int n = 3;
        List<int> nList = new List<int>();
        nList.Clear();
        Vector3 coinPos;
        int len = 0;
        
        for (int i = 1; i < objDict[roadName].Count; i++)
        {
            if (System.Math.Abs(objDict[roadName][i - 1].transform.position.x - objDict[roadName][i].transform.position.x) < 0.001)
            {
                if (objDict[roadName][i].transform.position.z - objDict[roadName][i - 1].transform.position.z > 10.0f)
                {
                    do
                    {
                        pos = new Vector3(objDict[roadName][i].transform.position.x, 0,
                                objDict[roadName][i - 1].transform.position.z + 3.0f * n);
                        obj = Instantiate(coin, pos, Quaternion.identity);
                        coinList.Add(obj);
                        n++;
                    } while (objDict[roadName][i].transform.position.z - obj.transform.position.z > 3.0f && n < 6);
                    n = 3;
                }
            }
            else
            {
                for (int j = 0; j < 3; j++)
                {
                    pos = new Vector3(objDict[roadName][i - 1].transform.position.x, 0,
                            objDict[roadName][i - 1].transform.position.z + 3.0f * (j + 3));
                    obj = Instantiate(coin, pos, Quaternion.identity);
                    coinList.Add(obj); 
                }
                pos = new Vector3(objDict[roadName][i].transform.position.x, 0,
                    objDict[roadName][i - 1].transform.position.z + 3.0f * 6.2f);
                obj = Instantiate(doublecoin, pos, Quaternion.identity);
                coinList.Add(obj); 
            }
        }
        
        for (int i = 0; i < 5; i++)
        {
            n = Random.Range(0, objDict[roadName].Count);
            if (nList.Contains(n))
            {
                i--;
            }
            else
            {
                nList.Add(n);
            }
        }
        
        for (int j = 0; j < nList.Count; j++)
        {
            if (objDict[roadName][j].name.Length == 13) len = 3;
            if (objDict[roadName][j].name.Length == 14) len = 5;
            for (int p = 0; p < len; p++)
            {
                coinPos = new Vector3(objDict[roadName][j].transform.position.x, 3,
                    objDict[roadName][j].transform.position.z + 2.0f * (p - len / 2));
                obj = Instantiate(coin, coinPos, Quaternion.identity);
                coinList.Add(obj);
            }
            len = 0;
        }
        
        foreach (GameObject coinItem in coinList)
        {
            coinDict[roadName].Add(coinItem);
        }
    }
}
