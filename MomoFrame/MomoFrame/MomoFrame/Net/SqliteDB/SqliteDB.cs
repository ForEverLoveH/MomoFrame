using PEProtocol;
using PEUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class SqliteDB
{
    public static SqliteDB Instance;

    public void Awake()
    {
        Instance = this;
    }

    //private string DBPath = Application.StartupPath + Constants.DBPath;
    //private string DatabasePath = Application.StartupPath + Constants.DatabasePath;

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Init()
    {
        //if (!Directory.Exists(DBPath))
        //{
        //    Directory.CreateDirectory(DBPath);
        //    Debug.LogError($"Database�ļ����Ѷ�ʧ,�����´��� Database�ļ���·��:{DBPath}");
        //}

        //if (File.Exists(DatabasePath))
        //{
        //    Debug.Log("Database.db�ļ�����");
        //}
        //else
        //{
        //    using (SqlDbCommand sql = new SqlDbCommand(DatabasePath)){}
        //    PELog.Error($"Database.db�ļ�������,�����´���   Database.db�ļ�·��{DatabasePath}");
        //}

        //IsExistenceAdminData("Admin");
    }

    // <summary>
    /// �Ƿ����Admin��
    /// </summary>
    /// <param name="name"></param>
    public void IsExistenceAdminData(string name)
    {
        
        //using (SqlDbCommand sql = new SqlDbCommand(DatabasePath))
        //{
        //    int isExistenceAdminData = sql.IsCreateTable(name);
        //    if (isExistenceAdminData == 0)
        //    {
        //        sql.CreateTable<AdminDBTitle>(name);

        //        //��ʼ��Admin��
        //        Admin admin = new Admin() { User = "Admin", Password = "123456" };
        //        Admin user = new Admin() { User = "User", Password = "123456" };
        //        List<Admin> admins = new List<Admin>();
        //        admins.Add(admin);
        //        admins.Add(user);
        //        sql.Insert<Admin>(admins, name);
        //        Debug.LogError($"���ݿ��{name}������,�ѳ�ʼ��{name}��");

        //    }
        //    if (isExistenceAdminData == 1)
        //    {
        //        Debug.Log($"���ݿ��{name}����");
        //    }
        //}
    }


    public void TestAddSqliteDB(string name)
    {
        //using (SqlDbCommand sql = new SqlDbCommand(DatabasePath))
        //{
        //    List<Admin> admins = new List<Admin>();
        //    Admin admin = new Admin() { User = "Admin", Password = "123456" };
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        admins.Add(admin);
        //    }
        //    sql.Insert<Admin>(admins, name);
        //}

    }

    public void TestQuerySqliteDB(string name)
    {
    //    using (SqlDbCommand sql = new SqlDbCommand(DatabasePath))
    //    {
    //        sql.SelectBySql<Admin>(name);
    //    }
    }

}
