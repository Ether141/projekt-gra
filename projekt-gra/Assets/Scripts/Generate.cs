using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public int Width;
    public int Height;
    public float BoxSize;
    public bool Save;

    private int[] map;

    void InitMap()
    {
        Debug.Log("Initialiizng map.");
        System.Array.Resize(ref map, Width * Height);
        for (int i = 0; i < map.Length; i++)
        {
            if (i < Width ||
                i % Width == 0 ||
                (i + 1) % Width == 0 ||
                i + Width >= Width * Height)
            {
                map[i] = 1;
            }
            else if (Random.Range(0, 5) == 0)
            {
                map[i] = 1;
            }
        }
    }

    void PrintMap()
    {
        string str = "";
        str += "Map preview (click to show):\n";
        for (int i = 0; i < map.Length; i++)
        {
            str += map[i].ToString();
            if ((i + 1) % Width == 0)
            {
                str += "\n";
            }
        }
        Debug.Log(str);
    }

    void BuildMap()
    {
        Debug.Log("Building world.");
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 0) continue;

            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            box.transform.position = new Vector3(i % Width * BoxSize, i / Width * BoxSize, 0);
            box.transform.localScale = new Vector3(BoxSize, BoxSize, BoxSize);
        }
    }

    void Start()
    {
        InitMap();
        PrintMap();
        BuildMap();
    }

    void Update()
    {
        
    }
}
