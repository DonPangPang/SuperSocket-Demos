using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SuperSocket.Channel
{
    public static class SocketExtensions
    {
        /// <summary>
        /// ����Socket�Ĵ������
        /// </summary>
        /// <param name="se">������Ϣ</param>
        /// <returns>true:���ԣ�false:�����ԣ�</returns>
        internal static bool IsIgnorableSocketException(this SocketException se)
        {
            if (se.ErrorCode == 89)
                return true;

            if (se.ErrorCode == 125)
                return true;

            if (se.ErrorCode == 104)
                return true;

            if (se.ErrorCode == 54)
                return true;

            if (se.ErrorCode == 10054)
                return true;

            if (se.ErrorCode == 995)
                return true;

            return false;
        }
    }
}
