using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//123
//405
//678

public class StageCreater : MonoBehaviour {
    [SerializeField] List<GameObject> tileList;

    void Start() {
        CreateStage(StageLevel.stage01);
    }

    public void CreateStage(int[,] stageDataList) {
        var height = stageDataList.GetLength(0);
        var width = stageDataList.GetLength(1);
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (stageDataList[y, x] == -1) continue;
                GenerateTile (stageDataList[y, x], new Vector2(x, -y));
            }
        }
    }

    private void GenerateTile(int tileType, Vector2 pos) {
        var obj = (GameObject)Instantiate (tileList[tileType], pos, Quaternion.identity);
        obj.transform.SetParent(transform);
    }

}
