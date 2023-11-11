using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    /*[SerializeField] private GameObject savesMenu;
    [SerializeField] private TextMeshProUGUI loadGameText;*/
    private static string path;
    private static FileInfo fi;
    [SerializeField, Tooltip("This parameter allow the script to delete the file with the saved data (used for test).")] private bool deleteFileQuit = false;

    private void Start()
    {
        // Open an existing file, or create a new one.
        fi = new FileInfo("MainMenu.cs");
        Debug.Log("Game directory: " + fi.DirectoryName);
        path = (fi.DirectoryName + @"\Assets\Data\saveData.txt");
        if (File.Exists(path))
        {
            //loadGameText.color = Color.white;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("VideoScene");
        if (File.Exists(path) == false)
        {
            createFile();
        }
    }

    private static void createFile() {
        // Determine the full path of the file just created.
        //DirectoryInfo di = fi.Directory;
        // Figure out what other entries are in that directory.
        //FileSystemInfo[] fsi = di.GetFileSystemInfos();

        try {
            /* //This code will create a new file in "<user>\AppData\Local\Temp"
            string path = Path.GetTempFileName();
            var fi1 = new FileInfo(path);
            Debug.Log(path + " " + fi1.FullName);
            */

            //FileStream f;
            var f = new FileInfo(path);
            StreamWriter sw = f.CreateText();
            sw.WriteLine("Hello");
            sw.WriteLine("And");
            sw.WriteLine("Welcome");
            sw.Close();
            if (File.Exists(path))
            {
                Debug.Log("File is created.");
                //f = File.Open(path, FileMode.Open);
                /*StreamWriter sw = f.CreateText();
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
                sw.Close();*/
            }
            else
            {
                Debug.Log("File is not created.");
                //f = File.Create(path); //If the file exist already, this method will overwrite it.
            }
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    private static void deleteFile()
    {
        if (File.Exists(path))
        {
            try
            {
                var f = new FileInfo(path);
                // Determine the full path of the file just created.
                DirectoryInfo di = f.Directory;
                // Figure out what other entries are in that directory.
                FileSystemInfo[] fsi = di.GetFileSystemInfos();
                foreach (FileSystemInfo fsi2 in fsi)
                {
                    Debug.Log(fsi2.Name);
                    fsi2.Delete();
                }
            } catch
            {
                Debug.Log("Error while deleting the data files");
            }
        }
    }

    public void gameLoaded() {
        if (File.Exists(path))
        {
            //savesMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void QuitGame()
    {
        if (deleteFileQuit)
        {
            deleteFile();
        }
        PlayerPrefs.SetInt("played", 0);
        //Conditional compilation
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}