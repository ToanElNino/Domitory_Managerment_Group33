﻿using Abp.AutoMapper;
using App.Shared.Common;
using Project.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Dto
{
    public class SVDKPDto : SVDKPEntity
    {
        //public IEnumerable<Rate> Rates { get; set; }
        //public IEnumerable<Items> Items { get; set; }
        // custome
    }
    public class GetSVDKPDto
    {
        public long? Id { get; set; }
        //public long? RoomId { get; set; }
    }

    public class CreateSVDKPDto : SVDKPEntity
    {
    }
}

