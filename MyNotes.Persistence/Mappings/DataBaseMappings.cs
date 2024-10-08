﻿using MyNotes.Core.Models;
using MyNotes.Persistence.Entities;
using AutoMapper;

namespace MyNotes.Persistence.Mappings
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<NoteEntity, Note>();
            CreateMap<UserEntity, User>();
        }
    }
}
