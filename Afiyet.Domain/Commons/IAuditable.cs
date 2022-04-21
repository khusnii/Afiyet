using Afiyet.Domain.Enums;
using System;

namespace Afiyet.Domain.Commons
{
    public interface IAuditable
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? UpdatedBy { get; set; }
        ItemState State { get; set; }

        void Create();
        void Update();
        void Delete();



    }
}
