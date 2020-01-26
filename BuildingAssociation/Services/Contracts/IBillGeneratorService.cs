using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IBillGeneratorService : IBaseService<GeneratedBill>
    {
        void Generate(long mansionId, int month, int year);
    }
}
