using System;
using System.Buffers;
using System.Text;
using SuperSocket.ProtoBase;

namespace SuperSocket.WebSocket
{
    public static partial class ExtensionMethods
    {
        private readonly static char[] m_CrCf = new char[] { '\r', '\n' };

        /// <summary>
        /// Appends in the format with CrCf as suffix.
        /// ��CrCf��Ϊ��׺��ʽ׷��
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The arg.</param>
        public static void AppendFormatWithCrCf(this StringBuilder builder, string format, object arg)
        {
            builder.AppendFormat(format, arg);
            builder.Append(m_CrCf);
        }

        /// <summary>
        /// Appends in the format with CrCf as suffix.
        /// ��CrCf��Ϊ��׺��ʽ׷��
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public static void AppendFormatWithCrCf(this StringBuilder builder, string format, params object[] args)
        {
            builder.AppendFormat(format, args);
            builder.Append(m_CrCf);
        }

        /// <summary>
        /// Appends with CrCf as suffix.
        /// ��CrCf��Ϊ��׺��ʽ׷��
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="content">The content.</param>
        public static void AppendWithCrCf(this StringBuilder builder, string content)
        {
            builder.Append(content);
            builder.Append(m_CrCf);
        }

        /// <summary>
        /// Appends with CrCf as suffix.
        /// ��CrCf��Ϊ��׺��ʽ׷��
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void AppendWithCrCf(this StringBuilder builder)
        {
            builder.Append(m_CrCf);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="seq">����</param>
        /// <returns></returns>
        internal static ReadOnlySequence<byte> CopySequence(ref this ReadOnlySequence<byte> seq)
        {
            SequenceSegment head = null;
            SequenceSegment tail = null;

            foreach (var segment in seq)
            {                
                var newSegment = SequenceSegment.CopyFrom(segment);

                if (head == null)
                    tail = head = newSegment;
                else
                    tail = tail.SetNext(newSegment);
            }

            return new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="first"></param>
        /// <returns></returns>
        internal static (SequenceSegment, SequenceSegment) DestructSequence(ref this ReadOnlySequence<byte> first)
        {
            SequenceSegment head = first.Start.GetObject() as SequenceSegment;
            SequenceSegment tail = first.End.GetObject() as SequenceSegment;
            
            if (head == null)
            {
                foreach (var segment in first)
                {                
                    if (head == null)
                        tail = head = new SequenceSegment(segment);
                    else
                        tail = tail.SetNext(new SequenceSegment(segment));
                }
            }

            return (head, tail);
        }
        /// <summary>
        /// ƴ������
        /// </summary>
        /// <param name="first">��һ������</param>
        /// <param name="second">�ڶ�������</param>
        /// <returns></returns>
        internal static ReadOnlySequence<byte> ConcatSequence(ref this ReadOnlySequence<byte> first, ref ReadOnlySequence<byte> second)
        {
            var (head, tail) = first.DestructSequence();

            if (!second.IsEmpty)
            {
                foreach (var segment in second)
                {
                    tail = tail.SetNext(new SequenceSegment(segment));
                }
            }

            return new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
        }
        /// <summary>
        /// ƴ������
        /// </summary>
        /// <param name="first">��һ������</param>
        /// <param name="segment">���ж�</param>
        /// <returns></returns>
        internal static ReadOnlySequence<byte> ConcatSequence(ref this ReadOnlySequence<byte> first, SequenceSegment segment)
        {
            var (head, tail) = first.DestructSequence();
            tail = tail.SetNext(segment);
            return new ReadOnlySequence<byte>(head, 0, tail, tail.Memory.Length);
        }
    }
}
