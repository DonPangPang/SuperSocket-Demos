using System;

namespace SuperSocket.Command
{
    public class CommandMetadata
    {
        public string Name { get; private set; }

        public object Key { get; private set; }
/// <summary>
/// ��ʼ��������Ԫ����
/// </summary>
/// <param name="name"></param>
/// <param name="key"></param>
public CommandMetadata(string name, object key)
{
    Name = name;
    Key = key;
}
/// <summary>
/// ��ʼ��������Ԫ����
/// </summary>
/// <param name="name"></param>
public CommandMetadata(string name)
    : this(name, name)
{
            
}
    }
}
