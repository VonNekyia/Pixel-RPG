using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsCanvas : MonoBehaviour
    {

        
        private List<String> _resolutionOptions = new List<string>();
        
        [Header("UI")] 
        [SerializeField] private Dropdown fullScreenDropdown;
        [SerializeField] private Dropdown resolutionDropdown;

        //Screen.fullscreen
        //Screen.width, Screen.height
        //Screen.resolutions -> player settings panel
        //Screen.SetResolution(int width, int height, bool fullscreen)
        
        private void Start()
        {
            ResolutionDropdownInit();
        }

        private void SettingsClicked()
        {
            gameObject.SetActive(true);
        }

        private void SettingsExit()
        {
            gameObject.SetActive(false);
        }

        public void OnFullScreenDropdown()
        {
            if (fullScreenDropdown.value == 0) //Fullscreen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            if (fullScreenDropdown.value == 1) //Windowed Fullscreen
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            if (fullScreenDropdown.value == 2) //Windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        public void ResolutionDropdownInit()
        {
            foreach (Resolution resolution in Screen.resolutions)
            {
                Debug.Log(resolution.width + " x " + resolution.height);
                _resolutionOptions.Add(resolution.width + " x " + resolution.height);
            }
            resolutionDropdown.AddOptions(_resolutionOptions);
        }
        
        public void OnResolutionDropdown()
        {
            String resolution = resolutionDropdown.transform.GetChild(0).GetComponent<Text>().text;
            String[] resolutionValues = new String[3];
            int width;
            int height;
            
            resolutionValues = resolution.Split(" ");
            width = Int32.Parse(resolutionValues[0]);
            height = Int32.Parse(resolutionValues[2]);
            
            Debug.Log(width + " x " + height);
        }
        
    }
}