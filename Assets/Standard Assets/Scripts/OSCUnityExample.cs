using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Ventuz.OSC;
using System.Collections;

class OSCUnityExample : MonoBehaviour
{
   	private bool initialized = false;
	private UdpReader OscReader;
    private UdpWriter OscWriter;
	private static string[] oscArgs = new string[2];
    public string OSCPath = "/";
	private byte[] fullMessage;
   	private byte[] byteReceived;
	int currentIndex = 0;
    int total;
	private int count = 1;
	private bool newData = false;
    private string stringToEdit = "Hello World";
	//public TextMesh texto;
	private SkeletonDataSource skeleton;
	public Texture2D image;
	public List<GameObject> hands;
	public float Factor = 1.0f;
	public float Delta = 0.01f;
	private List<Transform> lastJoints;
	public float xCenterKinect,yCenterKinect,xMinUnity,xMaxUnity,yMinUnity,yMaxUnity,zUnity;
	private float xCenterUnity, yCenterUnity;
	public float xMinKinect = -0.46f, xMaxKinect = 0.46f, yMinKinect = -0.5f, yMaxKinect = 0.2f;
	public float deltaHands = 0.4f; 
	private Texture2D tex;
	
    void Start()
    {
		tex = new Texture2D(4,4);
		//tex = new Texture2D(16,16, TextureFormat.DXT1, false);
		xMinUnity = 7.896372f;
		xMaxUnity = 11.14758f;
		yMinUnity = 1.121581f;
		yMaxUnity = 2.856261f;
		zUnity = 9.992952f;
		
		xCenterUnity = (xMaxUnity - xMinUnity)/2 + xMinUnity;
		yCenterUnity = (yMaxUnity - yMinUnity)/2 + yMinUnity;// - hands[0].renderer.bounds.size.y/2;
		
		lastJoints = new List<Transform>();
		for(int i=0;i<hands.Count;i++){
		//	hands[i].transform.position = new Vector3(xCenterUnity,yCenterUnity,zUnity);
			Transform t = hands[i].transform;
			lastJoints.Add(t);		
		}		
		//hands[0].transform.Rotate(90,90,90);
	//	hands[1].transform.Rotate(90,270,90);
	//	Debug.Log(hands[0].transform.rotation);
        initialized = true;
      	OscReader = new UdpReader(3001);
        oscArgs[0] = "127.0.0.1";
        oscArgs[1] = "3001";
        OscWriter = new UdpWriter(oscArgs[0], 3002);
    }

    void Update()
    {
      //  Debug.Log(hands[0].transform.rotation);
		// Wait for initialization
        if (!initialized) return;
        	ParseMessages();
    }
	
/*	private static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;

        }*/
	
	 void ParseMessages()
    {
    
		// Loop until failure
       // if (bundle != null)
       // {           
	       //  while (true)
	     //   {
	            // Recieve message from Stack
		if (OscReader != null){
	            OscMessage message = OscReader.Receive();
	
	            // Return if there are no more messages available
	            if (message == null) return;
	            OscBundle bundle = message as OscBundle;
	            if (bundle == null) return;
			
	           // textBox1.Text += string.Format("{0}\r", "time = " + bundle.DateTime.Millisecond);
	            foreach (OscElement element in bundle.Elements)
	            {
	               // textBox1.Text += string.Format("{0}\r\n", element.Args[0]);
	
	                if (element.Address.Contains("Skeleton"))
	                {
						string hands = (string)element.Args[3];
//						Debug.Log(hands.ToString());
	            		string gesto = (string)element.Args[2];      
//						Debug.Log(gesto.ToString());
						
						if (gesto.Equals("Swipe Right")){
							unlockbar.swipeDetected = true;
						}
					
						string dato = (string)element.Args[1];
	                    string time = (string)element.Args[0];
					    stringToEdit+= string.Format("{0}\r\n",dato);
						skeleton = SkeletonParser.parse(dato);
						newData = true;
						//texto.text += string.Format("{0}\r\n",dato);
	                    //stringToEdit+=  string.Format("{0}\r\n",time + " vs " + DateTime.Now.Millisecond);
	                }
	                
	              	if (element.Address.Contains("/video/color"))
                    {
                       
						//int count = (int)element.Args[1];
                    	byte[] bytesReceived = (byte[])element.Args[4];//new byte[total];
             
 						//Memory stream to store the bitmap data.
					    System.IO.MemoryStream ms =  new System.IO.MemoryStream(bytesReceived);
					   
					    tex.LoadImage(ms.ToArray());
					    ms.Dispose();
						
				
					
                     }
				}
	         }
       // }
	}
	
	private bool canMove(Vector3 newPosition,int pos){
//		Debug.Log("pos " + lastJoints[pos].position);
		if ((Math.Abs(newPosition.x - lastJoints[pos].position.x) > Delta)&&
			(Math.Abs(newPosition.y - lastJoints[pos].position.y) > Delta) && 
			(Math.Abs(newPosition.z - lastJoints[pos].position.z) > Delta))
			return true;
		return false;
		
		
	}
	
	private void moveGloves(Vector3 rightHand,Vector3 leftHand,Vector3 shoulderCenter){
		
		
			
		float xRPosHandRight,yRPosHandRight,xRPosHandLeft,yRPosHandLeft;
		
		//handLeftX - shoulderCenterX --> valor negativo
		xRPosHandLeft = leftHand.x - shoulderCenter.x;
		
		if (xRPosHandLeft > xMaxKinect)
			xRPosHandLeft = xMaxKinect;
		if (xRPosHandLeft < xMinKinect)
			xRPosHandLeft = xMinKinect;
		
		
		yRPosHandLeft = leftHand.y - shoulderCenter.y;
		
		if (yRPosHandLeft > yMaxKinect)
			yRPosHandLeft = yMaxKinect;
		if (yRPosHandLeft < yMinKinect)
			yRPosHandLeft = yMinKinect;
		
		//Debug.Log("yRPosHandLeft " +yRPosHandLeft);
		
		xRPosHandRight = rightHand.x - shoulderCenter.x;		
		
		if (xRPosHandRight > xMaxKinect)
			xRPosHandRight = xMaxKinect;
		if (xRPosHandRight < xMinKinect)
			xRPosHandRight = xMinKinect;
		
		yRPosHandRight = rightHand.y - shoulderCenter.y;
		
		
		if (yRPosHandRight > yMaxKinect)
			yRPosHandRight = yMaxKinect;
		if (yRPosHandRight < yMinKinect)
			yRPosHandRight = yMinKinect;
		
		float xCentroKinect = (xMaxKinect - xMinKinect)/2 + xMinKinect;
	
		if (canMove(leftHand,0)){
			//xRealWorld
			float xRealWorld = (-xRPosHandLeft/xMaxKinect)*((xMaxUnity - xMinUnity)/2) +xCenterUnity;
			
			//yRealWorld
			float yRealWorld;
			
			if (yRPosHandLeft > 0)
				yRealWorld = (yRPosHandLeft/yMaxKinect)*((yMaxUnity - yMinUnity)/2) +yCenterUnity;
			else
			 	yRealWorld = (-yRPosHandLeft/yMinKinect)*((yMaxUnity - yMinUnity)/2) +yCenterUnity;
		 	
			if (!isHandOver()){
				hands[0].transform.position = new Vector3(xRealWorld,yRealWorld,hands[0].transform.position.z);//HandLeft
				lastJoints[0].position = hands[0].transform.position;
			}else{
				hands[0].transform.position = new Vector3(hands[0].transform.position.x,hands[0].transform.position.y,hands[0].transform.position.z);
			}
		}
		if (canMove(rightHand,1)){
			//xRealWorld
			float xRealWorld = (-xRPosHandRight/xMaxKinect)*((xMaxUnity - xMinUnity)/2) +xCenterUnity;
		 	
			//yRealWorld
			float yRealWorld; 
			if (yRPosHandRight > 0)
				yRealWorld = (yRPosHandRight/yMaxKinect)*((yMaxUnity - yMinUnity)/2) +yCenterUnity;
			else
			 	yRealWorld = (-yRPosHandRight/yMinKinect)*((yMaxUnity - yMinUnity)/2) +yCenterUnity;
			
			if (!isHandOver()){
				hands[1].transform.position = new Vector3(xRealWorld,yRealWorld,hands[1].transform.position.z);//HandLeft
				lastJoints[1].position = hands[1].transform.position;
			}
		}
		
		if (isHandOver())
			//hands[0].transform.position = new Vector3(hands[0].transform.position.x,hands[0].transform.position.y,hands[0].transform.position.z);
			Debug.Log(hands[0].renderer.bounds+" "+hands[0].renderer.bounds.size+" "+hands[1].renderer.bounds);
		
		
		
	}
	
	private bool isHandOver(){
	/*	if (hands[0].transform.position.x - hands[1].transform.position.x < deltaHands)
			return true;
		if (hands[0].transform.position.x < hands[1].transform.position.x )
			return true;*/
		return false;
		//return ((hands[0].renderer.bounds.center.x+ hands[0].renderer.bounds.size.x)== (hands[1].renderer.bounds.center.x+ hands[1].renderer.bounds.size.x));		
	}
	
	void OnGUI () {
    // Make a text field that modifies stringToEdit.
    	 //GUI.TextField(new Rect(10, 10, 600, 600), stringToEdit, 500);
		 //stringToEdit = GUI.TextArea(new Rect(10, 10, 600, 600), stringToEdit, 1000);
		if (skeleton != null && newData)
		{
//			Debug.Log(count);
			newData = false;
			count++;
			List<SkeletonsData> Skeletons = skeleton.getSkeletons();
			foreach (SkeletonsData skel in Skeletons){
				List<JointSkeleton> Joints = skel.getSkeletonJoint();
				Vector3 rightHand = new Vector3();
				Vector3 leftHand = new Vector3(); 
				Vector3 shoulderCenter = new Vector3();
				foreach(JointSkeleton joint in Joints)	{		
					if (joint.getJointName().ToString().Equals(JointType.HandRight.ToString()))
						rightHand = new Vector3(joint.getJointPosition().X,joint.getJointPosition().Y, joint.getJointPosition().Z);
					if (joint.getJointName().ToString().Equals(JointType.HandLeft.ToString()))
						leftHand = new Vector3(joint.getJointPosition().X,joint.getJointPosition().Y, joint.getJointPosition().Z);
					if (joint.getJointName().ToString().Equals(JointType.ShoulderCenter.ToString()))
						shoulderCenter = new Vector3(joint.getJointPosition().X,joint.getJointPosition().Y, joint.getJointPosition().Z);
					
				}
				moveGloves(rightHand,leftHand,shoulderCenter);
			}
		}	

		if (tex != null){
			 GUI.DrawTexture(new Rect(Screen.width - 128,0,128,128), tex);
		}
	}
	
	private bool isValid(byte[] fullMessage)
        {
            int count = 0;
            for (int i = 0; i < fullMessage.Length; i++) {
                if (count == 50 && i == 50)
                    return false;
                if (fullMessage[i] != 0)
                    return true;
                else
                    count++;
            }
            return false;
        }
	/*
	# region texture2image 
public static System.Drawing.Image Texture2Image(Texture2D texture)
{
  if (texture == null)
  {
      return null;
  }
  //Save the texture to the stream.
  byte[] bytes = texture.EncodeToPNG();
 
   //Memory stream to store the bitmap data.
  MemoryStream ms = new MemoryStream(bytes);
 
  //Seek the beginning of the stream.
  ms.Seek(0, SeekOrigin.Begin);
 
  //Create an image from a stream.
  System.Drawing.Image bmp2 = System.Drawing.Bitmap.FromStream(ms);
 
  //Close the stream, we nolonger need it.
  ms.Close();
  ms = null;
 
  return bmp2;
}
#endregion
 
 
 
 
 
# region image2texture 
public static Texture2D Image2Texture(System.Drawing.Image im)
{
    if (im == null)
      {
          return new Texture2D(4,4);
      }
 
 
      //Memory stream to store the bitmap data.
      MemoryStream ms = new MemoryStream();
 
 
      //Save to that memory stream.
      im.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
 
       //Go to the beginning of the memory stream.
      ms.Seek(0, SeekOrigin.Begin);
 
      //make a new Texture2D
      Texture2D tex = new Texture2D(im.Width, im.Height);
 
      tex.LoadImage(ms.ToArray());
 
      //Close the stream.
      ms.Close(); 
      ms = null;
 
      //
      return tex;
}
#endregion
	
	/*
    void ParseMessages()
    {
        // Loop until failure
        while (true)
        {
            // Recieve message from Stack
            OscMessage message = oscReader.Receive();

            // Return if there are no more messages available
            if (message == null) return;
            OscBundle bundle = message as OscBundle;
            if (bundle == null) return;
            
            // Enumerate over all elements
            IEnumerator e = bundle.Elements.GetEnumerator();
            while (e.MoveNext())
            {
                // Check if element matches OSC path of this gameObject
                OscElement el = e.Current as OscElement;
                if (el.Match(OSCPath))
                {
                    Move(bundle.DateTime, el.Args);
                }
            }
        }
    }

    private void Move(DateTime dateTime, object[] p)
    {
        Vector3 v = transform.position;
        if (p.Length >= 2)
        {
            // the object has to be cast to double first, before it can be cast to float
            v.x = (float)(double)p[0]; 
            v.y = (float)(double)p[1];
        }
        if (p.Length >= 3)
        {
            v.z = (float)(double)p[2];
        }
        transform.position = v;
    }*/
}

