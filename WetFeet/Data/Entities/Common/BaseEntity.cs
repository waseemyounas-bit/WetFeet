﻿namespace Data.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime UpdatedDate { get; set; }

    }
}
