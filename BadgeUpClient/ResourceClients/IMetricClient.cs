using System.Collections.Generic;
using System.Threading.Tasks;
using BadgeUp.Responses;
using BadgeUp.Types;

namespace BadgeUp.ResourceClients
{
	public interface IMetricClient
	{
		Task<MetricResponse> Create(Metric metric);
		Task<List<MetricResponse>> GetAll();
		Task<List<MetricResponse>> GetAllBySubject(string subjectId);
		Task<MetricResponse> GetIndividualBySubject(string subjectId, string key);
	}
}
