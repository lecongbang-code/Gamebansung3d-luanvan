using UnityEngine;

public class GunS : MonoBehaviour
{
    [Header("Weapon")]
    public Weapon weapon;

    [Header("Recoil_Transform")]
    public Transform RecoilPositionTranform;
    public Transform RecoilRotationTranform;

    void FixedUpdate()
    {
        weapon.CurrentRecoil1 = Vector3.Lerp(weapon.CurrentRecoil1, Vector3.zero, weapon.Recoil1 * Time.deltaTime);
        weapon.CurrentRecoil2 = Vector3.Lerp(weapon.CurrentRecoil2, weapon.CurrentRecoil1, weapon.Recoil2 * Time.deltaTime);
        weapon.CurrentRecoil3 = Vector3.Lerp(weapon.CurrentRecoil3, Vector3.zero, weapon.Recoil3 * Time.deltaTime);
        weapon.CurrentRecoil4 = Vector3.Lerp(weapon.CurrentRecoil4, weapon.CurrentRecoil3, weapon.Recoil4 * Time.deltaTime);

        RecoilPositionTranform.localPosition = Vector3.Slerp(RecoilPositionTranform.localPosition, weapon.CurrentRecoil3, weapon.PositionDampTime * Time.fixedDeltaTime);
        weapon.RotationOutput = Vector3.Slerp(weapon.RotationOutput, weapon.CurrentRecoil1, weapon.RotationDampTime * Time.fixedDeltaTime);
        RecoilRotationTranform.localRotation = Quaternion.Euler(weapon.RotationOutput);
    }

    public void Fire()
    {
        if (weapon.aim == true)
        {
            weapon.CurrentRecoil1 += new Vector3(weapon.RecoilRotation_Aim.x, Random.Range(-weapon.RecoilRotation_Aim.y, weapon.RecoilRotation_Aim.y), Random.Range(-weapon.RecoilRotation_Aim.z, weapon.RecoilRotation_Aim.z));
            weapon.CurrentRecoil3 += new Vector3(Random.Range(-weapon.RecoilKickBack_Aim.x, weapon.RecoilKickBack_Aim.x), Random.Range(-weapon.RecoilKickBack_Aim.y, weapon.RecoilKickBack_Aim.y), weapon.RecoilKickBack_Aim.z);
        }
        if (weapon.aim == false)
        {
            weapon.CurrentRecoil1 += new Vector3(weapon.RecoilRotation.x, Random.Range(-weapon.RecoilRotation.y, weapon.RecoilRotation.y), Random.Range(-weapon.RecoilRotation.z, weapon.RecoilRotation.z));
            weapon.CurrentRecoil3 += new Vector3(Random.Range(-weapon.RecoilKickBack.x, weapon.RecoilKickBack.x), Random.Range(-weapon.RecoilKickBack.y, weapon.RecoilKickBack.y), weapon.RecoilKickBack.z);
        }
    }
}