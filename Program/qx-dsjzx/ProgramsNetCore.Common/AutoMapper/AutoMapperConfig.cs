﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Common.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomProfile());
            });
        }
    }
}
