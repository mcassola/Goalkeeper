using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class SkeletonsData {


        private int skeletonId;
        private List<JointSkeleton> skeletonJoint;

        public SkeletonsData(int id) {
            this.skeletonId = id;
            skeletonJoint = new List<JointSkeleton>();
        }

        public void addJointSkeleton(JointSkeleton joint) {
            skeletonJoint.Add(joint);        
        }
	
		public List<JointSkeleton> getSkeletonJoint(){ return skeletonJoint;}
	
        public override string ToString()
        {
           string data = skeletonId + "#";
           foreach (JointSkeleton joint in skeletonJoint) { 
                data += joint.getJointName() +";" + joint.getJointPosition().ToString() +"#" ;
           }
           return data;
        }
}
