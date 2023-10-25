using UnityEngine;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bones;
    public float weight = 1;
}


public class WeaponIK : MonoBehaviour
{

    public Transform aimTransform;

    public int iteration = 10;
    [Range(0, 1)] public float weight = 0;

    public float angleLimit = 90;
    public float distanceLimit = 1.5f;
    public Vector3 offset;

    public HumanBone[] humanBones;
    private Transform[] boneTransform;
    private AiAgent agent;

    private void Start()
    {
        Animator anim = GetComponent<Animator>();
        boneTransform = new Transform[humanBones.Length];
        for (int i = 0; i < boneTransform.Length; i++)
        {
            boneTransform[i] = anim.GetBoneTransform(humanBones[i].bones);
        }

        agent = GetComponent<AiAgent>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = GetTransformPosition();

        for (int i = 0; i < iteration; i++)
        {
            for (int b = 0; b < boneTransform.Length; b++)
            {
                Transform bone = boneTransform[b];
                float _weight = weight * humanBones[b].weight;
                AimAtTarget(bone, targetPosition, _weight);
            }
        }

    }

    void AimAtTarget(Transform _bone, Vector3 _targetPosition, float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirction = _targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirction);
        Quaternion blendRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        _bone.rotation = blendRotation * _bone.rotation;
    }

    Vector3 GetTransformPosition()
    {
        Vector3 targetDirection = (agent.target.position + offset) - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50;
        }
        float targetDistance = targetDirection.magnitude;
        if(targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance;
        }
        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);

        return aimTransform.position + direction;
    }
}
