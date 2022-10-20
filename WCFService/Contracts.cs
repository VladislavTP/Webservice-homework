using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Dictionary<string, int> GetDic(string text);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        public Dictionary<string, int> GetDic(string text)
        {
            try
            {
                DictionaryCreator.DicCreator dicCreatorInstance = new DictionaryCreator.DicCreator();
                return dicCreatorInstance.ParallelCreateDic(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
