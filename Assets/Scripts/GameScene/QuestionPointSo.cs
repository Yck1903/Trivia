using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionPoint", menuName = "Question Point")]

public class QuestionPointSo : ScriptableObject
{
    public int CorrectAnswerPoint;
    public int WrongAnswerPoint;
    public int TimeIsUpPoint;
}