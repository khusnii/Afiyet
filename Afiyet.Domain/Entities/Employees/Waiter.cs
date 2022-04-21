using Afiyet.Domain.Commons;
using Afiyet.Domain.Entities.Locations;
using Afiyet.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Afiyet.Domain.Entities.Employees
{
    public class Waiter : Person, IAuditable
    {

        public Guid Id { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? UpdatedBy { get; set; }
        public ItemState State { get; set; }

        public void Create()
        {
            this.CreatedAt = DateTime.Now;
            this.State = ItemState.Created;
        }

        public void Delete()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Deleted;
        }

        public void Update()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Upadated;
        }
    }
}
