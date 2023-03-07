using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float speed = 10f;
    private float normalSpeed;
    public float speedUp;
    public float SpeedColDown;
    public float jumpHeight = 6f;
    public float drag = 0.5f;
    public bool isGrounded = false;
    public float rotationSpeed = 10f;
    public Vector3 MoveVec{set;get;}

    private Rigidbody mybody;
    private Transform camTrans;

    private void Start() {
        mybody = gameObject.GetComponent<Rigidbody>();
        mybody.maxDepenetrationVelocity = rotationSpeed; //tốc độ lăn
        mybody.drag = drag; //tỉ trọng lăn

        normalSpeed = speed;

    }

    private void Update() {
        MoveVec = MoveInput();
        MoveVec = RotateWithCam();
        MoveBall();
        BallJump();
        
    }

    void MoveBall(){
        if(isGrounded ==true){ //nếu bóng đang trên không thì k được di chuyển
        mybody.AddForce((MoveVec * speed));}
        else
        {
            mybody.AddForce((MoveVec * 0));
        }
    }

    private Vector3 MoveInput(){
        Vector3 dir = Vector3.zero; //reset hướng liên tục
        dir.x = Input.GetAxis("Horizontal"); //tiến lùi
        dir.z = Input.GetAxis("Vertical");//trái phải
        //magnitude là hàm chỉ độ dài của vector
        if(dir.magnitude>1)  //đơn vị hướng vector nếu lớn hơn 1 thì sẽ k phải là vector
            dir.Normalize(); //khi đó sẽ gọi Normalize() để biến nó trở lại thành vector
        return dir;
    }

    private Vector3 RotateWithCam(){
        if(camTrans != null){ //nếu đã có cam
            Vector3 dir = camTrans.TransformDirection(MoveVec);//đặt hướng di chuyển bằng hướng cam
            dir.Set (dir.x, 0, dir.z); //đặt giá trị hướng theo thiết lập để khi di chuyển vẫn giữ theo hướng của cam
            return dir.normalized * MoveVec.magnitude;
        }else{ //nếu k có cam thì tìm và gắn cam
            camTrans= Camera.main.transform;
            return MoveVec;
        }
    }

    void BallJump()
    {
        //cài phím nhảy là space
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            Vector3 jumpDirection = new Vector3(0, jumpHeight, 0); // Vector3 for the jump direction
            mybody.AddForce(jumpDirection, ForceMode.VelocityChange);
            

        }
    }
    


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Wall") //nếu chạm tường thì tạch
            FindObjectOfType<GameManager>().GameOver();
        if(other.gameObject.tag=="Ground")//kiểm tra chạm đất
            isGrounded = true;
        if(other.gameObject.tag=="SpeedUp"){//kiểm tra chạm vạch boots
            speed = speedUp;
            StartCoroutine("SpeedTimeUp");
        }
        if(other.gameObject.tag=="Win")//kiểm tra chạm vạch win
            FindObjectOfType<GameManager>().Win();

    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag=="Ground")//kiểm tra khi bóng k chạm đất
            isGrounded = false;
    }

    IEnumerator SpeedTimeUp(){//gờ tăng tốc
        yield return new WaitForSeconds (SpeedColDown);
        speed = normalSpeed;
    }
}

    
