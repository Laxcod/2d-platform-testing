using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PlayManger : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] CameraController camController;
    [SerializeField] GameObject finishWindow;
    [SerializeField] TMP_Text finishText;
    [SerializeField] TMP_Text shootCountText;

    bool isBallOutside;
    bool isBallTeleporting;
    bool isGoal;
    Vector3 lastBallPosition;

    private void OnEnable() {
        ballController.onBallShooted.AddListener(updateShootCount);
    }

    private void OnDisable() {
       ballController.onBallShooted.RemoveListener(updateShootCount);
    }

    private void Update() 
    {
        // Debug.Log(
        //     ballController.ShootingMode.ToString() + "" +
        //     ballController.IsMove() + "" +
        //     isBallOutside + "" +
        //     ballController.enabled + "" +
        //     isBallTeleporting + "" +
        //     isGoal
        //     );

        if(ballController.ShootingMode)
        {
            lastBallPosition = ballController.transform.position;
        }

       var InputActive = Input.GetMouseButton(0) 
            && ballController.IsMove() == false 
            && ballController.ShootingMode == false
            && isBallOutside == false;   
            
       camController.SetInputActive(InputActive);
    }

    public void OnBallGoalEnter()
    {
        isGoal = true;
        ballController.enabled = false;

        // TODO window player win pop up 
        finishWindow.gameObject.SetActive(true);
        finishText.text = "Masuk Pak Eko(Kata Bang Faris Klo Bolanya Masuk Dikasih Hadiah Kiko Run)\n" +"Jumlah Tembakan: " +ballController.ShootCount;
    }

    public void OnBallOutside()
    {
       if(isGoal)
         return;

       if(isBallTeleporting == false)
            Invoke("TeleportBallLastPosition",3);

       ballController.enabled = false;
       isBallOutside = true; 
       isBallTeleporting = true;
    }

    public void TeleportBallLastPosition()
    {
        TeleportBall(lastBallPosition);
    }

    public void TeleportBall(Vector3 targetPosition)
    {
        var rb = ballController.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ballController.transform.position = targetPosition;
        rb.isKinematic = false;

        ballController.enabled = true;
        isBallOutside = false;
        isBallTeleporting = false;
    }

    public void updateShootCount(int shootCount)
    {
        shootCountText.text = shootCount.ToString();
    }
}
