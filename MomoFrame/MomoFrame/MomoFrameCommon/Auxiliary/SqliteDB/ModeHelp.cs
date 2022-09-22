using System;
using System.Collections;
using System.Collections.Generic;

public class ModeHelp : Attribute
{
    /// <summary>
    /// 是否创建字段
    /// </summary>
   public bool IsCreated { get; set; }

    /// <summary>
    /// 对应到数据库字段名称
    /// </summary>
   public string FieldName { get; set; }

    /// <summary>
    /// 对应到数据库字段类型
    /// </summary>
   public string Type { get; set; }

    /// <summary>
    /// 是否可以为空
    /// </summary>
    public bool IsCanBeNull { get; set; }

    /// <summary>
    /// 是否为主键
    /// </summary>
    public bool IsPrinaryKey { get; set; }

    /// <summary>
    /// 是否自动替增
    /// </summary>
    public bool IsAutomaticIncrease { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isCreated">是否创建字段</param>
    /// <param name="fieldName">对应到数据库字段名称</param>
    /// <param name="type">对应到数据库字段类型</param>
    /// <param name="isCanBeNull">是否可以为空</param>
    /// <param name="isprinaryKey">是否为主键</param>
    /// <param name="isAutomaticIncrease">是否自动替增(字段类型必须为integer)(必须是主键)</param>
    public ModeHelp(bool isCreated, string fieldName, string type, bool isCanBeNull = false, bool isprinaryKey = false, bool isAutomaticIncrease = false)
    {
        IsCreated = isCreated;
        FieldName = fieldName;
        Type = type;
        IsCanBeNull = isCanBeNull;
        IsPrinaryKey = isprinaryKey;
        IsAutomaticIncrease = isAutomaticIncrease;
    }
}
