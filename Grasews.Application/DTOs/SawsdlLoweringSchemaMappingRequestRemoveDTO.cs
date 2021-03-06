﻿using Grasews.Domain.Interfaces.DTOs;

namespace Grasews.Application.DTOs
{
    public class SawsdlLoweringSchemaMappingRequestRemoveDTO : ISawsdlLoweringSchemaMappingRequestRemoveDTO
    {
        public int IdServiceDescription { get; set; }
        public int IdServiceDescriptionElement { get; set; }
        public int IdOwnerUser { get; set; }
    }
}