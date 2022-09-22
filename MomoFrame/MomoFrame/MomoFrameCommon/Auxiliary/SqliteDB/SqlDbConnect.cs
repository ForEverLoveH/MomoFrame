using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using PEUtils;

/// <summary>
/// Sqlite连接类
/// </summary>
public class SqlDbConnect : IDisposable
{
    protected SQLiteConnection _sqlConn;

    public SqlDbConnect(string dbPath)
    {
        if (!File.Exists(dbPath))
        {
            CreateDbSqlite(dbPath);
        }
        ConnectDbSqlite(dbPath);
    }

    /// <summary>
    /// 创建数据库
    /// </summary>
    /// <param name="dbPath"></param>
    /// <returns></returns>
    protected bool CreateDbSqlite(string dbPath)
    {
        try
        {
            var dirName = new FileInfo(dbPath).Directory.FullName;
            if(!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }

            SQLiteConnection.CreateFile(dbPath);
            return true;
        }
        catch (System.Exception e)
        {

            PELog.Error($"数据库创建异常：{e.Message}");
            return false;
        }

    }

    

    /// <summary>
    /// 连接数据库
    /// </summary>
    /// <param name="dbPath"></param>
    /// <returns></returns>
    private bool ConnectDbSqlite(string dbPath)
    {
        try
        {
            _sqlConn = new SQLiteConnection(new SQLiteConnectionStringBuilder() { DataSource = dbPath}.ToString());
            _sqlConn.Open();
            return true;
        }
        catch (System.Exception e)
        {
            PELog.Error($"数据库连接异常：{e.Message}");
            return false;
        }
    }

    /// <summary>
    /// 释放数据库连接
    /// </summary>
    public void Dispose()
    {
        _sqlConn?.Close();
        _sqlConn?.Dispose();
    }
}
