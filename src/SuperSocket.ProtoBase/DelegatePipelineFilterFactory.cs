using System;

namespace SuperSocket.ProtoBase
{
    public class DelegatePipelineFilterFactory<TPackageInfo> : PipelineFilterFactoryBase<TPackageInfo>
    {
        private readonly Func<object, IPipelineFilter<TPackageInfo>> _factory;
        /// <summary>
        /// �ܵ�ɸѡ������ί��
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="factory"></param>
        public DelegatePipelineFilterFactory(IServiceProvider serviceProvider, Func<object, IPipelineFilter<TPackageInfo>> factory)
            : base(serviceProvider)
        {
            _factory = factory;
        }
        /// <summary>
        /// �����ܵ�ɸѡ������
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected override IPipelineFilter<TPackageInfo> CreateCore(object client)
        {
            return _factory(client);
        }
    }
}