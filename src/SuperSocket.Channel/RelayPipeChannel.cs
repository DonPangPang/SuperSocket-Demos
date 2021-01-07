using System;
using System.Buffers;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Pipelines;
using SuperSocket.ProtoBase;

namespace SuperSocket.Channel
{
    public class RelayPipeChannel<TPackageInfo> : PipeChannel<TPackageInfo>
        where TPackageInfo : class
    {
/// <summary>
/// ���½����ܵ���ѡ������
/// </summary>
/// <param name="options">ѡ��</param>
/// <param name="pipeIn">�ܵ����</param>
/// <param name="pipeOut">�ܵ�����</param>
/// <returns>�ܵ�ѡ������</returns>
static ChannelOptions RebuildOptionsWithPipes(ChannelOptions options, Pipe pipeIn, Pipe pipeOut)
{
    options.In = pipeIn;
    options.Out = pipeOut;
    return options;
}
/// <summary>
/// ���½����ܵ�
/// </summary>
/// <param name="pipelineFilter">������</param>
/// <param name="options">ѡ������</param>
/// <param name="pipeIn">�ܵ����</param>
/// <param name="pipeOut">�ܵ�����</param>
public RelayPipeChannel(IPipelineFilter<TPackageInfo> pipelineFilter, ChannelOptions options, Pipe pipeIn, Pipe pipeOut)
    : base(pipelineFilter, RebuildOptionsWithPipes(options, pipeIn, pipeOut))
{

}
/// <summary>
/// �رչܵ�����
/// </summary>
protected override void Close()
{
    In.Writer.Complete();
    Out.Writer.Complete();
}
/// <summary>
/// ����������д��ܵ�
/// </summary>
/// <param name="buffer">ֻ���ַ�����</param>
/// <param name="cancellationToken">�첽������ʶToken</param>
/// <returns>д��ĳ���</returns>
protected override async ValueTask<int> SendOverIOAsync(ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
{
    var writer = Out.Writer;
    var total = 0;

    foreach (var data in buffer)
    {
        var result = await writer.WriteAsync(data, cancellationToken);

        if (result.IsCompleted)
            total += data.Length;
        else if (result.IsCanceled)
            break;
    }

    return total;
}
/// <summary>
/// ���ݳ����ܵ�
/// </summary>
/// <param name="memory">�ڴ�</param>
/// <param name="cancellationToken">�첽������ʶToken</param>
/// <returns></returns>
protected override ValueTask<int> FillPipeWithDataAsync(Memory<byte> memory, CancellationToken cancellationToken)
{
    throw new NotSupportedException();
}
    }
}