using System.Collections;
using System.Collections.Generic;
using PEUtils;
using System.Text;
using System.Reflection;
using System;
using System.Data.SQLite;
using System.Data.Common;

public class SqlDbCommand : SqlDbConnect
{
    private SQLiteCommand _sqlComm;

    public SqlDbCommand(string dbPath):base(dbPath)
    {
        _sqlComm = new SQLiteCommand(_sqlConn);
        _sqlComm.CommandText = $"vacuum";
        _sqlComm.ExecuteNonQuery();
    }

    #region �����

    /// <summary>
    /// ��ѯ���Ƿ����
    /// </summary>
    /// <param name="name">����</param>
    /// <returns></returns>
    public int IsCreateTable(string name)
    {
        try
        {
            var sql = $"select count(*) as c from sqlite_master where type ='table' and name = '{name}' ";
            _sqlComm.CommandText = sql;
            if (_sqlComm.ExecuteScalar().ToString() == "0")
            {
                return 0;
            }
            else
            {
                return 1;

            }
        }
        catch (System.Exception e)
        {
            PELog.Error($"���ݿ���ѯ�쳣��{e.Message}");
            return -1;
        }
        
    }

    /// <summary>
    /// ������ݿ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">����</param>
    /// <returns></returns>
    public int CreateTable<T>(string name)
    {
        try
        {
            int isCreateTable = IsCreateTable(name);
            if (isCreateTable == 1)
            {
                return 1;
            }
            if (isCreateTable == -1)
            {
                return -1;
            }
            else
            {
                var type = typeof(T);
                var taleName = type.Name;
                var sb = new StringBuilder();
                sb.Append($"create table {name} (");
                var properties = type.GetProperties();
                foreach (var p in properties)
                {
                    var attribute = p.GetCustomAttribute<ModeHelp>();
                    if (attribute.IsCreated)
                    {
                        sb.Append($"{attribute.FieldName} {attribute.Type} ");
                        if (attribute.IsCanBeNull)
                        {
                            sb.Append(" null ");
                        }
                        else
                        {
                            sb.Append(" not null ");
                        }
                        if (attribute.IsPrinaryKey)
                        {
                            sb.Append(" primary key");
                            if (attribute.IsAutomaticIncrease)
                            {
                                if (attribute.Type == "integer")
                                {
                                    sb.Append(" autoincrement");
                                }
                            }
                        }
                        sb.Append(",");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(")");

                _sqlComm.CommandText = sb.ToString();
                return _sqlComm.ExecuteNonQuery();
            }
            
        }
        catch (System.Exception e)
        {
            PELog.Error($"���ݿ�����쳣��{e.Message}");
            return -1;
        }
        
    }

    /// <summary>
    /// ɾ�����ݿ��
    /// </summary>
    /// <param name="name">����</param>
    /// <returns></returns>
    public int DeleteTable(string name)
    {
        try
        {
            var sql = $"drop table {name}";
            _sqlComm.CommandText = sql;
            return _sqlComm.ExecuteNonQuery();
        }
        catch (System.Exception e)
        {

            PELog.Error($"���ݿ��ɾ���쳣��{e.Message}");
            return -1;
        }
        
    }

    /// <summary>
    /// ��ȡ������
    /// </summary>
    /// <param name="name">����</param>
    /// <param name="key">�ֶ���</param>
    /// <returns></returns>
    public object GetKey(string name, string key)
    {
        try
        {
            var sql = $"SELECT max({key}) FROM {name} ";
            _sqlComm.CommandText = sql;
            return _sqlComm.ExecuteScalar();
        }
        catch (System.Exception e)
        {

            PELog.Error($"��ѯKey�쳣��{e.Message}");
            return -1;
        }
    }

    #endregion


    #region ���ݹ���(����)
    /// <summary>
    /// ��������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="name">����</param>
    /// <returns></returns>
    public int Insert<T>(T t,string name) where T : class
    {
        try
        {
            DateTime beforDT = System.DateTime.Now;

            if (t == default(T))
            {
                PELog.Error("Insert()��������!");
                return -1;
            }
            var type = typeof(T);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"INSERT INTO {name} (");

            var propertys = type.GetProperties();
            foreach (var p in propertys)
            {
                if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                {
                    stringBuilder.Append(p.GetCustomAttribute<ModeHelp>().FieldName);
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(") VALUES (");

            foreach (var p in propertys)
            {
                if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                {
                    if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                    {
                        stringBuilder.Append($"'{p.GetValue(t)}'");

                    }
                    else
                    {
                        stringBuilder.Append(p.GetValue(t));

                    }
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(")");

            _sqlComm.CommandText = stringBuilder.ToString();
            int code = _sqlComm.ExecuteNonQuery();

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            PELog.Log($"Sqlite�������� �ܺ�ʱ{ts.TotalMilliseconds}ms.");

            return code;
        }
        catch (System.Exception e)
        {
            PELog.Error($"���ݿ����������쳣<T>(<T>, name)��{e.Message}");
            return -1;
        }
        
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tList"></param>
    /// <param name="name">����</param>
    /// <returns></returns>
    public int Insert<T>(List<T> tList, string name) where T : class
    {
        try
        {
            DateTime beforDT = System.DateTime.Now;


            if (tList == null || tList.Count == 0)
            {
                PELog.Error("Insert()��������!");
                return -1;
            }
            var type = typeof(T);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"INSERT INTO {name} (");

            var propertys = type.GetProperties();
            foreach (var p in propertys)
            {
                if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                {
                    stringBuilder.Append(p.GetCustomAttribute<ModeHelp>().FieldName);
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(") VALUES ");
            foreach (var t in tList)
            {
                stringBuilder.Append("(");
                foreach (var p in propertys)
                {
                    if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                    {
                        if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                        {
                            stringBuilder.Append($"'{p.GetValue(t)}'");

                        }
                        else
                        {
                            stringBuilder.Append(p.GetValue(t));

                        }
                        stringBuilder.Append(",");
                    }
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("),");

            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);


            _sqlComm.CommandText = stringBuilder.ToString();
            int code = _sqlComm.ExecuteNonQuery();

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            PELog.Log($"Sqlite�������� �ܺ�ʱ{ts.TotalMilliseconds}ms.");

            return code;
        }
        catch (System.Exception e)
        {
            PELog.Error($"���ݿ����������쳣<T>(List<T>, name)��{e.Message}");
            return -1;
        }

    }

    #endregion


    #region ���ݹ���(ɾ��)

    /// <summary>
    /// ɾ������
    /// </summary>
    /// <param name="name">����</param>
    /// <param name="sqlWhere"></param>
    public int DeleteBySql(string name, string sqlWhere)
    {
        try
        {
            DateTime beforDT = System.DateTime.Now;

            var sql = $"DELETE FROM {name} where {sqlWhere}";
            _sqlComm.CommandText = sql;
            int code =  _sqlComm.ExecuteNonQuery();

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            PELog.Log($"Sqliteɾ������ �ܺ�ʱ{ts.TotalMilliseconds}ms.");
            return code;
        }
        catch (System.Exception e)
        {
            PELog.Error($"���ݿ������ɾ���쳣��{e.Message}");
            return -1;
        }
        
    }

    #endregion


    #region ���ݹ���(����/�޸�)

    /// <summary>
    /// ���ݸ���/�޸�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="name">����</param>
    /// <param name="sqlWhere"></param>
    /// <returns></returns>
    public int Updete<T>(T t, string name, string sqlWhere) where T : class
    {
        try
        {
            DateTime beforDT = System.DateTime.Now;

            if (t == default(T))
            {
                PELog.Error("Update()��������!");
                return -1;
            }

            var type = typeof(T);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE {name} set ");
            var propertys = type.GetProperties();

            foreach (var p in propertys)
            {
                stringBuilder.Append($"{p.GetCustomAttribute<ModeHelp>().FieldName} = ");
                if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                {
                    stringBuilder.Append($"'{p.GetValue(t)}'");

                }
                else
                {
                    stringBuilder.Append(p.GetValue(t));

                }
                stringBuilder.Append(",");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append($" where {sqlWhere}");

            _sqlComm.CommandText = stringBuilder.ToString();
            int code = _sqlComm.ExecuteNonQuery();

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            PELog.Log($"Sqlite���ݸ���/�޸� �ܺ�ʱ{ts.TotalMilliseconds}ms.");

            return code;
        }
        catch (Exception e)
        {
            PELog.Error($"���ݿ�����ݸ����쳣��{e.Message}");
            return -1;
        }
        
    }

    #endregion

    #region ���ݹ���(��ѯ)

    /// <summary>
    /// ���ݲ�ѯ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">����</param>
    /// <param name="sqlWhere"></param>
    /// <returns></returns>
    public List<T> SelectBySql<T>(string name, string sqlWhere = "") where T : class
    {
        DateTime beforDT = System.DateTime.Now;

        var ret = new List<T>();
        string sql;
        if (string.IsNullOrEmpty(sqlWhere))
        {
            sql = $"SELECT * FROM {name}";
        }
        else
        {
            sql = $"SELECT * FROM {name} where {sqlWhere}";

        }
        _sqlComm.CommandText= sql;
        var dr = _sqlComm.ExecuteReader();
        if (dr != null)
        {
            while (dr.Read())
            {
                ret.Add(DataReaderToData<T>(dr));
            }
        }

        DateTime afterDT = System.DateTime.Now;
        TimeSpan ts = afterDT.Subtract(beforDT);
        PELog.Log($"Sqlite���ݲ�ѯ �ܺ�ʱ{ts.TotalMilliseconds}ms.");

        return ret;
    }
    
    private T DataReaderToData<T>(SQLiteDataReader dr) where T : class
    {
        try
        {
            List<string> fieldNames = new List<string>();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                fieldNames.Add(dr.GetName(i));
            }

            var type = typeof(T);
            T data = Activator.CreateInstance<T>();
            var properties = type.GetProperties();

            foreach (var p in properties)
            {
                if (!p.CanWrite) continue;
                var fieldName = p.GetCustomAttribute<ModeHelp>().FieldName;
                if (fieldName.Contains(fieldName) && p.GetCustomAttribute<ModeHelp>().IsCreated)
                {
                    p.SetValue(data, dr[fieldName]);
                }
            }
            return data;
        }
        catch (System.Exception e)
        {
            PELog.Error($"DataReaderToData()ת������, ����{typeof(T).Name}����, ������Ϣ��{e.Message}");
            return null;
        }
    }

    #endregion

}
