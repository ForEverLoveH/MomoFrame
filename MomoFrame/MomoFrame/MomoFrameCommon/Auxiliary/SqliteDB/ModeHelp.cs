using System;
using System.Collections;
using System.Collections.Generic;

public class ModeHelp : Attribute
{
    /// <summary>
    /// �Ƿ񴴽��ֶ�
    /// </summary>
   public bool IsCreated { get; set; }

    /// <summary>
    /// ��Ӧ�����ݿ��ֶ�����
    /// </summary>
   public string FieldName { get; set; }

    /// <summary>
    /// ��Ӧ�����ݿ��ֶ�����
    /// </summary>
   public string Type { get; set; }

    /// <summary>
    /// �Ƿ����Ϊ��
    /// </summary>
    public bool IsCanBeNull { get; set; }

    /// <summary>
    /// �Ƿ�Ϊ����
    /// </summary>
    public bool IsPrinaryKey { get; set; }

    /// <summary>
    /// �Ƿ��Զ�����
    /// </summary>
    public bool IsAutomaticIncrease { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isCreated">�Ƿ񴴽��ֶ�</param>
    /// <param name="fieldName">��Ӧ�����ݿ��ֶ�����</param>
    /// <param name="type">��Ӧ�����ݿ��ֶ�����</param>
    /// <param name="isCanBeNull">�Ƿ����Ϊ��</param>
    /// <param name="isprinaryKey">�Ƿ�Ϊ����</param>
    /// <param name="isAutomaticIncrease">�Ƿ��Զ�����(�ֶ����ͱ���Ϊinteger)(����������)</param>
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
