using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
     float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) return;
        float cycles = Time.time / period; // Постоянно растущее время

        const float tau = Mathf.PI * 2; // Постоянное значение 6,283
        float rawSineWave = Mathf.Sin(cycles * tau); // от -1 к 1

        movementFactor = (rawSineWave + 1f) / 2f; // Пересчет,чтобы было от 0 к 1


        Vector3 offset = movementVector * movementFactor; 
        transform.position = startingPosition + offset;
    }
}
