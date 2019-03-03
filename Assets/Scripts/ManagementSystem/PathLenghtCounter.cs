using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management
{
    public class PathLenghtCounter
    {
        float drivenPath = 0;
        int currentLine = 0;
        float lineLenght;
        public int CurrentLine { get { return currentLine; } }
        
        public void Init(SceneData sceneData)
        {
            lineLenght = sceneData.LineLength;
        }
        
        public void CountLenght(SceneData sceneData, float deltaTime)
        {
            drivenPath += sceneData.TrainSpeed * deltaTime;
            currentLine = (int) (drivenPath / lineLenght); //get only whole part of number.
            sceneData.CurrentLine = currentLine;
        }

    }
}

