using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public List<Shape> gameShapes;
    public Dictionary<string, Shape> shapeDictionary;
    public Queue<Shape> shapeQueue;
    public Stack<Shape> shapeStake;

    void Start()
    {
        //Exemplo de Dictionary (T)
        shapeDictionary = new Dictionary<string, Shape>();
        foreach (Shape shape in gameShapes)
        {
            shapeDictionary.Add(shape.Name, shape);
        }

        //FIFO
        shapeQueue = new Queue<Shape>();
        shapeQueue.Enqueue(shapeDictionary["Triangle"]);
        shapeQueue.Enqueue(shapeDictionary["Square"]);
        shapeQueue.Enqueue(shapeDictionary["Octagon"]);
        shapeQueue.Enqueue(shapeDictionary["Circle"]);

        //LIFO
        shapeStake = new Stack<Shape>();
        shapeStake.Push(shapeDictionary["Triangle"]);
        shapeStake.Push(shapeDictionary["Square"]);
        shapeStake.Push(shapeDictionary["Octagon"]);
        shapeStake.Push(shapeDictionary["Circle"]);

    }

    private void SetRedByName(string shapeName)
    {
        shapeDictionary[shapeName].SetColor(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shape shape = shapeStake.Pop();
            shape.SetColor(Color.blue);
        }
    }

    private void DictionaryExemplo()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetRedByName("Square");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SetRedByName("Circle");
        }
    }

    private void DequeueExemplo() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shapeQueue.Count > 0)
            {
                Shape shape = shapeQueue.Dequeue();
                shape.SetColor(Color.blue);
            }
            else
            {
                Debug.Log("Queue vazia");
            }
        }
    }
}
