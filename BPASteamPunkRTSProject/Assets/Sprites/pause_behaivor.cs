using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class pause_behaivor : MonoBehaviour
{
    public GameManager g_manager;
    public int i;
    public GameObject Parent;
    //the invisible buttons for different panels
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    [SerializeField]
    public GameObject panel4;
    public GameObject panel5;
    public GameObject MovingButtonandGear;
    //public Image c_g_s;
    //public Image n_g_s;
    //public Image g_s;
    public TextMeshProUGUI c_g_s;
    public TextMeshProUGUI n_g_s;
    public TextMeshProUGUI g_s;
    public TextMeshProUGUI a_s;
    public TextMeshProUGUI lcs_s;
    //list of the 3 menu settings right now
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    [SerializeField]
    public bool SettingOn = false;
    public Sprite UncheckedBox;
    public Sprite CheckedBox;
    //other things

    //quality settings
    public GameObject CheckHex;
    public GameObject checker_hexer;
    public Vector3[] check_hex_pos = new Vector3[6];
    public int quality_lvlforstart;
    public Text textures_txt;
    public TextMeshProUGUI anti_ass_text;
    public Text TD_res_text;
    public Text texture_text;
    public font_scaling td_rest_scaling;

    public Slider Final_AudioSlider;
    public Slider OutofG_AudioSlider;
    public Slider InG_AudioSlider;
    public TextMeshProUGUI final_audio_text;
    public float _FireRate = 0.0f;
    public float _CanFire = -1f;
    public TextMeshProUGUI out_of_game_txt;
    public TextMeshProUGUI inga_txt;

    public Image Final_AudioSlider_img;
    public Image OutofG_AudioSlider_img;
    public Image inga_slider_img;
    public Image inga_greening;
    public Image final_greening;
    public Image o_o_g_a_greening;
    public audio_manager audio_manager;

    public bool on_value_changed_is_true = true;

    //public level_creator_script lvl_creator_manager;
    public Transform[] _curFrametrAr;
    public GameObject[] allUdders;
    public GameObject image7;

    //only for the level creator begin testing button
    public Transform[] AllThisUddersFrames;
    public GameObject DevOnlYDEVMENU;
    public Scrollbar mainpoolSlider;
    public GameObject pool_area;
    public int[] PreviousUdderAmounts;
    public Text levelText;
    public TMP_InputField THEINPUTFIELD;
    public TMP_InputField PART2;
    // Start is called before the first frame update
    //void Start()
    //{
    //    if (this.CompareTag("level_creation"))
    //    {
    //        i = 0;
    //        lvl_creator_manager = GameObject.Find("lvl_creator_manager").GetComponent<level_creator_script>();
    //        image7 = GameObject.Find("Seperator");
    //        if (name == "Button")
    //        {
    //            DevOnlYDEVMENU = GameObject.Find("DEVONLY_devmenu");
    //        }
    //    }
    //    else
    //    {
    //        a_s = GameObject.Find("audio_settings").GetComponent<TextMeshProUGUI>();

    //    }
    //    g_manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
    //    audio_manager = GameObject.Find("Audio_Manager").GetComponent<audio_manager>();
    //    if (this.gameObject.name != "Button")
    //    {


    //        if (this.gameObject.name != "frame_creator_DEVONLY")
    //        {
    //            OutofG_AudioSlider = GameObject.Find("Slider (1)").GetComponent<Slider>();
    //            Final_AudioSlider = GameObject.Find("Slider").GetComponent<Slider>();
    //            InG_AudioSlider = GameObject.Find("Slider (2)").GetComponent<Slider>();
    //        }

    //    }

    //    if (this.gameObject.CompareTag("Slider"))
    //    {
    //        Final_AudioSlider_img = gameObject.GetComponentInChildren<Image>();
    //        final_greening = GameObject.FindGameObjectWithTag("greening_up_panel").GetComponent<Image>();

    //        out_of_game_txt = GameObject.Find("Square Image (1)").GetComponentInChildren<TextMeshProUGUI>();
    //        inga_txt = GameObject.Find("Square Image (2)").GetComponentInChildren<TextMeshProUGUI>();

    //        final_audio_text = GameObject.Find("Final Audio 2").GetComponent<TextMeshProUGUI>();
    //        if (this.gameObject.name == "Slider (1)")
    //        {
    //            OutofG_AudioSlider_img = gameObject.GetComponentInChildren<Image>();
    //            o_o_g_a_greening = GameObject.Find("o_o_g_a_greening").GetComponent<Image>();
    //        }
    //        if (this.gameObject.name == "Slider (2)")
    //        {
    //            inga_slider_img = gameObject.GetComponentInChildren<Image>();
    //            inga_greening = GameObject.Find("inga_greening").GetComponent<Image>();
    //        }
    //    }
    //    CheckHex = GameObject.Find("check_hex");
    //    //check_hex_pos[5] = new Vector3(318.3f, 521.9f+80.2f, 0f);
    //    //check_hex_pos[4] = new Vector3(318.3f, 521.9f, 0f);
    //    //check_hex_pos[3] = new Vector3(318.3f, 521.9f - 80.2f, 0f);
    //    //check_hex_pos[2] = new Vector3(318.3f, 521.9f - (80.2f*2), 0f);
    //    //check_hex_pos[1] = new Vector3(318.3f, 521.9f - (80.2f * 3), 0f);
    //    //check_hex_pos[0] = new Vector3(318.3f, 521.9f - (80.2f * 4), 0f);
    //    //panel4 = GameObject.Find("Ihavebigpp");
    //    //if(panel4 == null)
    //    //{
    //    //    Debug.LogError("We have small pp");
    //    //}
    //    //else
    //    //{
    //    //    Debug.LogError("Big Pp aquired");
    //    //}
    //    button4 = GameObject.Find("invisible_button_bottomest");
    //    if (this.gameObject.CompareTag("graphics_settings_arrow"))
    //    {
    //        if (this.gameObject.name == "Custom69")
    //        {
    //            check_hex_pos[5] = new Vector3(transform.position.x, transform.position.y, 0);


    //        }
    //        if (this.gameObject.name == "Super-Ultra2")
    //        {
    //            check_hex_pos[4] = new Vector3(transform.position.x, transform.position.y, 0);


    //        }
    //        if (this.gameObject.name == "Ultra32")
    //        {
    //            check_hex_pos[3] = new Vector3(transform.position.x, transform.position.y, 0);


    //        }
    //        if (this.gameObject.name == "High23")
    //        {
    //            check_hex_pos[2] = new Vector3(transform.position.x, transform.position.y, 0);


    //        }
    //        if (this.gameObject.name == "Medium23")
    //        {
    //            check_hex_pos[1] = new Vector3(transform.position.x, transform.position.y, 0);


    //        }
    //        if (this.gameObject.name == "Low23")
    //        {
    //            check_hex_pos[0] = new Vector3(transform.position.x, transform.position.y, 0);


    //        }
    //        textures_txt = GameObject.Find("texturetext").GetComponent<Text>();
    //        TD_res_text = GameObject.Find("3D Resolution1").GetComponent<Text>();
    //        td_rest_scaling = GameObject.Find("3D Resolution1").GetComponent<font_scaling>();
    //        anti_ass_text = GameObject.Find("anti_assailing_settigns").GetComponent<TextMeshProUGUI>();

    //    }
    //    if (this.gameObject.transform.parent == GameObject.Find("Place Where Settings Go (2)"))
    //    {
    //        TD_res_text = GameObject.Find("3D Resolution1").GetComponent<Text>();
    //        textures_txt = GameObject.Find("Text5").GetComponent<Text>();

    //    }
    //    if (this.gameObject.tag == "ui_tab_button")
    //    {
    //        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.05f;

    //    }
    //}
    //public void OnLevelWasLoaded(int level)
    //{
    //    Debug.Log("OnLevelWasLoaded");

    //    if (gameObject.name == "Exit")
    //    {
    //        //Debug.Log("gameObject.name == Exit");

    //        DevOnlYDEVMENU = GameObject.Find("ScrollView_DEVONLY (1)");
    //        if (level == 1)
    //        {
    //            Debug.Log("lvl_creator_manager");

    //            lvl_creator_manager = GameObject.Find("lvl_creator_manager").GetComponent<level_creator_script>();
    //        }
    //    }
    //}
    #region
    //public void StopTesting()
    //{
    //    int _curFrameUddAss;
    //    foreach (GameObject item in g_manager.AllUdders)
    //    {
    //        AllThisUddersFrames = item.GetComponentsInChildren<Transform>();
    //        for (int j = 1; j < AllThisUddersFrames.Length; j++)
    //        {
    //            _curFrameUddAss = AllThisUddersFrames[j].GetComponent<Frame>().Frame_ID;
    //            AllThisUddersFrames[j].SetParent(lvl_creator_manager.ar_o_stuff[_curFrameUddAss - 1].transform);
    //            AllThisUddersFrames[j].localPosition = new Vector3(lvl_creator_manager.Children_of_lineholder[AllThisUddersFrames[j].GetComponent<Frame>().UdderAssignment].position.x - 1266.5f, 0f, 0f);
    //        }

    //    }
    //    lvl_creator_manager.LineHolder.SetActive(true);
    //    lvl_creator_manager.space_number.gameObject.SetActive(true);
    //    lvl_creator_manager.ar_o_stuff[0].transform.parent.parent.parent.gameObject.SetActive(true);
    //    image7.SetActive(true);
    //    DevOnlYDEVMENU.SetActive(true);
    //    lvl_creator_manager.space_number.GetComponent<spacing_slider_behaivor>().space_slider.gameObject.SetActive(true);
    //    g_manager.StopAllCoroutines();
    //    audio_manager.Stop(SceneManager.GetActiveScene().name);
    //    g_manager.FrameCoutning = false;
    //    g_manager.FrameNumber = -3;
    //    g_manager.Text.text = "";
    //    foreach (GameObject item in g_manager.AllUdders)
    //    {
    //        item.GetComponent<Animator>().SetFloat("unwind_speed", 100000f);
    //        item.GetComponent<Animator>().SetTrigger("UdderMissed");
    //    }
    //}
    #endregion
    #region
    //public void CreateFrame()
    //{
    //    StartCoroutine(OpenPool());
    //    GameObject _newFrame = Instantiate(lvl_creator_manager.frame, lvl_creator_manager.frameHolder.transform, true);
    //    _newFrame.transform.localPosition = new Vector3(0, 0, 0f);
    //    _newFrame.GetComponent<Frame>().lvl_creator = lvl_creator_manager;
    //}
    #endregion
    #region
    //public IEnumerator OpenPool()
    //{
    //    //there needs to be some sort of counter after the click that says after .2 seconds reset a certain number that increases when this is clicked on.
    //    i++;
    //    int j = 0;
    //    while (i < 2 && j == 0)
    //    {
    //        yield return new WaitForSecondsRealtime(0.2f);
    //        j++;
    //        i = 0;
    //    }
    //    if (i < 2)
    //    {

    //    }
    //    else
    //    {
    //        mainpoolSlider.gameObject.SetActive(true);
    //        mainpoolSlider.value = 0.771f;
    //    }
    //}
    #endregion
    #region
    //public void StartTesting()
    //{
    //    foreach (GameObject item in g_manager.AllUdders)
    //    {
    //        item.GetComponent<Animator>().SetFloat("unwind_speed", 1f);
    //        item.GetComponent<NewUdderScript>().k = 0;
    //    }
    //    g_manager.h_scorescript.i = 0;
    //    g_manager.h_scorescript.I = 0;

    //    int _curFrameUddAss;
    //    int j = 0;
    //    //Frame _curFrame = lvl_creator_manager.ar_o_stuff[j].transform.GetChild(0).GetComponent<Frame>();

    //    foreach (GameObject item in lvl_creator_manager.ar_o_stuff)
    //    {
    //        _curFrametrAr = item.GetComponentsInChildren<Transform>();
    //        int childCount = item.transform.childCount;
    //        if (childCount > 1)
    //        {
    //            //item2 are the actual frames
    //            for (int i = 2; i - 1 < childCount; i++)
    //            {
    //                if (childCount > 2)
    //                {
    //                    Debug.Log(childCount + "<- Child Count");
    //                    Debug.Log(i - 1 + "<- i value");
    //                }



    //                _curFrametrAr[i].SetParent(GameObject.Find("Udder" + _curFrametrAr[i].GetComponent<Frame>().UdderAssignment).transform);
    //                _curFrametrAr[i].position = new Vector3(-100, -100, 0f);
    //            }
    //        }
    //    }
    //    allUdders = GameObject.FindGameObjectsWithTag("Udder");
    //    foreach (GameObject item in allUdders)
    //    {
    //        item.GetComponent<NewUdderScript>().Start();
    //    }
    //    lvl_creator_manager.LineHolder.SetActive(false);
    //    lvl_creator_manager.space_number.gameObject.SetActive(false);
    //    lvl_creator_manager.ar_o_stuff[0].transform.parent.parent.parent.gameObject.SetActive(false);
    //    image7.SetActive(false);
    //    transform.parent.gameObject.SetActive(false);
    //    lvl_creator_manager.space_number.GetComponent<spacing_slider_behaivor>().space_slider.gameObject.SetActive(false);
    //    g_manager.StartCoroutine(g_manager.CountDownAndRemoveFramesIgnoringIndex());
    //    //g_manager.StartCoroutine()
    //}
    #endregion
    public void LessTexture()
    {
        if (QualitySettings.GetQualityLevel() == 5)
        {
            switch (QualitySettings.globalTextureMipmapLimit)
            {
                default:
                    //0
                    QualitySettings.globalTextureMipmapLimit += 1;
                    break;
                case 1:
                    QualitySettings.globalTextureMipmapLimit += 1;
                    break;
                case 2:
                    QualitySettings.globalTextureMipmapLimit += 1;
                    break;
                case 3:
                    QualitySettings.globalTextureMipmapLimit = 0;
                    break;
            }
            UpdateSettingsText();
        }
    }
    public void MoreTexture()
    {
        if (QualitySettings.GetQualityLevel() == 5)
        {
            switch (QualitySettings.globalTextureMipmapLimit)
            {
                default:
                    //0
                    QualitySettings.globalTextureMipmapLimit = 3;
                    break;
                case 1:
                    QualitySettings.globalTextureMipmapLimit -= 1;
                    break;
                case 2:
                    QualitySettings.globalTextureMipmapLimit -= 1;
                    break;
                case 3:
                    QualitySettings.globalTextureMipmapLimit -= 1;
                    break;
            }
            UpdateSettingsText();
        }
    }
    public void LessAnti()
    {

        if (QualitySettings.GetQualityLevel() == 5)
        {
            switch (QualitySettings.antiAliasing)
            {
                default:
                    //0
                    QualitySettings.antiAliasing = 8;
                    break;
                case 8:
                    QualitySettings.antiAliasing = 4;
                    break;
                case 4:
                    QualitySettings.antiAliasing = 2;
                    break;
                case 2:
                    QualitySettings.antiAliasing = 0;
                    break;
            }
            UpdateSettingsText();
        }
    }
    public void MoreAnti()
    {

        if (QualitySettings.GetQualityLevel() == 5)
        {
            switch (QualitySettings.antiAliasing)
            {
                default:
                    //0
                    QualitySettings.antiAliasing = 2;
                    break;
                case 2:
                    QualitySettings.antiAliasing = 4;
                    break;
                case 4:
                    QualitySettings.antiAliasing = 8;
                    break;
                case 8:
                    QualitySettings.antiAliasing = 0;
                    break;
            }
            UpdateSettingsText();
        }
    }
    public void LessReso()
    {
        if (QualitySettings.GetQualityLevel() == 5)
        {
            switch (QualitySettings.resolutionScalingFixedDPIFactor)
            {
                default:
                    //0
                    QualitySettings.resolutionScalingFixedDPIFactor -= .1f;
                    break;
                case .1f:
                    break;
            }
            UpdateSettingsText();
        }
    }
    public void MoreReso()
    {
        if (QualitySettings.GetQualityLevel() == 5)
        {


            //0
            if (QualitySettings.resolutionScalingFixedDPIFactor >= 1.99f)
            {
                //nothing
            }
            else
            {
                QualitySettings.resolutionScalingFixedDPIFactor += .1f;

            }

            UpdateSettingsText();
        }
    }
    public void Continue()
    {
        if (Parent.activeSelf == true)
        {
            Parent.SetActive(false);
            Time.timeScale = 1f;
            audio_manager.all_audio_srcs[0].volume = OutofG_AudioSlider.value / 100f;
            audio_manager.all_audio_srcs[1].volume = OutofG_AudioSlider.value / 100f;
            audio_manager.UpdateVolume(SceneManager.GetActiveScene().name);

        }
    }
    public void Exit()
    {
        
    }
    public void Button1()
    {
        c_g_s.color = new Color(0f, 0f, 0f);
        n_g_s.color = new Color(1f, 1f, 1f);
        g_s.color = new Color(1f, 1f, 1f);
        a_s.color = new Color(1f, 1f, 1f);
        lcs_s.color = new Color(1f, 1f, 1f);

        g_manager.is_on_button_1 = true;
        g_manager.is_on_button_2 = false;
        g_manager.is_on_button_3 = false;
        g_manager.is_on_button_4 = false;
        g_manager.is_on_button_5 = false;
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);

        MovingButtonandGear.transform.position = new Vector3(button1.transform.position.x, button1.transform.position.y, 0f);

    }
    public void Button2()
    {
        c_g_s.color = new Color(1f, 1f, 1f);
        n_g_s.color = new Color(0f, 0f, 0f);
        g_s.color = new Color(1f, 1f, 1f);
        a_s.color = new Color(1f, 1f, 1f);
        lcs_s.color = new Color(1f, 1f, 1f);

        g_manager.is_on_button_1 = false;
        g_manager.is_on_button_2 = true;
        g_manager.is_on_button_3 = false;
        g_manager.is_on_button_4 = false;
        g_manager.is_on_button_5 = false;

        panel1.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);

        MovingButtonandGear.transform.position = new Vector3(button2.transform.position.x, button2.transform.position.y, 0f);

    }
    public void Button3()
    {
        c_g_s.color = new Color(1f, 1f, 1f);
        n_g_s.color = new Color(1f, 1f, 1f);
        g_s.color = new Color(0f, 0f, 0f);
        a_s.color = new Color(1f, 1f, 1f);
        lcs_s.color = new Color(1f, 1f, 1f);


        g_manager.is_on_button_1 = false;
        g_manager.is_on_button_2 = false;
        g_manager.is_on_button_3 = true;
        g_manager.is_on_button_4 = false;
        g_manager.is_on_button_5 = false;

        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
        panel4.SetActive(false);
        panel5.SetActive(false);

        MovingButtonandGear.transform.position = new Vector3(button3.transform.position.x, button3.transform.position.y, 0f);

    }
    public void Button4()
    {
        c_g_s.color = new Color(1f, 1f, 1f);
        n_g_s.color = new Color(1f, 1f, 1f);
        g_s.color = new Color(1f, 1f, 1f);
        a_s.color = new Color(0f, 0f, 0f);
        lcs_s.color = new Color(1f, 1f, 1f);


        g_manager.is_on_button_1 = false;
        g_manager.is_on_button_2 = false;
        g_manager.is_on_button_3 = false;
        g_manager.is_on_button_4 = true;
        g_manager.is_on_button_5 = false;

        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(true);
        panel5.SetActive(false);

        MovingButtonandGear.transform.position = new Vector3(button4.transform.position.x, button4.transform.position.y, 0f);

    }
    public void Button5()
    {
        c_g_s.color = new Color(1f, 1f, 1f);
        n_g_s.color = new Color(1f, 1f, 1f);
        g_s.color = new Color(1f, 1f, 1f);
        a_s.color = new Color(1f, 1f, 1f);
        lcs_s.color = new Color(0, 0, 0);

        g_manager.is_on_button_1 = false;
        g_manager.is_on_button_2 = false;
        g_manager.is_on_button_3 = false;
        g_manager.is_on_button_4 = false;
        g_manager.is_on_button_5 = true;

        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(true);

        MovingButtonandGear.transform.position = new Vector3(button5.transform.position.x, button5.transform.position.y, 0f);

    }
    public void BackButton()
    {
        if (g_manager.is_on_button_1 == true)
        {
            Continue();
        }
        else if (g_manager.is_on_button_2 == true)
        {
            Button1();
        }
        else if (g_manager.is_on_button_3 == true)
        {
            Button2();
        }
        else
        {
            Button3();
        }
    }
    //public void GraphicsTest()
    //{
    //    Debug.Log("IsFiring");
    //    CheckHex.transform.position = check_hex_pos[0];
    //    QualitySettings.SetQualityLevel(0, true);
    //    Debug.Log(QualitySettings.GetQualityLevel());
    //}

    public void OnOff()
    {
        if (SettingOn == false)
        {
            SettingOn = true;
            GetComponent<Image>().sprite = CheckedBox;
            PlayerPrefs.SetInt(this.gameObject.name + "1_if_true_0_if_false", 1);
        }
        else
        {
            SettingOn = false;
            GetComponent<Image>().sprite = UncheckedBox;
            PlayerPrefs.SetInt(this.gameObject.name + "1_if_true_0_if_false", 0);

        }
    }
    public void Six()
    {
        PlayerPrefs.SetInt("player_quality_lvl", 5);
        CheckHex.transform.position = check_hex_pos[5];
        QualitySettings.SetQualityLevel(5, true);
        Debug.Log(QualitySettings.GetQualityLevel());
        //make a method that checks each value in custom and then changes the text appropriately.
        UpdateSettingsText();
    }
    public void Five()
    {
        CheckHex.transform.position = check_hex_pos[4];
        QualitySettings.SetQualityLevel(4, true);
        Debug.Log(QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("player_quality_lvl", 4);
        UpdateSettingsText();


    }
    public void Four()
    {
        CheckHex.transform.position = check_hex_pos[3];
        QualitySettings.SetQualityLevel(3, true);
        Debug.Log(QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("player_quality_lvl", 3);
        UpdateSettingsText();

    }
    public void Three()
    {
        CheckHex.transform.position = check_hex_pos[2];
        QualitySettings.SetQualityLevel(2, true);
        Debug.Log(QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("player_quality_lvl", 2);
        UpdateSettingsText();


    }
    public void Two()
    {
        CheckHex.transform.position = check_hex_pos[1];
        QualitySettings.SetQualityLevel(1, true);
        Debug.Log(QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("player_quality_lvl", 1);
        UpdateSettingsText();

    }
    public void One()
    {
        CheckHex.transform.position = check_hex_pos[0];
        QualitySettings.SetQualityLevel(0, true);
        Debug.Log(QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("player_quality_lvl", 0);
        UpdateSettingsText();
    }
    // quality stuff

    public void UpdateSettingsText()
    {
        //textures
        switch (QualitySettings.GetQualityLevel())
        {
            default:
                //low 
                textures_txt.text = "Low";
                anti_ass_text.text = "Off";
                anti_ass_text.fontSize = 35.2f * (Screen.width / 1334f);
                TD_res_text.text = "50%";
                break;
            case 1:
                textures_txt.text = "Mid";
                anti_ass_text.text = "2x Multi Sampling";
                anti_ass_text.fontSize = 11.8f * (Screen.width / 1334f);
                TD_res_text.text = "66%";

                break;
            case 2:
                textures_txt.text = "High";
                anti_ass_text.text = "4x Multi Sampling";
                anti_ass_text.fontSize = 11.8f * (Screen.width / 1334f);
                TD_res_text.text = "82.6%";

                break;
            case 3:
                textures_txt.text = "Ultra";
                anti_ass_text.text = "8x Multi Sampling";
                anti_ass_text.fontSize = 11.8f * (Screen.width / 1334f);
                TD_res_text.text = "100%";

                break;
            case 4:
                textures_txt.text = "Ultra";
                anti_ass_text.text = "8x Multi Sampling";
                anti_ass_text.fontSize = 11.8f * (Screen.width / 1334f);
                TD_res_text.text = "100%";

                break;
            case 5:
                //dynamically determine
                switch (QualitySettings.globalTextureMipmapLimit)
                {
                    default:
                        //highest
                        textures_txt.text = "Ultra";

                        break;
                    case 1:
                        textures_txt.text = "High";


                        break;
                    case 2:
                        textures_txt.text = "Mid";


                        break;
                    case 3:
                        textures_txt.text = "Low";


                        break;
                }
                switch (QualitySettings.antiAliasing)
                {
                    default:
                        //8
                        anti_ass_text.text = "8x Multi Sampling";
                        anti_ass_text.fontSize = 11.8f * (Screen.width / 1334f);
                        break;
                    case 4:
                        anti_ass_text.text = "4x Multi Sampling";
                        anti_ass_text.fontSize = 11.8f * (Screen.width / 1334f);

                        break;
                    case 2:
                        anti_ass_text.text = "2x Multi Sampling";
                        anti_ass_text.fontSize = 11.8f * (Screen.width / 1334f);

                        break;
                    case 0:

                        anti_ass_text.text = "Off";
                        anti_ass_text.fontSize = 35.2f * (Screen.width / 1334f);
                        break;
                }
                string whatever;

                whatever = (QualitySettings.resolutionScalingFixedDPIFactor * 100) + "%";
                TD_res_text.text = whatever;
                PlayerPrefs.SetFloat("3d_resolution", QualitySettings.resolutionScalingFixedDPIFactor);
                PlayerPrefs.SetInt("anti-ail", QualitySettings.antiAliasing);
                PlayerPrefs.SetInt("textures", QualitySettings.globalTextureMipmapLimit);
                break;


        }
    }
    //slider method

    public void FinalAudioSlider()
    {
        if (Time.unscaledTime > _CanFire && on_value_changed_is_true == true)
        {
            float slider_val;
            slider_val = Final_AudioSlider.value;
            final_audio_text.text = "" + slider_val;
            _CanFire = Time.unscaledTime + _FireRate;
            AudioListener.volume = slider_val / 100f;
            Final_AudioSlider_img.color = new Color(1f, slider_val / 200f + .5f, 1f);
            final_greening.color = new Color(0f, 1f, 0f, slider_val / 1000);

        }

    }
    public void OutofGAudioSlider()
    {
        if (Time.unscaledTime > _CanFire && on_value_changed_is_true == true)
        {
            float slider_val;
            slider_val = OutofG_AudioSlider.value;

            out_of_game_txt.text = "" + slider_val;
            _CanFire = Time.unscaledTime + _FireRate;
            OutofG_AudioSlider_img.color = new Color(1f, slider_val / 200f + .5f, 1f);
            o_o_g_a_greening.color = new Color(0f, 1f, 0f, slider_val / 1000);

        }
        //Debug.Log(OutofG_AudioSlider + "" + out_of_game_txt);
    }
    public void InGAudioSlider()
    {
        if (Time.unscaledTime > _CanFire && on_value_changed_is_true == true)
        {
            float slider_val;
            slider_val = InG_AudioSlider.value;

            inga_txt.text = "" + slider_val;
            _CanFire = Time.unscaledTime + _FireRate;
            inga_slider_img.color = new Color(1f, slider_val / 200f + .5f, 1f);
            inga_greening.color = new Color(0f, 1f, 0f, slider_val / 1000);

        }
        //Debug.Log(OutofG_AudioSlider + "" + out_of_game_txt);
    }
    public void leftword_textures()
    {
        if (QualitySettings.GetQualityLevel() != 5)
        {
            int tempqual = QualitySettings.GetQualityLevel();
            //change 
        }
    }
    public void n_g_s_setting()
    {
        int getint = PlayerPrefs.GetInt(this.gameObject.name, 1);
    }
}