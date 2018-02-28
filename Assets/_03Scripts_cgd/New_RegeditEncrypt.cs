using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;

public class New_RegeditEncrypt : MonoBehaviour { 
    private int i = 0;
    private bool _isCommand = false;
    private bool isCommand
    {
        set
        {
            _isCommand = value;
        }
        get
        {
            return _isCommand;
        }
    }
    private bool isReceive = false;
    private bool isDay = false;
    private int value = 0;

    private string tip = "";
    private bool isTip = false;
    private float tipTime = 0f;
    public bool isDebug = false;
    private bool passed = false;
    private int showDepth = -1;
    public Rect showInfoRect = new Rect(12, 100, 1000, 300);
    private string showInfo;
    public float deadTime = 10;
    private float deadTimeCount;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        EncryptString obj = JSON.JsonDecode(Decrypt(PlayerPrefs.GetString("encrypt", ""), "19911221", "19901125"));
        if (obj == null)
        {
            obj = new EncryptString(GetMac(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.Date.AddDays(31).ToShortDateString());
            PlayerPrefs.SetString("encrypt", Encrypt(JSON.JsonEncode(obj), "19911221", "19901125"));
        }
        passed = ValidateEncrypt(obj);
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-encrypt-debug")
            {
                isDebug = true;
            }
        }
    }

    public class JSON
    {
        public static EncryptString JsonDecode(string s)
        {
            return JsonUtility.FromJson<EncryptString>(s);
        }

        public static string JsonEncode(object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }

    public class EncryptString
    {
        public enum EncryptType
        {
            forever,
            day,
            time
        }
        public string mac;
        public EncryptType type;
        public string last;
        public string target;
        public int value;
        public bool serverEnable = true;

        public EncryptString(string mac)
        {
            this.mac = mac;
            type = EncryptType.forever;
        }

        public EncryptString(string mac, string last, string target)
        {
            this.mac = mac;
            this.last = last;
            this.target = target;
            type = EncryptType.day;
        }

        public EncryptString(string mac, int value)
        {
            this.mac = mac;
            this.value = value;
            type = EncryptType.time;
        }
    }

    bool ValidateEncrypt(EncryptString obj)
    {
        if (obj != null)
        {
            if (obj.mac != GetMac())
            {
                if (isDebug)
                    Debug.Log("Mac changed");
            }
            if (obj.type == EncryptString.EncryptType.forever)
            {
                if (isDebug)
                    Debug.Log("this is a forever-encrypt");
                return true;
            }
            else if (obj.type == EncryptString.EncryptType.day)
            {
                DateTime last = DateTime.Parse(obj.last);
                DateTime target = DateTime.Parse(obj.target);
                int value = 0;
                if ((target.Date - last.Date).Days > 0)
                {
                    if ((DateTime.Now.Date - last.Date).Days >= 0)
                    {
                        if ((target.Date - DateTime.Now.Date).Days > 0)
                        {
                            obj.last = DateTime.Now.Date.ToShortDateString();
                            value = (target.Date - DateTime.Now.Date).Days;
                            PlayerPrefs.SetString("encrypt", Encrypt(JSON.JsonEncode(obj), "19911221", "19901125"));
                            if (isDebug)
                                Debug.Log(string.Format("this is a day-encrypt，{0} days remaining", value));
                            return true;
                        }
                        else
                        {
                            obj.last = target.Date.ToShortDateString();
                            PlayerPrefs.SetString("encrypt", Encrypt(JSON.JsonEncode(obj), "19911221", "19901125"));
                        }

                    }
                    else
                    {
                        if (isDebug)
                            Debug.Log("system date changed");
                    }
                }
            }
            else if (obj.type == EncryptString.EncryptType.time)
            {
                int value = obj.value;
                if (isDebug)
                    Debug.Log(string.Format("this is a time-encrypt，{0} time remaining", value));
                if (value > 0)
                {
                    obj.value = value - 1;
                    PlayerPrefs.SetString("encrypt", Encrypt(JSON.JsonEncode(obj), "19911221", "19901125"));
                    return true;
                }
            }
        }
        else
        {
            if (isDebug)
                Debug.Log("encrypt is not exists");
        }
        if (isDebug)
            Debug.Log("encrypt is invalid");
        return false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!passed)
        {
            deadTimeCount += Time.deltaTime;
            if (deadTimeCount > deadTime)
            {
                Debug.Log("Destroy Exe");
                Application.Quit();
            }
        }
        KeyUpdate();
    }

    void KeyUpdate()
    {
        if(isTip)
        {
            if (Time.realtimeSinceStartup - 1.5f > tipTime)
            {
                isTip = false;
            }
        }
        if (isCommand)
        {
            if (isReceive == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    SetTip("forever encrypt complete");
                    EncryptString es = new EncryptString(GetMac());

                    PlayerPrefs.SetString("encrypt", Encrypt(JSON.JsonEncode(es),"19911221","19901125"));
                    isCommand = false;
                    passed = true;
                    deadTimeCount = 0f;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    isDay = true;
                    isReceive = true;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    isDay = false;
                    isReceive = true;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    SetTip("encrypt is already clear");
                    isCommand = false;
                    PlayerPrefs.DeleteKey("encrypt");
                    passed = false;
                    deadTimeCount = 0f;
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SetTip("command canceled");
                    isCommand = false;
                    isReceive = false;
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Input.GetKeyDown(i.ToString()))
                    {
                        value = value * 10 + i;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    value = value / 10;
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SetTip("command canceled");
                    isCommand = false;
                    value = 0;
                    isReceive = false;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SetTip(string.Format("use {0}, value={1}, complete", isDay ? "day-encrypt" : "time-encrypt", value));

                    if (isDay)
                    {
                        EncryptString es = new EncryptString(GetMac(), DateTime.Now.Date.ToShortDateString(), DateTime.Now.Date.AddDays(value).ToShortDateString());
                        PlayerPrefs.SetString("encrypt", Encrypt(JSON.JsonEncode(es), "19911221", "19901125"));
                    }
                    else
                    {
                        EncryptString es = new EncryptString(GetMac(), value);
                        PlayerPrefs.SetString("encrypt", Encrypt(JSON.JsonEncode(es), "19911221", "19901125"));
                    }
                    isCommand = false;
                    isReceive = false;
                    if (value > 0)
                    {
                        passed = true;
                        deadTimeCount = 0f;
                    }
                    else
                    {
                        passed = false;
                        deadTimeCount = 0f;
                    }
                    value = 0;
                }
            }
            return;
        }
        if (Input.GetKey(KeyCode.P))
        {
            if (i == 0 && Input.GetKeyDown(KeyCode.Q))
            {
                i = 1;
                return;
            }
            if (i == 1 && Input.GetKeyDown(KeyCode.W))
            {
                i = 2;
                return;
            }
            if (i == 2 && Input.GetKeyDown(KeyCode.E))
            {
                i = 3;
                return;
            }
            if (i == 3 && Input.GetKeyDown(KeyCode.R))
            {
                i = 4;
                isCommand = true;
            }
        }

        if (Input.anyKeyDown)
        {
            i = 0;
        }
    }

    void OnGUI()
    {
        GUI.depth = showDepth;
        if (isTip)
        {
            GUILayout.Label(new GUIContent(tip));
        }
        if (isCommand)
        {
            if (!isReceive)
                GUILayout.Label(new GUIContent("command is open, please select encrypt type, 0:forever-encrypt, 1:day-encrypt, 2:time-encrypt, 3:clear encrypt, Escape:cancel"));
            else
                GUILayout.Label(new GUIContent(string.Format("{0}, please input value，value={1} ,Num:input, Enter:ensure, Backspace:backspace,  Escape:cancel",isDay ? "day-encrypt" : "time-encrypt", value)));
        }

        if (!passed)
        {
            GUI.Label(showInfoRect, showInfo);
            GUI.Label(new Rect(0, 0, 450, 100), "Quit at: " + (deadTime - deadTimeCount).ToString("0"));
        }
    }

    void SetTip(string tip)
    {
        this.tip = tip;
        isTip = true;
        tipTime = Time.realtimeSinceStartup;
    }

    /// <summary>
	/// Gets the mac address.
	/// </summary>
	/// <returns>The mac.</returns>
	public static string GetMac()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        if (nics == null || nics.Length < 1)
            return "";
        foreach (NetworkInterface adapter in nics)
        {
            if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                PhysicalAddress address = adapter.GetPhysicalAddress();
                return address.ToString();
            }
        }
        foreach (NetworkInterface adapter in nics)
        {
            if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            {
                PhysicalAddress address = adapter.GetPhysicalAddress();
                return address.ToString();
            }
        }
        return "";
    }

    public string Encrypt(string sourceString, string key, string iv)
    {
        try
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIV = Encoding.UTF8.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                    return sourceString;
                }
            }
        }
        catch { }

        return "DES加密出错";
    }

    public string Decrypt(string encryptedString, string key, string iv)
    {
        byte[] btKey = Encoding.UTF8.GetBytes(key);
        byte[] btIV = Encoding.UTF8.GetBytes(iv);
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        using (MemoryStream ms = new MemoryStream())
        {
            byte[] inData = Convert.FromBase64String(encryptedString);
            try
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                {
                    cs.Write(inData, 0, inData.Length);
                    cs.FlushFinalBlock();
                }

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return encryptedString;
            }
        }
    }

    IEnumerator GetTokenFromServer(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            EncryptString es = JsonUtility.FromJson<EncryptString>(www.text);
            if (es.serverEnable == false)
            {
                PlayerPrefs.DeleteKey("encrypt");
                passed = false;
                isCommand = false;
                deadTimeCount = 0f;
            }
        }
    }
}
