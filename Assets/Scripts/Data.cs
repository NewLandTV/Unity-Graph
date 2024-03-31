using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Data : MonoBehaviour
{
    [SerializeField]
    private Graph graph;

    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                graph.AddPointMousePosition();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save(EditorUtility.SaveFilePanel("Save Data", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Data.txt", "txt"));
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load(EditorUtility.OpenFilePanel("Load Data", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "txt"));
            }

            yield return null;
        }
    }

    // Write data to file
    private void Save(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        File.Create(path);

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            for (int i = 0; i < graph.PositionCount; i++)
            {
                streamWriter.WriteLine($"{graph.GetPositionByIndex(i).x}, {graph.GetPositionByIndex(i).y}");
            }
        }
    }

    // Read data from file and add points to screen
    private void Load(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("File not found!");

            return;
        }

        graph.Clear();

        using (StreamReader streamReader = new StreamReader(path))
        {
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                switch (line)
                {
                    case "LT":
                        graph.AddPoint(graph.leftTop);

                        break;
                    case "RT":
                        graph.AddPoint(graph.rightTop);

                        break;
                    case "LB":
                        graph.AddPoint(graph.leftBottom);

                        break;
                    case "RB":
                        graph.AddPoint(graph.rightBottom);

                        break;
                    default:
                        string[] position = line.Split(", ");

                        if (float.TryParse(position[0], out float x) && float.TryParse(position[1], out float y))
                        {
                            graph.AddPoint(x, y);
                        }

                        break;
                }
            }
        }
    }
}
