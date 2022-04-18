using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public static class UnlitScenes
{
    enum UnlitSceneNums
    {
        Cave
    };
     #region Variables
    private static List<UnlitSceneNums> scenes = new List<UnlitSceneNums>();
    #endregion

    #region Methods
    public static bool IsUnlitScene()
    {
        scenes = Enum.GetValues(typeof(UnlitSceneNums))
            .Cast<UnlitSceneNums>()
            .ToList(); 
        foreach (var item in scenes)
        {
            if (SceneManager.GetActiveScene().name == item.ToString())
            {
                return true;
            }
        }
        return false;
    }


    #endregion
}
