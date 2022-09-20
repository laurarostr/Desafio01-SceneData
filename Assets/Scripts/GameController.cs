using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{
    public string SaveFilePath { get => $"{Application.persistentDataPath}/Save.json"; }
    public List<SceneObjectsDTO> sceneDTO = new List<SceneObjectsDTO>();
    public List<GameObjectClass> goList = new List<GameObjectClass>();
    public List<PrimitiveClass> prList = new List<PrimitiveClass>();
    public List<PrefabClass> pfList = new List<PrefabClass>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScenerySaveToJson();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadSceneSave();
            Debug.Log(Application.persistentDataPath);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateEntity();
        }
    }

    private void CreateEntity() 
    {
        SceneObjectsDTO saveDTO = new SceneObjectsDTO();

        for (int i = 0; i < 1; i++)
        {
            var R = UnityEngine.Random.Range(0f, 1f);
            var G = UnityEngine.Random.Range(0f, 1f);
            var B = UnityEngine.Random.Range(0f, 1f);
            var color_go = new Color(R, G, B);

            var R1 = UnityEngine.Random.Range(0f, 1f);
            var G1 = UnityEngine.Random.Range(0f, 1f);
            var B1 = UnityEngine.Random.Range(0f, 1f);
            var color_pr = new Color(R1, G1, B1);

            var R2 = UnityEngine.Random.Range(0f, 1f);
            var G2 = UnityEngine.Random.Range(0f, 1f);
            var B2 = UnityEngine.Random.Range(0f, 1f);
            var color_pf = new Color(R2, G2, B2);

            Vector3 positionGO = new Vector3(UnityEngine.Random.Range(-8.0f, 8.0f), UnityEngine.Random.Range(-3.0f, 5.0f), 0);
            Vector3 positionPR = new Vector3(UnityEngine.Random.Range(-8.0f, 8.0f), UnityEngine.Random.Range(-3.0f, 5.0f), 0);
            Vector3 positionPF = new Vector3(UnityEngine.Random.Range(-8.0f, 8.0f), UnityEngine.Random.Range(-3.0f, 5.0f), 0);
            Quaternion rotationGO = new Quaternion(UnityEngine.Random.Range(0f, 4.0f), UnityEngine.Random.Range(0f, 2.0f), 0, 0);
            Quaternion rotationPR = new Quaternion(UnityEngine.Random.Range(0f, 4.0f), UnityEngine.Random.Range(0f, 2.0f), 0, 0);
            Quaternion rotationPF = new Quaternion(UnityEngine.Random.Range(0f, 4.0f), UnityEngine.Random.Range(0f, 2.0f), 0, 0);
            Vector3 scaleGO = new Vector3(UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f));
            Vector3 scalePR = new Vector3(UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f));
            Vector3 scalePF = new Vector3(UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f));

            GameObjectClass go = new(ObjectType.GameObject, Collider.Collider, color_go, positionGO, rotationGO, scaleGO);
            PrimitiveClass pr = new(ObjectType.Primitive, Collider.Trigger, color_pr, positionPR, rotationPR, scalePR);
            PrefabClass pf = new(ObjectType.Prefab, Collider.Collider, color_pf, positionPF, rotationPF, scalePF);

            goList.Add(go);
            prList.Add(pr);
            pfList.Add(pf);
        }
    }

    private void ScenerySaveToJson()
    {
        foreach (GameObjectClass item in goList)
        {
            sceneDTO.Add(item.GetSavedData());
        }
        foreach (PrimitiveClass item in prList)
        {
            sceneDTO.Add(item.GetSavedData());
        }
        foreach (PrefabClass item in pfList)
        {
            sceneDTO.Add(item.GetSavedData());
        }

        var saves = new ScenerySave()
        {
            Saves = sceneDTO.ToArray()
        };
        var json = JsonUtility.ToJson(saves, true);
        Debug.Log(json);

        Debug.Log(SaveFilePath);
        File.WriteAllText(SaveFilePath, json);
    }

    private void LoadSceneSave() 
    {
        var jsonText = File.ReadAllText(SaveFilePath);
        Debug.Log(jsonText);

        var saveData = JsonUtility.FromJson<ScenerySave>(jsonText);
        sceneDTO = new List<SceneObjectsDTO>();
        sceneDTO.AddRange(saveData.Saves);

        int aux = 0;

        foreach (var item in sceneDTO)
        {
            if (sceneDTO[aux].objectType == ObjectType.GameObject)
            {
                GameObjectClass go = new GameObjectClass(
                sceneDTO[aux].objectType,
                sceneDTO[aux].colliders,
                sceneDTO[aux].color,
                sceneDTO[aux].position,
                sceneDTO[aux].rotation,
                sceneDTO[aux].scale
                );
                goList.Add(go);
            }

            else if (sceneDTO[aux].objectType == ObjectType.Primitive)
            {
                PrimitiveClass pr = new PrimitiveClass(
                sceneDTO[aux].objectType,
                sceneDTO[aux].colliders,
                sceneDTO[aux].color,
                sceneDTO[aux].position,
                sceneDTO[aux].rotation,
                sceneDTO[aux].scale
                );
                prList.Add(pr);
            }

            else if (sceneDTO[aux].objectType == ObjectType.Prefab)
            {
                PrefabClass pf = new PrefabClass(
                sceneDTO[aux].objectType,
                sceneDTO[aux].colliders,
                sceneDTO[aux].color,
                sceneDTO[aux].position,
                sceneDTO[aux].rotation,
                sceneDTO[aux].scale
                );
                pfList.Add(pf);
            }

            aux++;
        }
    }
}
