namespace SuperSocket.ProtoBase
{
    public interface IPipelineFilterFactory<TPackageInfo>
    {
        /// <summary>
        /// �����ܵ�ɸѡ������
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        IPipelineFilter<TPackageInfo> Create(object client);
    }
}