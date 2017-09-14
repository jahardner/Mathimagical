using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradeText : MonoBehaviour {

	public static int score = 75;
    public Text txt;

    void Start ()
    {
        txt = GetComponent<Text>();
    } 

    // Update is called once per frame
    void Update () {
        string letterGrade;
		if (score >= 90)
        {
            letterGrade = "A";
        } else if (score >= 80)
        {
            letterGrade = "B";
        } else if (score >= 70)
        {
            letterGrade = "C";
        } else if (score >= 60)
        {
            letterGrade = "D";
        } else
        {
            letterGrade = "F";
        }
        txt.text = "Grade: " + letterGrade;
	}
}
