using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_cgd : MonoBehaviour {

    //游戏管理器脚本应该能够处理对游戏中所有脚本的初始化问题以及许多其它问题

    public static GameManager_cgd instance_cgd;
    public GameObject hitParticle_cgd;

    public GameObject hitParticleRed_cgd;
    public GameObject hitParticleBlue_cgd;
    public GameObject hitParticleGreen_cgd;
    public GameObject hitParticleWhite_cgd;

    public Image imageScoreNum01_cgd;
    public Image imageScoreNum02_cgd;
    public Image imageScoreNum03_cgd;
    public Sprite[] scoreImage = new Sprite[10];
    int image01Index_cgd = 0;
    int image02Index_cgd = 0;
    int image03Index_cgd = 0;
    public int playerScore_cgd;

    int firstScore_cgd;
    int secondScore_cgd;
    int thirdScore_cgd;
    public Text firstScoreText_cgd;
    public Text secondScoreText_cgd;
    public Text thirdScoreText_cgd;

    public Sprite congrafirst_cgd;
    public Sprite congrasecond_cgd;
    public Sprite congrathird_cgd;
    public Sprite encourage_cgd;
    public Text finalText_cgd;

    public GameObject imageNormal_cgd;
    public GameObject imageSmile_cgd;

    [HideInInspector]
    public bool is_90secsEnd_cgd;

    public AudioSource playShortMusic_cgd;
    public AudioClip redMusic_cgd;
    public AudioClip greenMusic_cgd;
    public AudioClip blueMusic_cgd;

    public AudioClip[] shortAudioClips = new AudioClip[7];
    public int indexAudioClip = 0;

    void Awake()
    {
        instance_cgd = this;
    }
    void Start()
    {
        playerScore_cgd = 0;
        GetTopThreeScore();
        InitPlayerScore();
        is_90secsEnd_cgd = false;
    }

    public void PlayShortMusicSceond()
    {
        if (indexAudioClip == 7)
        {
            indexAudioClip = 0;
        }
        playShortMusic_cgd.PlayOneShot(shortAudioClips[indexAudioClip]);
        indexAudioClip++;
    }

    #region 弃用，短音效放在粒子上了，不用手动播放了
    public void PlayShortMusic(string whichMusic)
    {
        switch (whichMusic)
        {
            case "red":
                playShortMusic_cgd.PlayOneShot(redMusic_cgd);
                break;
            case "green":
                playShortMusic_cgd.PlayOneShot(greenMusic_cgd);
                break;
            case "blue":
                playShortMusic_cgd.PlayOneShot(blueMusic_cgd);
                break;
        }
    }
    #endregion
    #region 弃用 新的设计方案中背景音乐不改变，从程序运行开始循环播放同一个背景音乐
    //public void PlayLongMusic(string whichMusic)
    //{
    //    switch (whichMusic)
    //    {
    //        case "startGame":
    //            bgMusicPlayer_cgd.clip = startBGMusic_cgd;
    //            bgMusicPlayer_cgd.Play();
    //            break;
    //        case "duringGame":
    //            bgMusicPlayer_cgd.clip = duringGameBGMusic_cgd;
    //            bgMusicPlayer_cgd.Play();
    //            break;
    //        case "endGame":
    //            bgMusicPlayer_cgd.clip = endBGMusic_cgd;
    //            bgMusicPlayer_cgd.Play();
    //            break;
    //    }
    //}
    #endregion

    public void InitPlayerScore()
    {
        imageScoreNum01_cgd.sprite = scoreImage[0];
        imageScoreNum02_cgd.sprite = scoreImage[0];
        imageScoreNum03_cgd.sprite = scoreImage[0];
    }
    public void AddPlayerScore(bool isEntire)
    {
        if (isEntire)
        {
            playerScore_cgd += 10;
        }
        else
        {
            playerScore_cgd += 1;
        }
        if (playerScore_cgd >= 100)
        {
            image01Index_cgd = (playerScore_cgd - playerScore_cgd % 100)/100;
            int indexTemp = playerScore_cgd - image01Index_cgd*100;
            image02Index_cgd = (indexTemp - indexTemp % 10)/10;
            image03Index_cgd = indexTemp % 10;
        }
        else if (playerScore_cgd >= 10)
        {
            image01Index_cgd = 0;
            image02Index_cgd = (playerScore_cgd - playerScore_cgd % 10)/10;
            image03Index_cgd = playerScore_cgd % 10;
        }
        else
        {
            image01Index_cgd = 0;
            image02Index_cgd = 0;
            image03Index_cgd = playerScore_cgd;
        }
        imageScoreNum01_cgd.sprite = scoreImage[image01Index_cgd];
        imageScoreNum02_cgd.sprite = scoreImage[image02Index_cgd];
        imageScoreNum03_cgd.sprite = scoreImage[image03Index_cgd];
    }
    public void SetRank()
    {
        if (playerScore_cgd > firstScore_cgd)
        {
            thirdScore_cgd = secondScore_cgd;
            secondScore_cgd = firstScore_cgd;
            firstScore_cgd = playerScore_cgd;
            //finalText_cgd.sprite = congrafirst_cgd;
            finalText_cgd.text = "恭喜获得冠军，请到服务台领取精美奖品一份！";
        }else if (playerScore_cgd == firstScore_cgd)
        {
            //finalText_cgd.sprite = congrafirst_cgd;
            finalText_cgd.text = "恭喜获得冠军，请到服务台领取精美奖品一份！";
        }
        else if (playerScore_cgd > secondScore_cgd)
        {
            thirdScore_cgd = secondScore_cgd;
            secondScore_cgd = playerScore_cgd;
            //finalText_cgd.sprite = congrasecond_cgd;
            finalText_cgd.text = "恭喜获得亚军，请到服务台领取精美奖品一份！";

        }
        else if (playerScore_cgd == secondScore_cgd)
        {
            //finalText_cgd.sprite = congrasecond_cgd;
            finalText_cgd.text = "恭喜获得亚军，请到服务台领取精美奖品一份！";
        }
        else if (playerScore_cgd >= thirdScore_cgd)
        {
            thirdScore_cgd = playerScore_cgd;
            //finalText_cgd.sprite = congrathird_cgd;
            finalText_cgd.text = "恭喜获得季军，请到服务台领取精美奖品一份！";
        }
        else
        {
            //finalText_cgd.sprite = encourage_cgd;
            finalText_cgd.text = "谢谢参与，请到服务台领取精美奖品一份！";
        }
        firstScoreText_cgd.text = firstScore_cgd.ToString();
        secondScoreText_cgd.text = secondScore_cgd.ToString();
        thirdScoreText_cgd.text = thirdScore_cgd.ToString();
        PlayerPrefs.SetString("1", firstScore_cgd.ToString());
        PlayerPrefs.SetString("2", secondScore_cgd.ToString());
        PlayerPrefs.SetString("3", thirdScore_cgd.ToString());
        playerScore_cgd = 0;
    }
    public void SetNormalOrSmile()
    {
        StartCoroutine(SetChildImage());
    }
    IEnumerator SetChildImage()
    {
        imageNormal_cgd.SetActive(false);
        imageSmile_cgd.SetActive(true);
        yield return new WaitForSeconds(1);
        imageNormal_cgd.SetActive(true);
        imageSmile_cgd.SetActive(false);
    }
    void GetTopThreeScore()
    {
        if (PlayerPrefs.HasKey("3"))
        {
            firstScore_cgd = Convert.ToInt32(PlayerPrefs.GetString("1"));
            secondScore_cgd = Convert.ToInt32(PlayerPrefs.GetString("2"));
            thirdScore_cgd = Convert.ToInt32(PlayerPrefs.GetString("3"));
        }
        else if (PlayerPrefs.HasKey("2"))
        {
            firstScore_cgd = Convert.ToInt32(PlayerPrefs.GetString("1"));
            secondScore_cgd = Convert.ToInt32(PlayerPrefs.GetString("2"));
            thirdScore_cgd = 0;
        }
        else if (PlayerPrefs.HasKey("1"))
        {
            firstScore_cgd = Convert.ToInt32(PlayerPrefs.GetString("1"));
            secondScore_cgd = 0;
            thirdScore_cgd = 0;
        }
        else
        {
            firstScore_cgd = 0;
            secondScore_cgd = 0;
            thirdScore_cgd = 0;
        }
    }
}
