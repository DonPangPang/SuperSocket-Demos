using System.Collections.Generic;
using System.Text;
using SuperSocket.Channel;

namespace SuperSocket
{
public class ServerOptions : ChannelOptions
{
/// <summary>
/// ����
/// </summary>
public string Name { get; set; }
/// <summary>
/// �����Ķ˿��б�
/// </summary>
public List<ListenOptions> Listeners { get; set; }
/// <summary>
/// Ĭ���ı�����
/// </summary>
public Encoding DefaultTextEncoding { get; set; }
/// <summary>
/// ������õ�Sessionʱ����
/// </summary>
public int ClearIdleSessionInterval { get; set; } = 120;
/// <summary>
/// ����Session��ʱ
/// </summary>
public int IdleSessionTimeOut { get; set; } = 300;
}
}