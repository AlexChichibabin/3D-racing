using UnityEngine;

namespace Racing
{
    public class WheelEffect : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] wheels;
        [SerializeField] private ParticleSystem[] wheelsSmoke;
        [SerializeField] private new AudioSource audio;
        [SerializeField] private float forvardSlipLimit;
        [SerializeField] private float sidewaySlipLimit;
        [SerializeField] private GameObject skidPrefab;
        [SerializeField] private float skidWheelOffset;

        private WheelHit wheelHit;
        private Transform[] skidTrail;

        private void Start()
        {
            skidTrail = new Transform[wheels.Length];
        }

        private void Update()
        {
            bool isSlip = false;
            for(int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetGroundHit(out wheelHit);

                if (wheels[i].isGrounded == true)
                {
                    if (wheelHit.forwardSlip >= forvardSlipLimit || wheelHit.sidewaysSlip >= sidewaySlipLimit)
                    {
                        if (skidTrail[i] == null) skidTrail[i] = Instantiate(skidPrefab).transform;
                        if (audio.isPlaying == false) audio.Play(); 

                        if (skidTrail[i] != null)
                        {
                            skidTrail[i].position = wheels[i].transform.position - wheelHit.normal * (wheels[i].radius + skidWheelOffset);
                            skidTrail[i].forward = -wheelHit.normal;

                            wheelsSmoke[i].transform.position = skidTrail[i].position;
                            wheelsSmoke[i].Emit(1);
                        }
                        isSlip = true;
                        continue;
                    }
                }
                skidTrail[i] = null;
                wheelsSmoke[i].Stop();
            }
            if(isSlip == false) audio.Stop();
        }
    }
}