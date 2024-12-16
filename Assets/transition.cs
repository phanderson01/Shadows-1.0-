using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LoadGamePlay : MonoBehaviour
{
   public string nextScene = "";

   public void LoadLevel()
   {
      SceneManager.LoadScene(nextScene);
   }
}