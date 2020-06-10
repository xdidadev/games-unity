﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSceneController : MonoBehaviour
{
    public List<Shape> gameShapes;

    private Coroutine countToNumberRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            countToNumberRoutine = StartCoroutine(CountToNumber(25000));
            // StartCoroutine("CountToNumber", 25000);
            // StartCoroutine(CountToNumber(25000));
            StartCoroutine(SetShapesBlue());
            Debug.Log("Keypress Complete");
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            StopCoroutine(countToNumberRoutine);
            //StopCoroutine("CountToNumber");
            // StopAllCoroutines(); 
        }
    }

    private IEnumerator CountToNumber(int NumberToCountTo)
    {
        for(int i = 0; i <= NumberToCountTo; i++)
        {
            Debug.Log(i);
            yield return null;
        } 
    }

    private IEnumerator SetShapesBlue()
    {
        Debug.Log("Start changing colors.");

        foreach (Shape shape in gameShapes)
        {
            shape.SetColor(Color.blue);
            yield return new WaitForSecondsRealtime(2);
            shape.SetColor(Color.white);
        }

        yield return new WaitForSecondsRealtime(1);
        Debug.Log("I just wasted a second");

        yield return StartCoroutine(SetShapesBlue());

    }

    private void SetShapesRed()
    {
        foreach(Shape shape in gameShapes)
        {
            shape.SetColor(Color.red); 
        } 
    }
}
