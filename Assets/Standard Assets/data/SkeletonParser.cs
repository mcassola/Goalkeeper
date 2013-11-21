using UnityEngine;
using System.Collections;


public class SkeletonParser{
	
	   private static float getPoint(string jointData){
            string[] a = jointData.Split(':');
			float b = float.Parse(a[1]);
			//Debug.Log(a[1]);
			//Debug.Log(b);
            return b;
		//return (float)System.ToDouble(a[1]);
        }
        
		public static SkeletonDataSource parse(string stringEntrada)
        {
            SkeletonDataSource skeletonDataSource = new SkeletonDataSource();
            string[] skeletons = stringEntrada.Split('/');
            for(int a=0; a < skeletons.Length -1;a++)
            {
                SkeletonsData skeletonsData = null;
                string[] skeletonJoints = skeletons[a].Split('#');
                
				for (int i = 0; i<skeletonJoints.Length-1;i++)
                //foreach (string skeletonJoint in skeletonJoints)
                {
                    if (i == 0) //este es el ID de skeleton
                    {
//						Debug.Log(i + " " + skeletonJoints[i]);
                        skeletonsData = new SkeletonsData( System.Int32.Parse(skeletonJoints[i]));
                        i++;
                    }
                    else
                    {
                        string[] jointDatas = skeletonJoints[i].Split(';');
                        float[] points = new float[3];
                        string jointName = "";
	                    int j = -1;    
						foreach (string jointData in jointDatas)
	                        {
                             
                            if (j == -1) //este es el Nombre de la Joint
                            {
                                jointName = jointData;
                                j++;
                            }
                            else
                            {
                                string[] myPoints = jointData.Split(' ');
                                foreach (string myPoint in myPoints)
                                {
                                    points[j] = getPoint(myPoint);
                                    j++;
                                }
                            }
                        }
                        Point p = new Point(points[0], points[1],points[2]);
                        JointSkeleton jS = new JointSkeleton(jointName, p);
                        skeletonsData.addJointSkeleton(jS);
                    }
                }
                skeletonDataSource.addSkeletonData(skeletonsData);
            }
		return skeletonDataSource;
	}

}
