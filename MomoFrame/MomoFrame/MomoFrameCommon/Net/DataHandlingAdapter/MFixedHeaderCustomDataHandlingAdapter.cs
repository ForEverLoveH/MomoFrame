using TouchSocket.Sockets;

public class MFixedHeaderCustomDataHandlingAdapter : CustomFixedHeaderDataHandlingAdapter<MFixedHeaderRequestInfo>
{
    /// <summary>
    /// 接口实现，指示固定包头长度
    /// </summary>
    public override int HeaderLength => 15;

    /// <summary>
    /// 获取新实例
    /// </summary>
    /// <returns></returns>
    protected override MFixedHeaderRequestInfo GetInstance()
    {
        return new MFixedHeaderRequestInfo();
    }
}

public class MFixedHeaderRequestInfo : IFixedHeaderRequestInfo
{
    private int bodyLength;
    /// <summary>
    /// 接口实现，标识数据长度
    /// </summary>
    public int BodyLength
    {
        get { return bodyLength; }
    }

    private byte dataType;
    /// <summary>
    /// 自定义属性，标识数据类型
    /// </summary>
    public byte DataType
    {
        get { return dataType; }
    }

    private byte orderType;
    /// <summary>
    /// 自定义属性，标识指令类型
    /// </summary>
    public byte OrderType
    {
        get { return orderType; }
    }

    private byte[] body;
    /// <summary>
    /// 自定义属性，标识实际数据
    /// </summary>
    public byte[] Body
    {
        get { return body; }
    }


    public bool OnParsingBody(byte[] body)
    {
        if (body.Length == this.bodyLength)
        {
            this.body = body;
            return true;
        }
        return false;
    }


    public bool OnParsingHeader(byte[] header)
    {
        //在该示例中，第一个字节表示后续的所有数据长度，但是header设置的是15，所以后续还应当接收length-14个长度。
        this.bodyLength = header[0] - 14;
        this.dataType = header[1];
        this.orderType = header[2];
        return true;
    }
}
