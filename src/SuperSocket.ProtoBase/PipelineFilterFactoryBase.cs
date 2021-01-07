using System;

namespace SuperSocket.ProtoBase
{
    public abstract class PipelineFilterFactoryBase<TPackageInfo> : IPipelineFilterFactory<TPackageInfo>
    {
        /// <summary>
        /// ��������
        /// </summary>
        protected IPackageDecoder<TPackageInfo> PackageDecoder { get; private set; }
        /// <summary>
        /// ��ʼ���ܵ�ɸѡ����������
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        public PipelineFilterFactoryBase(IServiceProvider serviceProvider)
        {
            PackageDecoder = serviceProvider.GetService(typeof(IPackageDecoder<TPackageInfo>)) as IPackageDecoder<TPackageInfo>;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="client">�ͻ���</param>
        /// <returns></returns>
        protected abstract IPipelineFilter<TPackageInfo> CreateCore(object client);
        /// <summary>
        /// �����ܵ�ɸѡ��
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public virtual IPipelineFilter<TPackageInfo> Create(object client)
        {
            var filter = CreateCore(client);
            filter.Decoder = PackageDecoder;
            return filter;
        }
    }
}