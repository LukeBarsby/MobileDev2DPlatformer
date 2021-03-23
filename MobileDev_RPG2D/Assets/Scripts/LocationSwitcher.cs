using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocationSwitcher : MonoBehaviour
{
    public event Action<string> onEnterPointCollisionEnter;
    public void TriggerEnter(string id)
    {
        if (onEnterPointCollisionEnter != null)
        {
            onEnterPointCollisionEnter(id);
        }
    }

    [SerializeField] GameObject outsideArea = default;
    [SerializeField] GameObject casteArea = default;
    [SerializeField] GameObject castleUpperArea = default;
    [SerializeField] GameObject castleLowerArea = default;
    [SerializeField] GameObject bossRoomArea = default;

    //outside
    [SerializeField] GameObject OutsideSpawnPoint = default;
    [SerializeField] GameObject OutsideCaslteSpawnPoint = default;
    //caslte
    [SerializeField] GameObject CastleSpawnPoint = default;
    [SerializeField] GameObject CastleSpawnPoint1 = default;
    [SerializeField] GameObject CastleSpawnPoint2 = default;
    [SerializeField] GameObject CastleSpawnPoint3 = default;
    [SerializeField] GameObject CastleSpawnPoint4 = default;
    [SerializeField] GameObject CastleDungeonSpawnPoint = default;
    //caslte upper
    [SerializeField] GameObject CastleUpperSpawnPoint1 = default;
    [SerializeField] GameObject CastleUpperSpawnPoint2 = default;
    [SerializeField] GameObject CastleUpperSpawnPoint3 = default;
    [SerializeField] GameObject CastleUpperSpawnPoint4 = default;
    //castle lower
    [SerializeField] GameObject CastleLowerSpawnPoint1 = default;
    [SerializeField] GameObject CastleLowerSpawnPoint2 = default;
    //boss room
    [SerializeField] GameObject BossRoomSpawnPoint = default;
    [SerializeField] GameObject BossRoomExitPoint = default;




    void Start()
    {
        PlayerController.Instance.EnableUI();

        outsideArea.SetActive(true);
        casteArea.SetActive(false);
        castleUpperArea.SetActive(false);
        castleLowerArea.SetActive(false);
        bossRoomArea.SetActive(false);

        PlayerController.Instance.transform.position = OutsideSpawnPoint.transform.position;
    }


    public void ChangeLocation(string id)
    {
        switch (id)
        {
            //outside
            case "CastleEnter":
                outsideArea.SetActive(false);
                casteArea.SetActive(true);
                PlayerController.Instance.transform.position = CastleSpawnPoint.transform.position;
                break;
            case "EnterOutside":
                outsideArea.SetActive(true);
                casteArea.SetActive(false);
                PlayerController.Instance.transform.position = OutsideCaslteSpawnPoint.transform.position;
                break;
            //caslte
            case "CastleEnterUp1":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                PlayerController.Instance.transform.position = CastleUpperSpawnPoint1.transform.position;
                break;
            case "CastleEnterUp2":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                PlayerController.Instance.transform.position = CastleUpperSpawnPoint2.transform.position;
                break;
            case "CastleEnterUp3":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                PlayerController.Instance.transform.position = CastleUpperSpawnPoint3.transform.position;
                break;
            case "CastleEnterUp4":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                PlayerController.Instance.transform.position = CastleUpperSpawnPoint4.transform.position;
                break;
            case "EnterDungeon":
                casteArea.SetActive(false);
                castleLowerArea.SetActive(true);
                PlayerController.Instance.transform.position = CastleLowerSpawnPoint1.transform.position;
                break;
            //castle Upper
            case "CastleEnterDown1":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                PlayerController.Instance.transform.position = CastleSpawnPoint1.transform.position;
                break;
            case "CastleEnterDown2":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                PlayerController.Instance.transform.position = CastleSpawnPoint2.transform.position;
                break;
            case "CastleEnterDown3":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                PlayerController.Instance.transform.position = CastleSpawnPoint3.transform.position;
                break;
            case "CastleEnterDown4":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                PlayerController.Instance.transform.position = CastleSpawnPoint4.transform.position;
                break;
            //Castle Lower
            case "CastleLowerEnterUp":
                casteArea.SetActive(true);
                castleLowerArea.SetActive(false);
                PlayerController.Instance.transform.position = CastleDungeonSpawnPoint.transform.position;
                break;
            case "CastleLowerEnterBoss":
                bossRoomArea.SetActive(true);
                castleLowerArea.SetActive(false);
                BossRoomExitPoint.SetActive(false);
                PlayerController.Instance.transform.position = BossRoomSpawnPoint.transform.position;
                break;
            //Boss room
            case "BossRoomExit":
                bossRoomArea.SetActive(false);
                castleLowerArea.SetActive(true);
                PlayerController.Instance.transform.position = CastleLowerSpawnPoint2.transform.position;
                break;
            default:
                Debug.Log("incorrect spelling from enter points");
                break;
        }
    }

    public void EnableExit()
    {
        BossRoomExitPoint.SetActive(true);
    }
}
