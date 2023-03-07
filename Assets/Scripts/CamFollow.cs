using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    // public float moveX =0f;
    // public float moveY = 0f;
    public float camSpeed =0.5f;
    public bool lookAtBall = false;
    public bool rotateWithAroundPlayer = true;
    public float rotateSpeed =5f;

    private Vector3 _camOffset;

    private void Start() {
        _camOffset = transform.position - player.position;
    }

    private void LateUpdate() { //dung lateupdate cho camera
        // transform.position = player.position + offset;
        if(rotateWithAroundPlayer){ //dùng Quaternion đẻ tính toán phép quay trong không gian 3d
            Quaternion camTurnAngle =  Quaternion.AngleAxis(Input.GetAxis("HorizontalArrow") *rotateSpeed, Vector3.up);

            _camOffset = camTurnAngle *_camOffset; //gắn vị trí cam xung quanh góc quay quanh người chơi
        }

        Vector3 newCamPos = player.position + _camOffset; //đặt ví trí cam vào người chơi
        transform.position = Vector3.Slerp(transform.position, newCamPos, camSpeed); 

        if(lookAtBall || rotateWithAroundPlayer){ //nếu đang k nhìn bóng hoặc đang ở xung quanh người chơi thì làm cam nhìn bóng
            transform.LookAt(player);
        }
    }

    // private void Update() {
    //     MouseCam();
    // }

    // void MouseCam(){
    //     moveX += Input.GetAxis("Mouse X") *camSpeed;
    //     moveY -= Input.GetAxis("Mouse Y") *camSpeed;
    //     transform.localEulerAngles = new Vector3(moveY, moveX, 0);
    // }
}
