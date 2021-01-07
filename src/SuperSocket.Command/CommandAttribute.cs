using System;

namespace SuperSocket.Command
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        public string Name { get; set; }

        public object Key { get; set; }

        public CommandAttribute()
        {

        }
        /// <summary>
        /// ��ʼ������������(Attribute)
        /// </summary>
        /// <param name="name"></param>
        public CommandAttribute(string name)
        {

        }
        /// <summary>
        /// ��ʼ������������(Attribute)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        public CommandAttribute(string name, object key)
            : this(name)
        {
            Key = key;
        }
    }
}
