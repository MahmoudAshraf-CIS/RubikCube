using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        YesNoDialog dialog = YesNoDialog.Instance();
        dialog.Title("Rubik Cube").Message("Continue the lase game ?")
        .OnAccept("Yes", () => { // define what happens when user clicks Yes:
            Debug.Log("Yes is clicked");
            dialog.Hide();
        })
        .OnDecline("No", () =>
        {
            Debug.Log("No is clicked");
            DropMenuDialog sizeDialog = DropMenuDialog.Instance();
            sizeDialog.Title("size").Items(new string[] { "2*2", "3*3", "4*4", "5*5", "6*6" }).
            OnOkay((int i) =>
                {
                    Debug.Log(i + " is selected");
                    sizeDialog.Hide();
                }).Show();
            dialog.Hide();
        }
        )
        .Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
