using System;
using System.Threading.Tasks;

namespace SuperSocket.Channel
{
    interface IObjectPipe<T>
    {
        /// <summary>
        /// Write an object into the pipe
        /// ��ܵ���д�����
        /// </summary>
        /// <param name="target">the object tp be added into the pipe. Ҫд��Ķ���</param>
        /// <returns>pipe's length, how many objects left in the pipe. �ܵ��ĳ���</returns>
        int Write(T target);
        /// <summary>
        /// �첽��ȡ����
        /// </summary>
        /// <returns></returns>
        ValueTask<T> ReadAsync();
    }

    interface ISupplyController
    {
        ValueTask SupplyRequired();

        void SupplyEnd();
    }
}
