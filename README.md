# Mixed Reality Recognition Labs


Overview
---------
This github repo contains sample applications used for the Mixed Reality Recognition Labs. 
Those applications are Windows Mixed Reality Applications running either on Hololens or Windows Mixed Reality Immersive headset.
The applications 


The Labs
---------
Below the list of Labs:<p/>
	- **Lab 1**: 2D recognition using Vuforia</p>
	- **Lab 2**: 3D recognition using Vuforia</p>
	- **Lab 3**: Using Assimp to convert STL files into OBJ files supported by Unity</p>
	- **Lab 4**: Remoting Host Sample application running on Windows 10 used to stream 3D models towards Hololens running the Holographic Remoting Player. Further information [here](https://docs.microsoft.com/en-us/windows/mixed-reality/holographic-remoting-player) </p>
	- **Lab 5**: How to generate AssetBundles, Hosting AssetBundles on Azure Web App, Loading AssetBundles hosted on a Web Site with my Windows Mixed Reality application</p>	
    - **Lab 6**: How to create programmatically GameObject from primitive objects (cube, sphere, ...), prefabs objects, local AssetBundles, remote AssetBundles.</p>	
    - **Lab 7**: How to display programmatically GameObject when the application finds a MultiTarget object. The GameObject are created from primitive objects (cube, sphere, ...), prefabs objects, local AssetBundles, remote AssetBundles. This Labs contains 3 applications:</p>
    **Test_HoloNavigation**: basic navigation between scenes,</p>
    **Test_HoloVuforia**: sample application which activate Vuforia on individual scene, this sample is a turn around to the Vuforia bugs described [here](https://forum.unity.com/threads/use-ar-camera-vuforia-core-in-individual-scene-not-entire-project.498489/)</p>
    **VF_DynamicReco**: final application </p>

Prerequisites
--------------

In order to build the applications associated with each lab, you need: 
1. A machine running Windows 10 Fall Creator Update (RS3)
2. [Visual Studio 2017](https://www.visualstudio.com/downloads/ )
3. [Windows 10 SDK Fall Creator Update](https://developer.microsoft.com/en-us/windows/downloads/windows-10-sdk)
4. [Unity version 2017.2.1p2](https://unity3d.com/unity/qa/patch-releases). While  installing Unity, don't forget to select Vuforia, Windows Store ILCPP Scripting Backend, Windows Store .NET Scripting Backend options.

![](https://raw.githubusercontent.com/flecoqui/MixedRealityRecognitionLabs/master/Docs/options.png)

Building the applications
--------------------------

1. Start Unity, **Open** the folder where the project is installed on your machine.
2. Once the project is opened,select **File** \> **Build Settings** \>

![](https://raw.githubusercontent.com/flecoqui/MixedRealityRecognitionLabs/master/Docs/settings.png)

3. On the dialog box **Build Settings** select **Universal Windows Platform** and click on button **Switch platform**.
4. Click on **Unity C# projects** check box.
5. Click on button **Build**. Select or Create the folder where you want to store the Visual Studio solution. Unity is now generating the Visual Studio solution. After few seconds the solution is generated.
6. Double-Click on the solution file, the Visual Studio will automacially open the Visual Studio project.
7. On the tool bar, select **Debug**, **x86** and **Device**

![](https://raw.githubusercontent.com/flecoqui/MixedRealityRecognitionLabs/master/Docs/vs.png)

8. Press Ctrl+Shift+B, or select **Build** \> **Build Solution** to build the solution.


Building the applications
--------------------------

1. Connect your Hololens to your machine with the USB cable
2. Power-on the Hololens.
3. On the tool bar, select **Debug**, **x86** and **Device**

![](https://raw.githubusercontent.com/flecoqui/MixedRealityRecognitionLabs/master/Docs/vs.png)

4. To debug the application and then run it, press F5 or select **Debug** \> **Start Debugging**. To run the application without debugging, press Ctrl+F5 or select **Debug** \> **Start Without Debugging**.


Next steps
-----------

Those recognition Labs are based on Vuforia. Those Labs could be extends to support the following features:</p>
1.  Support of other recognition libraries like dlib.net, Cognitive Services Computer Vision</p>
2.  Support of the latest verison of Unity.</p>

 

  
