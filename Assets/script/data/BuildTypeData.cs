using UnityEngine;

enum BuildType{
    VITA,
    PC
}

class BuildTypeData{
    
    #if UNITY_WEBPLAYER || UNITY_EDITOR
    public const BuildType buildType = BuildType.PC;
    #elif UNITY_PSM
    public const BuildType buildType = BuildType.VITA;
    #endif

    private static KeyCode pauseKey_;
    public static KeyCode pauseKey {
       get {
          return pauseKey_; 
       }
    }

    private static KeyCode mapKey_;
    public static KeyCode mapKey {
        get {
            return mapKey_;
        }
    }

    public static float itemScroll{
        get{
            if(BuildTypeData.buildType == BuildType.PC){
                return Input.GetAxis("Mouse ScrollWheel");
            }else if(BuildTypeData.buildType == BuildType.VITA){
                int count = 0;
                if (Input.GetKey(KeyCode.JoystickButton9)){
                    count++;
                }
                if (Input.GetKey(KeyCode.JoystickButton11)){
                    count--;
                }
                return count;
            }
        }
    }

    public static void Init(){
        if (BuildTypeData.buildType == BuildType.PC) {
            pauseKey_ = KeyCode.T;
        }else if(BuildTypeData.buildType == BuildType.VITA){
            pauseKey_ = KeyCode.JoystickButton7;
        }
        if (BuildTypeData.buildType == BuildType.PC){
            mapKey_ = KeyCode.M;
        }else if (BuildTypeData.buildType == BuildType.VITA){
            mapKey_ = KeyCode.JoystickButton6;
        }
    }
    
}