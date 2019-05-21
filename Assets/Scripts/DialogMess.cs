using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMess : MonoBehaviour
{
    public float speed;
    [TextArea]
    public string[] mess;
    private int vitri1 = 0;
    private int vitri2 = 0;
    private bool ketThucCau;
    private bool seenDialog;

    private Text noidung;

    private GameObject dialog;
    

    IEnumerator hienthimess()
    {
        yield return new WaitForSeconds(speed);
        noidung.text  = noidung.text + mess[vitri1][vitri2];
        vitri2++;
        if(vitri2 >= mess[vitri1].Length)
        {
            ketThucCau = true;
            vitri2 = 0;
            vitri1++;
            //if(vitri1 < mess.Length)
                   
            
        }else if(vitri1 <mess.Length)
            StartCoroutine(hienthimess());
    }

    public void OnTriggerEnter2D(Collider2D sukien)
    {
        if (seenDialog == false &&  sukien.CompareTag("MainPlayer"))
        {
            sukien.GetComponent<MainPlayer>().choPhepDiChuyen = false;
            sukien.GetComponent<Animator>().SetBool("move", false);
            seenDialog = true;
            hiendialog();
        }
    }
   
    public void OnTriggerExit2D(Collider2D sukien)
    {
        if (seenDialog && sukien.CompareTag("MainPlayer"))
        {
            vitri1 = vitri2 = 0;
            seenDialog = ketThucCau = false;
            noidung.text = "";
        }
    }


    public void hiendialog()
    {
        dialog.SetActive(true);
     
        StartCoroutine(hienthimess());

    }
    // Start is called before the first frame update
    void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("textmes");
        noidung = dialog.transform.GetChild(0).GetComponent<Text>();

        noidung.text = "";
        dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(ketThucCau && Input.GetKey(KeyCode.Space))
        {
            if (vitri1 < mess.Length)
            {
                noidung.text = "";
                StartCoroutine(hienthimess());
                ketThucCau = false;
            }
            else {
                dialog.SetActive(false);
                GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<MainPlayer>().choPhepDiChuyen = true;
            }
        }
    }
}
