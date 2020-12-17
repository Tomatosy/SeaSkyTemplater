using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.ServiceModel.Description;

namespace Tomato.StandardLib.MyFactory
{
    /// <summary>
    /// WCF工厂类
    /// </summary>
    /// <typeparam name="IType">需要创建对象的接口类型</typeparam>
    public static class WCFFactory<IType>
    {
        /// <summary>
        /// 根据接口对象名称自动创建相应的接口对象
        /// 例如接口名称为：IPortalAuth_BLL
        /// 则WCF服务对象自动认定为：PortalAuth_BLL_wcf.svc
        /// 服务地址从配置文件WcfHost结点中获取
        /// </summary>
        /// <returns>接口</returns>
        public static IType CreateObj()
        {
            IType t;
            string classname = typeof(IType).Name.Substring(1);
            string wcfHost = ConfigurationManager.AppSettings["WcfHost"];
            string wcfUfl = string.Format("{0}/{1}_wcf.svc", wcfHost, classname);
            t = CreateBLLFromWCF(wcfUfl);
            return t;
        }

        /// <summary>
        /// 根据指定WCF服务地址创建相应的接口对象
        /// </summary>
        /// <param name="WcfUrl">WCF服务URL地址</param>
        /// <returns>需要创建对象的接口类型</returns>
        public static IType CreateObj(string WcfUrl)
        {
            IType t;
            t = CreateBLLFromWCF(WcfUrl);
            return t;
        }

        private static IType CreateBLLFromWCF(string WcfUrl)
        {
            BasicHttpBinding m_Binding = new BasicHttpBinding();
            XmlDictionaryReaderQuotas m_ReaderQuotas = new XmlDictionaryReaderQuotas();
            m_Binding.MaxReceivedMessageSize = int.MaxValue;
            m_Binding.MaxBufferPoolSize = int.MaxValue;
            m_Binding.MaxBufferSize = int.MaxValue;
            m_Binding.SendTimeout = new TimeSpan(1, 1, 1);
            m_ReaderQuotas.MaxArrayLength = int.MaxValue;
            m_ReaderQuotas.MaxStringContentLength = int.MaxValue;
            m_ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            m_ReaderQuotas.MaxDepth = 32;
            m_ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
            m_Binding.ReaderQuotas = m_ReaderQuotas;

            EndpointAddress endaddress = new EndpointAddress(WcfUrl);

            ChannelFactory<IType> channelFactory = new ChannelFactory<IType>(m_Binding);
            foreach (OperationDescription op in channelFactory.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }

            IType t;
            t = channelFactory.CreateChannel(endaddress);
            return t;
        }
    }
}
