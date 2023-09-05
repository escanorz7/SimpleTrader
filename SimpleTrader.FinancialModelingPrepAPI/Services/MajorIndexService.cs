using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using (HttpClient client = new())
            {
                string uri = "https://financialmodelingprep.com/api/v3/majors-indexes/" + GetUriSuffix(indexType);
                string apiKey = "?apikey=f85011934d721df2d3f8001ba4adf999";

                HttpResponseMessage response = await client.GetAsync(uri + apiKey);

                string jsonRespone = await response.Content.ReadAsStringAsync();

                MajorIndex majorIndex = JsonConvert.DeserializeObject<MajorIndex>(jsonRespone);
                majorIndex.Type = indexType;

                return majorIndex;
            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    return ".DJI";
            }
        }
    }
}