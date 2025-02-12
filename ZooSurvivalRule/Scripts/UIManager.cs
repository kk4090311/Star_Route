using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance
    {
        get { return _instance; }
    }
    public bool openUI = false;
    [SerializeField] private GameObject player; 

    //手冊
    [SerializeField] private GameObject manual;
    public bool manualOpen = false;
    public int manualSheet = 0;
    private Text sheetNumber;
    public List<GameObject> sheets = new List<GameObject>();
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    //任務
    [SerializeField] private GameObject job;
    public bool jobOpen = false;
    private Text jobText;

    //選單
    [SerializeField] private GameObject menu;
    public bool menuOpen = false;

    private void Update()
    {
        FindObject();

        //選單
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuOpen = !menuOpen;
        }

        if (menuOpen)
        {
            menu.SetActive(true);
        }
        else
        { 
            menu.SetActive(false);

            //手冊
            ManualLocation();
            JobLocation();
            ShowSheetNumber();
            ShowSheetButton();

            if (Input.GetKeyDown(KeyCode.Tab))
            { openUI = !openUI; }

            if (openUI == true)
            {
                manual.SetActive(true);
                job.SetActive(true);
            }
            else
            {
                manual.SetActive(false);
                job.SetActive(false);
            }

            ShowManualSheets();
        }

    }

    private void FindObject()
    {
        if(player == null)
        { player = GameObject.Find("Player"); }

        if(manual == null)
        { manual = GameObject.Find("Manual"); }

        if(job == null)
        { job = GameObject.Find("Job"); }

        if(sheetNumber == null)
        { sheetNumber = GameObject.Find("頁碼").GetComponent<Text>(); }

        if (leftButton == null)
        { leftButton = GameObject.Find("上一頁"); }

        if (rightButton == null)
        { rightButton = GameObject.Find("下一頁"); }
    }
    
    private void ManualLocation()
    {
        if (manualOpen)
        {
            manual.GetComponent<RectTransform>().anchoredPosition = new Vector2(-180f, 0f);
        }
        else
        {
            manual.GetComponent<RectTransform>().anchoredPosition = new Vector2(140f, 0f);
        }
    }
    private void ShowSheetNumber()
    {
        if(manualSheet == 0)
        { sheetNumber.text = ""; }
        else { sheetNumber.text = manualSheet.ToString(); }
    }
    private void ShowManualSheets()
    {
        if (manualOpen)
        {
            for (int i = 0; i < sheets.Count; i++)
            {
                sheets[i].SetActive(false);
                if(i == manualSheet)
                {
                    sheets[i].SetActive(true);
                }
            }
        }
    }
    private void ShowSheetButton()
    {
        if(manualSheet == 0)
        { leftButton.SetActive(false); }
        else { leftButton.SetActive(true); }

        if(manualSheet == sheets.Count - 1)
        { rightButton.SetActive(false); }
        else { rightButton.SetActive(true); }
    }

    private void JobLocation()
    {
        if (jobOpen)
        {
            job.GetComponent<RectTransform>().anchoredPosition = new Vector2(-186f, 117f);
        }
        else
        {
            job.GetComponent<RectTransform>().anchoredPosition = new Vector2(-186f, -90f);
        }
    }
    
    
}
