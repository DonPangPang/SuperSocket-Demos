using System;
using System.Threading.Tasks;
using System.Text.Json;
using SuperSocket.ProtoBase;
using Microsoft.Extensions.DependencyInjection;

namespace SuperSocket.Command
{
    interface ICommandWrap
    {
        ICommand InnerCommand { get; }
    }

    class CommandWrap<TAppSession, TPackageInfo, IPackageInterface, TCommand> : ICommand<TAppSession, TPackageInfo>, ICommandWrap
        where TAppSession : IAppSession
        where TPackageInfo : IPackageInterface
        where TCommand : ICommand<TAppSession, IPackageInterface>
    {
        public TCommand InnerCommand { get; }
        /// <summary>
        /// ��ʼ�������װ
        /// </summary>
        /// <param name="command">����</param>
        public CommandWrap(TCommand command)
        {
            InnerCommand = command;
        }
        /// <summary>
        /// ��ʼ�������װ
        /// </summary>
        /// <param name="serviceProvider">ServiceProvider</param>
        public CommandWrap(IServiceProvider serviceProvider)
        {
            InnerCommand = (TCommand)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TCommand));
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="package">���ݰ�</param>
        public void Execute(TAppSession session, TPackageInfo package)
        {
            InnerCommand.Execute(session, package);
        }
        /// <summary>
        /// ��������
        /// </summary>
        ICommand ICommandWrap.InnerCommand
        {
            get { return InnerCommand; }
        }
    }

    class AsyncCommandWrap<TAppSession, TPackageInfo, IPackageInterface, TAsyncCommand> : IAsyncCommand<TAppSession, TPackageInfo>, ICommandWrap
        where TAppSession : IAppSession
        where TPackageInfo : IPackageInterface
        where TAsyncCommand : IAsyncCommand<TAppSession, IPackageInterface>
    {
        public TAsyncCommand InnerCommand { get; }
        /// <summary>
        /// ��ʼ���첽�����װ
        /// </summary>
        /// <param name="command">�첽����</param>
        public AsyncCommandWrap(TAsyncCommand command)
        {
            InnerCommand = command;
        }
        /// <summary>
        /// ��ʼ���첽�����װ
        /// </summary>
        /// <param name="serviceProvider">ServiceProvider</param>
        public AsyncCommandWrap(IServiceProvider serviceProvider)
        {
            InnerCommand = (TAsyncCommand)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TAsyncCommand));
        }
        /// <summary>
        /// �첽ִ��
        /// </summary>
        /// <param name="session"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public async ValueTask ExecuteAsync(TAppSession session, TPackageInfo package)
        {
            await InnerCommand.ExecuteAsync(session, package);
        }
        /// <summary>
        /// ��������
        /// </summary>
        ICommand ICommandWrap.InnerCommand
        {
            get { return InnerCommand; }
        }
    }
}
