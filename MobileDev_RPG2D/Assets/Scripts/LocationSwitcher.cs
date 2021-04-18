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
                StartCoroutine(TravelEffect(CastleSpawnPoint));
                outsideArea.SetActive(false);
                casteArea.SetActive(true);
                break;
            case "EnterOutside":
                outsideArea.SetActive(true);
                casteArea.SetActive(false);
                StartCoroutine(TravelEffect(OutsideCaslteSpawnPoint));
                break;
            //caslte
            case "CastleEnterUp1":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                StartCoroutine(TravelEffect(CastleUpperSpawnPoint1));
                break;
            case "CastleEnterUp2":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                StartCoroutine(TravelEffect(CastleUpperSpawnPoint2));
                break;
            case "CastleEnterUp3":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                StartCoroutine(TravelEffect(CastleUpperSpawnPoint3));
                break;
            case "CastleEnterUp4":
                casteArea.SetActive(false);
                castleUpperArea.SetActive(true);
                StartCoroutine(TravelEffect(CastleUpperSpawnPoint4));
                break;
            case "EnterDungeon":
                casteArea.SetActive(false);
                castleLowerArea.SetActive(true);
                StartCoroutine(TravelEffect(CastleLowerSpawnPoint1));
                break;
            //castle Upper
            case "CastleEnterDown1":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                StartCoroutine(TravelEffect(CastleSpawnPoint1));
                break;
            case "CastleEnterDown2":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                StartCoroutine(TravelEffect(CastleSpawnPoint2));
                break;
            case "CastleEnterDown3":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                StartCoroutine(TravelEffect(CastleSpawnPoint3));
                break;
            case "CastleEnterDown4":
                casteArea.SetActive(true);
                castleUpperArea.SetActive(false);
                StartCoroutine(TravelEffect(CastleSpawnPoint4));
                break;
            //Castle Lower
            case "CastleLowerEnterUp":
                casteArea.SetActive(true);
                castleLowerArea.SetActive(false);
                StartCoroutine(TravelEffect(CastleDungeonSpawnPoint));
                break;
            case "CastleLowerEnterBoss":
                bossRoomArea.SetActive(true);
                castleLowerArea.SetActive(false);
                BossRoomExitPoint.SetActive(false);
                StartCoroutine(TravelEffect(BossRoomSpawnPoint));
                break;
            //Boss room
            case "BossRoomExit":
                bossRoomArea.SetActive(false);
                castleLowerArea.SetActive(true);
                StartCoroutine(TravelEffect(CastleLowerSpawnPoint2));
                break;
            default:
                Debug.Log("incorrect spelling from enter points");
                break;
        }
    }

    IEnumerator TravelEffect(GameObject location)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfx, "Door");
        GameSceneManager.Instance.transition.Play("BlackToClear");
        PlayerController.Instance.transform.position = location.transform.position;
        yield return new WaitForSeconds(1);
    }

    public void EnableExit()
    {
        BossRoomExitPoint.SetActive(true);
    }
}
