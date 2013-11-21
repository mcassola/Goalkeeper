using UnityEngine;
using System.Collections;

public class JointSkeleton {
	/*
	using System;
using UnityEngine;
using System.Collections;

using System.Linq;
using System.Text;*/


    private string jointName;
    private Point jointPosition;

    public JointSkeleton(string name, Point p) {
        this.jointName = name;
        this.jointPosition = p;        
    }

    public string getJointName() { return jointName; }
    public Point getJointPosition() { return jointPosition; }


}
