using System;

namespace SuperSocket.ProtoBase
{
    public class DefaultPipelineFilterFactory<TPackageInfo, TPipelineFilter> : PipelineFilterFactoryBase<TPackageInfo>
        where TPipelineFilter : IPipelineFilter<TPackageInfo>, new()
    {
        /// <summary>
        /// Ĭ�Ϲܵ�ɸѡ������
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DefaultPipelineFilterFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }
        /// <summary>
        /// �����ܵ�ɸѡ������
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected override IPipelineFilter<TPackageInfo> CreateCore(object client)
        {
            return new TPipelineFilter();
        }
    }
}