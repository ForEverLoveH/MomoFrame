using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public class IniFile
{
    private string m_FileName;

    public string FileName
    {
        get { return m_FileName; }
        set { m_FileName = value; }
    }


    /// <summary>
    /// 调用动态库链接读取int值
    /// </summary>
    /// <param name="lpAppName">ini节名</param>
    /// <param name="lpKeyName">ini键名</param>
    /// <param name="nDefault">默认值: 当无对应键值,则返回改值</param>
    /// <param name="lpFileName">缓冲区大小</param>
    /// <returns></returns>
    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileInt(
        byte[] lpAppName,
        byte[] lpKeyName,
        int nDefault,
        string lpFileName
        );

    /// <summary>
    /// 调用动态库链接读取值
    /// </summary>
    /// <param name="lpAppName">ini节名</param>
    /// <param name="lpKeyName">ini键名</param>
    /// <param name="lpDefault">默认值: 当无对应键值,则返回改值</param>
    /// <param name="lpReturnedString">结果缓冲区</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <param name="lpFileName"></param>
    /// <returns></returns>
    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(
        byte[] lpAppName,
        byte[] lpKeyName,
        byte[] lpDefault,
        byte[] lpReturnedString,
        int nSize,
        string lpFileName
        );

    /// <summary>
    /// 调用动态库链接写入值
    /// </summary>
    /// <param name="lpAppName">ini节名</param>
    /// <param name="lpKeyName">ini键名</param>
    /// <param name="lpString">写入值</param>
    /// <param name="lpFileName">文件位置</param>
    /// <returns>0: 写入失败 1:写入成功</returns>
    [DllImport("kernel32.dll")]
    private static extern int WritePrivateProfileString(
        byte[] mpAppName,
        byte[] mpKeyName,
        byte[] mpString,
        string mpFileName
        );

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="aFileName">Ini文件路径</param>
    public IniFile(string aFileName)
    {
        this.m_FileName = aFileName;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public IniFile()
    { }

    /// <summary> 
    /// 验证文件是否存在 
    /// </summary> 
    /// <returns>布尔值</returns> 
    public bool ExistIniFile()
    {
        return File.Exists(m_FileName);
    }

    /// <summary>
    /// 根据文件名创建文件
    /// </summary>
    /// <returns></returns>
    public bool IniCreat()
    {
        try
        {
            if (!File.Exists(m_FileName))
            {
                FileStream _fs = File.Create(m_FileName);
                _fs.Close();
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    /// <summary>
    /// 与ini交互必须统一编码格式
    /// </summary>
    /// <param name="s"></param>
    /// <param name="encodingName">编码格式名称</param>
    /// <returns></returns>
    private static byte[] getBytes(string s, string encodingName)
    {
        return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
    }

    /// <summary>
    /// 节名下是否存在该键名
    /// </summary>
    /// <param name="section">ini节名</param>
    /// <param name="name">ini键名</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <returns></returns>
    public bool IsKeyName(string section, string name, string encodingName = "utf-8", int nSize = 256)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(null, encodingName), buffer, nSize, this.m_FileName);
        if (string.IsNullOrEmpty(Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim()))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// [扩展]读Int数值
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="def">默认值</param>
    /// <param name="encodingName">编码</param>
    /// <returns></returns>
    public int ReadInt(string section, string name, int def, string encodingName = "utf-8")
    {
        return GetPrivateProfileInt(getBytes(section, encodingName), getBytes(name, encodingName), def, this.m_FileName);
    }

    /// <summary>
    /// [扩展]读取string字符串
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="def">默认值</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <returns></returns>
    public string ReadString(string section, string name, string def, string encodingName = "utf-8", int nSize = 2048)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(def, encodingName), buffer, nSize, this.m_FileName);
        return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
    }

    /// <summary>
    /// [扩展]读取double字符串
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="def">默认值</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <returns></returns>
    public double ReadDouble(string section, string name, double def, string encodingName = "utf-8", int nSize = 2048)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(null, encodingName), buffer, nSize, this.m_FileName);
        string str = Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        if (string.IsNullOrEmpty(str))
        {
            return def;
        }
        double numDouble;
        if (double.TryParse(str, out numDouble))
        {
            return numDouble;
        }
        else
        {
            return def;
        }
    }

    /// <summary>
    /// [扩展]读取bool字符串
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <returns>-1: 不是Bool值 读取错误 0: false 1: true</returns>
    public int ReadBool(string section, string name, string encodingName = "utf-8", int nSize = 2048)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(null, encodingName), buffer, nSize, this.m_FileName);
        string str = Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        if (string.IsNullOrEmpty(str))
        {
            return -1;
        }
        if (str == "True" || str == "true" || str == "TRUE")
        {
            return 1;
        }
        if (str == "False" || str == "false" || str == "FALSE")
        {
            return 0;
        }
        return -1;
    }


    /// <summary>
    /// [扩展]读取List<string>
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="separator">分割符</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <returns>null: 没有数据</returns>
    public List<string> ReadStringList(string section, string name, char separator = ',', string encodingName = "utf-8", int nSize = 2048)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(null, encodingName), buffer, nSize, this.m_FileName);
        string str = Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }
        string[] strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 0)
        {
            return null;
        }
        List<string> strlist = new List<string>();
        foreach (string item in strArray)
        {
            strlist.Add(item);
        }
        return strlist;
    }

    /// <summary>
    /// [扩展]读取List<int>
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="separator">分割符</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <returns>null: 没有数据或格式不正确</returns>
    public List<int> ReadIntList(string section, string name, char separator = ',', string encodingName = "utf-8", int nSize = 2048)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(null, encodingName), buffer, nSize, this.m_FileName);
        string str = Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }
        string[] strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 0)
        {
            return null;
        }
        List<int> strlist = new List<int>();
        int result;
        foreach (string item in strArray)
        {
            if (!int.TryParse(item, out result))
            {
                return null;
            }
            else
            {
                strlist.Add(result);
            }
        }
        return strlist;
    }

    /// <summary>
    /// [扩展]读取路径字符串
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="def">默认值</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <returns></returns>
    public string ReadPath(string section, string name, string def = null, string encodingName = "utf-8", int nSize = 2048)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(def, encodingName), buffer, nSize, this.m_FileName);
        string Path = Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        if (string.IsNullOrEmpty(Path))
        {
            return null;
        }
        if (Path.StartsWith("{App}"))
        {
            return AppDomain.CurrentDomain.BaseDirectory + Path.Substring(5);
        }
        else if (Path.StartsWith("{File}"))
        {
            int RightSlashIndex = m_FileName.LastIndexOf("\\");
            int LeftSlashIndex = m_FileName.LastIndexOf("/");
            if (RightSlashIndex > LeftSlashIndex)
            {
                return m_FileName.Substring(0, RightSlashIndex + 1) + Path.Substring(6);
            }
            if (RightSlashIndex < LeftSlashIndex)
            {
                return m_FileName.Substring(0, LeftSlashIndex + 1) + Path.Substring(6);
            }
            return null;
        }
        else if (Path.StartsWith("$"))
        {
            return Path.Substring(1);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// [扩展]写入Int数值，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="Ival">写入值</param>
    /// <param name="encodingName">编码格式名称</param>
    public void WriteInt(string section, string name, int Ival, string encodingName = "utf-8")
    {

        WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(Ival.ToString(), encodingName), this.m_FileName);
    }

    /// <summary>
    /// [扩展]写入String字符串，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="strVal">写入值</param>
    /// <param name="encodingName">编码格式名称</param>
    public void WriteString(string section, string name, string strVal, string encodingName = "utf-8")
    {
        WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(strVal, encodingName), this.m_FileName);
    }

    /// <summary>
    /// [扩展]写入Double字符串，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="dVal">写入值</param>
    /// <param name="encodingName">编码格式名称</param>
    public void WriteDouble(string section, string name, double dVal, string encodingName = "utf-8")
    {
        WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(dVal.ToString(), encodingName), this.m_FileName);
    }

    /// <summary>
    /// [扩展]写入Bool字符串，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="strVal">写入值</param>
    /// <param name="encodingName">编码格式名称</param>
    public void WriteBool(string section, string name, bool bVal, string encodingName = "utf-8")
    {
        string strVal = bVal ? "true" : "false";
        WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(strVal, encodingName), this.m_FileName);
    }

    /// <summary>
    /// [扩展]写入List<string>，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="strlist">写入List<string></param>
    /// <param name="separator">分隔符</param>
    /// <param name="encodingName">编码格式名称</param>
    public void WriteStringList(string section, string name, List<string> strlist, char separator = ',', string encodingName = "utf-8")
    {
        if (strlist.Count == 0)
        {
            return;
        }
        StringBuilder strVal = new StringBuilder();
        for (int i = 0; i < strlist.Count; i++)
        {
            if (i != strlist.Count - 1)
            {
                strVal.Append(strlist[i] + separator);
            }
            else
            {
                strVal.Append(strlist[i]);
            }
        }
        WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(strVal.ToString(), encodingName), this.m_FileName);
    }

    /// <summary>
    /// [扩展]写入List<int>，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="intlist">写入List<int></param>
    /// <param name="separator">分隔符</param>
    /// <param name="encodingName">编码格式名称</param>
    public void WriteIntList(string section, string name, List<int> intlist, char separator = ',', string encodingName = "utf-8")
    {
        if (intlist.Count == 0)
        {
            return;
        }
        StringBuilder strVal = new StringBuilder();
        for (int i = 0; i < intlist.Count; i++)
        {
            if (i != intlist.Count - 1)
            {
                strVal.Append(intlist[i].ToString() + separator);
            }
            else
            {
                strVal.Append(intlist[i].ToString());
            }
        }
        WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(strVal.ToString(), encodingName), this.m_FileName);
    }

    public enum PathType
    {
        AbsolutePath,
        AppPath,
        FilePath
    }

    /// <summary>
    /// [扩展]写入路径字符串，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="strVal">写入值</param>
    /// <param name="encodingName">编码格式名称</param>
    public void WritePath(string section, string name, string strVal, PathType pathType = PathType.AppPath, string encodingName = "utf-8")
    {
        switch (pathType)
        {
            case PathType.AbsolutePath:
                strVal = "$" + strVal;
                WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(strVal, encodingName), this.m_FileName);
                break;
            case PathType.AppPath:
                strVal = "{App}" + strVal;
                WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(strVal, encodingName), this.m_FileName);
                break;
            case PathType.FilePath:
                strVal = "{File}" + strVal;
                WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(strVal, encodingName), this.m_FileName);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 删除指定的 节
    /// </summary>
    /// <param name="section"></param>
    /// <param name="encodingName">编码格式名称</param>
    public void DeleteSection(string section, string encodingName = "utf-8")
    {
        WritePrivateProfileString(getBytes(section, encodingName), null, null, this.m_FileName);
    }

    /// <summary>
    /// 删除全部 节
    /// </summary>
    public void DeleteAllSection()
    {
        WritePrivateProfileString(null, null, null, this.m_FileName);
    }

    /// <summary>
    /// 读取指定 节-键 的值
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="nSize">缓冲区大小</param>
    /// <param name="encodingName">编码格式名称</param>
    /// <returns></returns>
    public string IniReadValue(string section, string name, string encodingName = "utf-8", int nSize = 256)
    {
        byte[] buffer = new byte[nSize];
        int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(null, encodingName), buffer, nSize, this.m_FileName);
        return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
    }

    /// <summary>
    /// 写入指定值，如果不存在 节-键，则会自动创建
    /// </summary>
    /// <param name="section">节</param>
    /// <param name="name">键</param>
    /// <param name="value">写入值</param>
    /// <param name="encodingName">编码格式名称</param>
    public void IniWriteValue(string section, string name, string value, string encodingName = "utf-8")
    {
        WritePrivateProfileString(getBytes(section, encodingName), getBytes(name, encodingName), getBytes(value, encodingName), this.m_FileName);
    }
}
