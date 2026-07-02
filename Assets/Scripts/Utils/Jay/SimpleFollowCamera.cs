using UnityEngine;

namespace Study.Utilities
{
    // 타겟을 부드럽게(Lerp) 따라가는 범용 추적 카메라

    public class SimpleFollowCamera : MonoBehaviour
    {
        public enum States
        {
            Holding,
            Following
        }

        [field:SerializeField] public Transform Target {get; set;}
        [field:SerializeField] public float lerpSpeed { get; set; } // 따라가는 속도, Damping 값
        [SerializeField] private Vector3 offset;
        [SerializeField] private float maxOffset = 5f;

        private Camera Cam {get; set;}
        private States State {get; set;} = States.Following;

        private void Start()
        {
            // 현재 Scene의 MainCamera(Tag)를 가져옵니다.
            // ps : MainCamera Tag가 달려있는 녀석을 가져옵니다.
            Cam = Camera.main;
        }

        private void Update()
        {
            switch (State)
            {
                case States.Holding:
                    // Holding은 Target은 존재하지만 카메라를 이동하지 않는 상태입니다.
                    break;
                case States.Following:
                    // Target의 위치를 적절히 보간하여 사용합니다.
                    // ps : lerpSpeed는 댐핑값이라고 해서 속도에 대한 보정값입니다.
                    
                    // 아래의 보간내용은 transform.position을 Target.position + offset으로 초당 lerpSpeed의 속도로 
                    // 계속 가깝게 만드는 코드입니다. Target.position이 멀다면 더 빨리, 가깝다면 느리게
                    Vector3 lerpPos = Vector3.Lerp(transform.position, Target.position + offset, lerpSpeed * Time.deltaTime);
                    lerpPos.z = transform.position.z;
                    transform.position = lerpPos;
                    break;
            }
        }

        public void ChangeState(States state)
        {
            if(state == States.Following && Target == null)
            {
                Debug.Log($"SimpleFollowCamera :: Target이 없이 Following 상태로 전환할 수 없습니다");
                return;
            }
            State = state;
        }

        public void OnChangeTarget(Transform target)
        {
            Target = target;
        }

        public void SetOffset(Vector2 offset)
        {
            this.offset = Vector3.ClampMagnitude(offset, maxOffset);
        }
    }    
}