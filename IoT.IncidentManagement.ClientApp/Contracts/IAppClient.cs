using IoT.IncidentManagement.ClientApp.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface IAppClient
    {
        public HttpClient HttpClient { get; }

        public Task<T> GetAsync<T>(CancellationToken cancellationToken);
        public Task UpdateAsync<T>(T body, CancellationToken cancellationToken);
        public Task<V> AddAsync<T,V>(T body, CancellationToken cancellationToken);
        public Task AddAsync<T>(T body, CancellationToken cancellationToken);
        public Task DeleteAsync(CancellationToken cancellationToken);
    }
}
