using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>(0);
   
    int resolutionNum;

    public Dropdown.OptionData option { get; set; }

    void Start()
    {
        initUI();
    }

    
    void initUI()
    {
        for( int i = 0; i<Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                resolutions.Add(Screen.resolutions[i]);
        }
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0;
        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if(item.width == Screen.width)
            {
                resolutionDropdown.value = optionNum;
                
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
        
    }

    public void DropboxOptionchange(int x)
    {
        resolutionNum = x;
    }
    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void okBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height,screenMode);
    }
}
