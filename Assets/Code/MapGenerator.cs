using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] private Texture2D map;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private Transform[] parent;
    [SerializeField] private int height = 2;
    [SerializeField] private int xSize = 64;
    [SerializeField] private int zSize = 64;

    // Start is called before the first frame update
    void Start()
    {
        Color[] color = map.GetPixels();
        int index = 0;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                if (color[index].Equals(Color.black)) Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity, parent[0]);
                else if (color[index].Equals(Color.white))
                {
                    Instantiate(ground, new Vector3(i, 0, j), Quaternion.identity, parent[1]);
                    Instantiate(ceiling, new Vector3(i, height, j), transform.rotation, parent[2]);
                }
                /*if (color[index].Equals(Color.white))
                {
                    Instantiate(ground, new Vector3(i, 0, j), Quaternion.identity, parent[1]);
                    Instantiate(ceiling, new Vector3(i, height, j), transform.rotation, parent[2]);
                }
                else Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity, parent[0]);*/
                index++;
            }
        }
    }
}
